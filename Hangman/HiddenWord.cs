using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    public class HiddenWord
    {
        private class HiddenLetter
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

        private List<HiddenLetter> myWord;

        public HiddenWord(string word)
        {
            myWord = new List<HiddenLetter>();

            foreach (char c in word.ToArray<char>())
            {
                myWord.Add(new HiddenLetter(c, false));
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

            foreach (HiddenLetter l in myWord)
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
            foreach (HiddenLetter l in myWord)
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

            foreach (HiddenLetter l in myWord)
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
