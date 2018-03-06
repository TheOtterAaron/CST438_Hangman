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

        public string Word { get; private set; }
        public int GuessCount { get; private set; }

        public void OnGet()
        {
            Word = "_ _ _ _ _ _ _ _";
            GuessCount = 0;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Guess.Count >= 7)
                {
                    Word = "Out of Guesses!";
                    return Page();
                }

                GuessCount = Guess.Count + 1;
            }

            return Page();
        }
    }
}
