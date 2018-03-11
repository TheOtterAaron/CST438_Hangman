using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hangman.Data;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace Hangman.Pages
{
    public class IndexModel : PageModel
    {
        // Private (Injected) Members
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;

        // Constructor
        public IndexModel(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        
        // Members
        public Game Game { get; private set; }
        public string Phrase { get; private set; }
        public string Message { get; private set; }

        [BindProperty]
        public char Guess { get; set; }

        // GET Controller
        public async Task OnGetAsync()
        {
            // Create a new Game record
            Game = new Game();
            Game.Phrase = new PhraseGenerator(_env).GetPhrase();

            // Save Game record
            _db.Games.Add(Game);
            await _db.SaveChangesAsync();

            // Generate Phrase
            HiddenWord hiddenWord = new HiddenWord(Game.Phrase);
            Phrase = hiddenWord.ToString();
        }

        // POST Controller
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Retrieve Game record
            Game = await _db.Games.FindAsync(id);

            // Generate Phrase
            HiddenWord hiddenWord = new HiddenWord(Game.Phrase, Game.Guesses);
            Phrase = hiddenWord.ToString();
            
            if (ModelState.IsValid && Game.IncorrectGuesses < 7)
            {
                // Get previous guesses
                List<char> guesses = Game.Guesses;

                // Guess must be a letter
                Regex rgx = new Regex(@"^[a-zA-Z]$");
                if (!rgx.IsMatch(Guess.ToString()))
                {
                    Message = "You can only guess letters.";
                    return Page();
                }

                // Guess must be new
                foreach (char prevGuess in guesses)
                {
                    if (Guess == prevGuess)
                    {
                        Message = "You already guessed '" + Guess + "'.";
                        return Page();
                    }
                }
                
                // Add Guess to Game record
                guesses.Add(Guess);
                Game.Guesses = guesses;
                
                if (hiddenWord.Reveal(Guess))   // Correct Guess...
                {
                    // ...update Phrase
                    Phrase = hiddenWord.ToString();
                }
                else                            // Incorrect Guess...
                {
                    // ...update Game record
                    Game.IncorrectGuesses++;
                }

                // Save Game record
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new System.Exception($"Game {Game.Id} not found!");
                }
            }
            
            if (hiddenWord.IsRevealed())            // Check win condition
            {
                Message = "Congratulations, you've won!";
            }
            else if (Game.IncorrectGuesses >= 7)    // Check lose condition
            {
                Game.IncorrectGuesses = 7;
                Message = "You've run out of guesses.";
            }

            return Page();
        }
    }
}
