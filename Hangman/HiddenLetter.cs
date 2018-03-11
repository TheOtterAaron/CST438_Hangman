namespace Hangman
{
    // Stores a character with a boolean used to identify if said
    // character should be hidden or visible
    public class HiddenLetter
    {
        // Members
        public char Letter { get; private set; }
        public bool Revealed { get; set; }

        // Constructor
        public HiddenLetter(char letter, bool revealed)
        {
            Letter = letter;
            Revealed = revealed;
        }
    }
}
