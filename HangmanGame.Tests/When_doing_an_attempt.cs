using System.Collections.Generic;
using NUnit.Framework;

namespace HangmanGame.Tests
{
    public class When_doing_an_attempt
    {
        private const int AttemptCount = 3;

        private readonly List<char> _expectedCharsFound = new EventedList<char> { 't', '_', '_', 't' };
        private readonly Term _expectedWord = new Term("test");
        private Hangman _hangman;

        [SetUp]
        public void SetUp()
        {
            _hangman = new Hangman(new List<string> { _expectedWord }, AttemptCount);
            _hangman.StartGame();
        }

        [Test]
        public void Should_count_attempts_down_when_letter_was_not_in_word()
        {
            _hangman.Attempt('d');
            Assert.AreEqual(AttemptCount - 1, _hangman.AttemptsLeft);
        }

        [Test]
        public void Should_not_count_attempts_down_when_letter_was_in_word()
        {
            _hangman.Attempt('e');
            Assert.AreEqual(AttemptCount, _hangman.AttemptsLeft);
        }

        [Test]
        public void Should_return_true_when_letter_was_not_in_word()
        {
            Assert.IsTrue(_hangman.Attempt('t'));
        }

        [Test]
        public void Should_include_all_found_matches_of_the_letter()
        {
            _hangman.Attempt('t');
            Assert.AreEqual(_expectedCharsFound, _hangman.SelectedTerm.Found.Chars);
        }

        [Test]
        public void Matching_should_not_be_case_sensitive()
        {
            _hangman.Attempt('T');
            Assert.AreEqual(_expectedCharsFound, _hangman.SelectedTerm.Found.Chars);
        }

        [Test]
        public void Game_should_be_lost_when_attempts_are_depleted()
        {
            _hangman.Attempt('a');
            _hangman.Attempt('a');
            _hangman.Attempt('a');
            Assert.IsTrue(_hangman.IsLost);
        }

        [Test]
        public void Game_should_be_won_when_word_is_found()
        {
            _hangman.Attempt('t');
            _hangman.Attempt('e');
            _hangman.Attempt('s');
            Assert.IsTrue(_hangman.IsWon);
        }
    }
}