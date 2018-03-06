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

        public void OnGet()
        {
            Guess = new Guess();
            Guess.Count = 0;
            Guess.Character = ' ';
        }

        public async Task<IActionResult> OnPostAsync()
        {
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

                Guess.Count++;
            }

            return Page();
        }
    }
}
