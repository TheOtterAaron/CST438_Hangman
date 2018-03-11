using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hangman.Data
{
    public class Game
    {
        public int Id { get; set; }
        public string Phrase { get; set; }
        public string GuessesString { get; private set; }
        public int IncorrectGuesses { get; set; }

        [NotMapped]
        public List<char> Guesses
        {
            get
            {
                return new List<char>(GuessesString.ToCharArray());
            }

            set
            {
                GuessesString = "";
                foreach (char c in value)
                {
                    GuessesString += char.ToLower(c);
                }
            }
        }

        public Game()
        {
            Phrase = "";
            GuessesString = "";
            IncorrectGuesses = 0;
        }
    }
}
