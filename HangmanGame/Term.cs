using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HangmanGame.Extensions;

namespace HangmanGame
{
    /// <summary>
    ///     Class that describes a Term for the Hangman class.
    /// </summary>
    public class Term : IEquatable<Term>
    {
        private string _string;

        /// <summary>
        ///     Creates a Term object with a string as parameter.
        /// </summary>
        /// <param name="word">string that represents the Term</param>
        public Term(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            SetTerm(word);
        }

        /// <summary>
        ///     The Term as string.
        /// </summary>
        public string String
        {
            get => _string;
            set => SetTerm(value);
        }

        /// <summary>
        ///     The specific chars of the Term as eventedList.
        /// </summary>
        public ObservableCollection<char> Chars { get; private set; }

        /// <summary>
        ///     implicit conversation Term object to String
        /// </summary>
        /// <param name="term">Term object to convert.</param>
        /// <returns>String representation of the Term object</returns>
        public static implicit operator string(Term term)
        {
            return term.String;
        }

        /// <summary>
        ///     Implicit conversation String to Term object.
        /// </summary>
        /// <param name="word">string to convert</param>
        /// <returns>Term object</returns>
        public static implicit operator Term(string word)
        {
            return new Term(word);
        }

        /// <summary>
        ///     Changes the string thar represents the Term and the List of chars to the new value.
        /// </summary>
        /// <param name="word">the new Term.</param>
        private void SetTerm(string word)
        {
            _string = word;
            Chars = new ObservableCollection<char>(word.ToCharArray());
            Chars.CollectionChanged += OnCharsIndexChanged;
        }

        /// <summary>
        ///     Changes the string property when the List of chars is changed.
        /// </summary>
        private void OnCharsIndexChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var newValue = notifyCollectionChangedEventArgs.NewItems.OfType<char>().FirstOrDefault();
            String = String.Replace(notifyCollectionChangedEventArgs.OldStartingIndex, newValue);
        }

        /// <summary>
        ///     String representation of the Term
        /// </summary>
        /// <returns>Term as String</returns>
        public override string ToString()
        {
            return String;
        }

        /// <summary>
        ///     Changes all chars in the Term to lower and returns the new Term.
        /// </summary>
        /// <returns>Term in lower chars.</returns>
        public Term ToLower()
        {
            return ToString().ToLower();
        }


        // Ensures that 2 Words with the same value are considered equal, even when they have different references.

        #region Equality Members

        public bool Equals(Term other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.String, String);
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

            if (obj.GetType() != typeof(Term))
            {
                return false;
            }

            return Equals((Term) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((String != null ? String.GetHashCode() : 0) * 397) ^ (Chars != null ? Chars.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Term left, Term right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Term left, Term right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}