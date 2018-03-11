using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    // Wraps a List of HiddenLetter objects to represent a word
    // or phrase that is only partially known
    public class HiddenWord
    {
        // Private Member
        private List<HiddenLetter> _word;

        // Constructor
        public HiddenWord(string word)
        {
            _word = new List<HiddenLetter>();

            foreach (char c in word.ToArray<char>())
            {
                _word.Add(new HiddenLetter(c, false));
            }
        }

        // Overloaded Constructor
        // Accepts an enumerable collection of initially revealed letters
        public HiddenWord(string word, IEnumerable<char> revealed) : this(word)
        {
            foreach (char c in revealed)
            {
                Reveal(c);
            }
        }

        // Marks all instance of a given letter as visible
        // Returns true if at least one letter is made visible as a result
        // of this call, false otherwise
        public bool Reveal(char letter)
        {
            bool found = false;

            foreach (HiddenLetter l in _word)
            {
                if (l.Letter == letter)
                {
                    l.Revealed = true;
                    found = true;
                }
            }

            return found;
        }

        // Returns true if all letters in the word are revealed
        public bool IsRevealed()
        {
            foreach (HiddenLetter l in _word)
            {
                if (!l.Revealed)
                {
                    return false;
                }
            }

            return true;
        }

        // Returns a human-friendly string where hidden letters are
        // replaced with underscores _
        public override string ToString()
        {
            string s = "";

            foreach (HiddenLetter l in _word)
            {
                if (l.Revealed)
                {
                    s += l.Letter + " ";
                }
                else
                {
                    s += "_ ";
                }
            }

            return s;
        }
    }
}
