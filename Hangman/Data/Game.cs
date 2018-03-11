using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hangman.Data
{
    // Definition of Game record
    public class Game
    {
        // Members
        public int Id { get; set; }
        public string Phrase { get; set; }
        public string GuessesString { get; private set; }
        public int IncorrectGuesses { get; set; }

        // Property for accessing GuessesString as a List
        // (Guesses are stored as a string for database compatibility,
        // but the below property let's us work with them as though
        // they were stored in a List object)
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

        // Constructor
        public Game()
        {
            Phrase = "";
            GuessesString = "";
            IncorrectGuesses = 0;
        }
    }
}
