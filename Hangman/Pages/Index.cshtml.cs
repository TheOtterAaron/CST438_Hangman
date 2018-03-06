using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Hangman.Pages
{
    public class Index : PageModel
    {
        public string Message { get; private set; } = "PageModel.Message";

        public void OnGet()
        {
            Message += $" Server time is {DateTime.Now}";
        }
    }
}
