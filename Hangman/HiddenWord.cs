using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    public class HiddenWord
    {
        private List<HiddenLetter> _word;

        public HiddenWord(string word)
        {
            _word = new List<HiddenLetter>();

            foreach (char c in word.ToArray<char>())
            {
                _word.Add(new HiddenLetter(c, false));
            }
        }

        public HiddenWord(string word, IEnumerable<char> revealed) : this(word)
        {
            foreach (char c in revealed)
            {
                Reveal(c);
            }
        }

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
