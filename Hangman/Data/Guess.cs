using System.ComponentModel.DataAnnotations;

namespace Hangman.Data
{
    public class Guess
    {
        private char character;

        public int Count
        {
            get;
            set;
        }

        [Required]
        public char Character
        {
            get
            {
                return character;
            }
            set
            {
                character = char.ToLower(value);
            }
        }
    }
}