using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Hangman
{
    public class PhraseGenerator
    {
        // Private (Injected) Members
        private readonly IHostingEnvironment _env;

        // Constructor
        public PhraseGenerator(IHostingEnvironment env)
        {
            _env = env;
        }

        // Returns a word selected at random from the hangmanwords.txt file
        public string GetPhrase()
        {
            string wordsFile = Path.Combine(_env.WebRootPath, "hangmanwords.txt");

            if (File.Exists(wordsFile))
            {
                string[] words = File.ReadAllLines(wordsFile);

                Random rand = new Random();
                int randWord = rand.Next(0, words.Length);

                return words[randWord];
            }
            else
            {
                return "computer";
            }
        }
    }
}
