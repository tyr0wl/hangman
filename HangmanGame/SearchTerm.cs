using System;
using System.Linq;
using System.Text;
using HangmanGame.Extensions;

namespace HangmanGame
{
    /// <summary>
    ///     Search term for the Hangman class.
    /// </summary>
    public class SearchTerm : IEquatable<SearchTerm>
    {
        /// <summary>
        ///     Placeholder for character not found yet.
        /// </summary>
        private const char ReplacementCharacter = '_';

        private const char Backspace = ' ';

        /// <summary>
        ///     Creates an instance of the SearchTerm instance.
        /// </summary>
        /// <param name="sought">Term to search.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="sought" /> is <c>null</c></exception>
        public SearchTerm(Term sought)
        {
            if (sought == null)
            {
                throw new ArgumentNullException(nameof(sought));
            }

            Sought = sought;
            InitFoundWord();
        }

        /// <summary>
        ///     Term that is sought.
        /// </summary>
        public Term Sought { get; }

        /// <summary>
        ///     What was found of the sought Term.
        /// </summary>
        public Term Found { get; set; }

        /// <summary>
        ///     Creates the Found Term with help of the Sought Term and replaces
        ///     its characters with the Replacement Character.
        /// </summary>
        private void InitFoundWord()
        {
            var placeHolder = new StringBuilder();

            foreach (var @char in Sought.Chars)
            {
                placeHolder.Append(GetReplacementChar(@char));
            }

            Found = new Term(placeHolder.ToString());
        }

        //TODO: Sentence object required to display multiple words.
        /// <summary>
        ///     Defines if the Replacement Character should be used or if a Backspace should be taken instead.
        /// </summary>
        /// <param name="char"></param>
        /// <returns></returns>
        private static char GetReplacementChar(char @char)
        {
            return @char == Backspace ? Backspace : ReplacementCharacter;
        }

        /// <summary>
        ///     Tests if the provided char is in the sought Term. If this is the case,
        ///     then it inserts the Char in the Found Term.
        /// </summary>
        /// <param name="char">char to test</param>
        /// <returns>true when its in the Term, otherwise false.</returns>
        public bool TryChar(char @char)
        {
            if (Contains(@char))
            {
                InsertMatchingCharInWordFound(@char);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Tests if the provided word is the sought Term. If this is the case,
        ///     then it sets the word to the Found Term.
        /// </summary>
        /// <param name="word">word to test</param>
        /// <returns>true when the word is the sought Term, otherwise false.</returns>
        public bool TryWord(string word)
        {
            var wordMatch = Sought.String.ToLower() == word.ToLower();

            if (wordMatch)
            {
                Found = word;
            }

            return wordMatch;
        }

        /// <summary>
        ///     Inserts the char in the Found Term where it belongs.
        /// </summary>
        /// <param name="charToMatch">char to insert</param>
        private void InsertMatchingCharInWordFound(char charToMatch)
        {
            for (var index = 0; index < Sought.Chars.Count; index++)
            {
                var @char = Sought.Chars[index];

                if (@char.Match(charToMatch))
                {
                    Found.Chars[index] = @char;
                }
            }
        }

        /// <summary>
        ///     Tests if the char is Found in the sought Term.
        /// </summary>
        /// <param name="char">char to test.</param>
        /// <returns>true if it is in the word, otherwise false.</returns>
        public bool Contains(char @char)
        {
            var lowerCharArray = Sought.Chars.Select(c => c.ToLower());
            return lowerCharArray.Contains(@char.ToLower());
        }

        /// <summary>
        ///     Resets the Search progress. (All chars of the Found Term are reset to the Replacement character)
        /// </summary>
        public void ResetProgress()
        {
            InitFoundWord();
        }

        /// <summary>
        ///     Tests if all characters of the Term were found.
        /// </summary>
        public bool IsWordFound()
        {
            return Sought.ToLower() == Found.ToLower();
        }

        public bool Equals(SearchTerm other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.Sought, Sought) && Equals(other.Found, Found);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(SearchTerm))
            {
                return false;
            }

            return Equals((SearchTerm) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Sought != null ? Sought.GetHashCode() : 0) * 397) ^ (Found != null ? Found.GetHashCode() : 0);
            }
        }

        public static bool operator ==(SearchTerm left, SearchTerm right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SearchTerm left, SearchTerm right)
        {
            return !Equals(left, right);
        }
    }
}