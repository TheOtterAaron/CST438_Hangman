using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hangman.Data;

namespace Hangman.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Guess Guess { get; set; }

        public string Output { get; private set; }
        public int GuessCount { get; private set; }

        public void OnGet()
        {
            GuessCount = 0;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            GuessCount = Guess.Count;

            if (ModelState.IsValid)
            {
                HiddenWord hiddenWord = new HiddenWord("computer");
                hiddenWord.Reveal(Guess.Character);
                Output = hiddenWord.ToString();

                if (Guess.Count >= 7)
                {
                    Output = "Out of Guesses!";
                    return Page();
                }

                GuessCount++;
            }

            return Page();
        }
    }
}
