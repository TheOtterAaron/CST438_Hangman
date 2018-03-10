using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hangman.Data;

namespace Hangman.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext m_db;

        public IndexModel(AppDbContext db)
        {
            m_db = db;
        }
        
        public Game Game { get; private set; }

        [BindProperty]
        public char Guess { get; set; }

        public string Phrase { get; private set; }
        public string Message { get; private set; }

        public async Task OnGetAsync()
        {
            Game = new Game();
            m_db.Games.Add(Game);
            await m_db.SaveChangesAsync();

            HiddenWord hiddenWord = new HiddenWord(Game.Phrase);
            Phrase = hiddenWord.ToString();

            Guess = ' ';
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Game = await m_db.Games.FindAsync(id);

            HiddenWord hiddenWord = new HiddenWord(Game.Phrase, Game.Guesses);

            if (ModelState.IsValid)
            {
                List<char> guesses = Game.Guesses;
                bool validGuess = true;

                foreach (char prevGuess in guesses)
                {
                    if (Guess == prevGuess)
                    {
                        Message = "You already guessed '" + Guess + "'.";
                        validGuess = false;
                    }
                }

                if (validGuess)
                {
                    guesses.Add(Guess);
                    Game.Guesses = guesses;

                    if (!hiddenWord.Reveal(Guess))
                    {
                        Game.IncorrectGusses++;
                    }

                    try
                    {
                        await m_db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw new System.Exception($"Game {Game.Id} not found!");
                    }
                }
            }
            
            Phrase = hiddenWord.ToString();

            if (hiddenWord.IsRevealed())
            {
                Message = "Congratulations, you've won!";
            }
            else if (Game.IncorrectGusses >= 7)
            {
                Game.IncorrectGusses = 7;
                Message = "You've run out of guesses.";
            }

            return Page();
        }
    }
}
