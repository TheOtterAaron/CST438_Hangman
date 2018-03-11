namespace Hangman
{
    public class HiddenLetter
    {
        public char Letter
        {
            get;
            private set;
        }

        public bool Revealed
        {
            get;
            set;
        }

        public HiddenLetter(char letter, bool revealed)
        {
            Letter = letter;
            Revealed = revealed;
        }
    }
}
