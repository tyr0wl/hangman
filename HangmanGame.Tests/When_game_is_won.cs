using System.Collections.Generic;
using HangmanGame.EventArgsObjects;
using NUnit.Framework;

namespace HangmanGame.Tests
{
    public class When_game_is_won
    {
        private const int AttemptCount = 3;
        private readonly Term _expectedWord = new Term("test");
        private HangmanEventArgs _actualHangmanEventArgs;

        private HangmanEventArgs _expectedHangmanEventArgs;

        private Hangman _hangman;

        [SetUp]
        public void SetUp()
        {
            var expectedTerm = new SearchTerm(new Term("test"));
            expectedTerm.TryWord(_expectedWord);
            _expectedHangmanEventArgs = new HangmanEventArgs(expectedTerm, 0, AttemptCount);

            _hangman = new Hangman(new List<string> { _expectedWord }, AttemptCount);
            _hangman.StartGame();
            _hangman.Won += OnWin;
            _hangman.TryToSolve(_expectedWord);
        }

        private void OnWin(object sender, HangmanEventArgs e)
        {
            _actualHangmanEventArgs = e;
        }

        [Test]
        public void Won_Event_should_be_raised()
        {
            Assert.IsNotNull(_actualHangmanEventArgs);
        }

        [Test]
        public void Raised_EventArgs_should_have_expected_values()
        {
            Assert.AreEqual(_expectedHangmanEventArgs.Attempts, _actualHangmanEventArgs.Attempts);
            Assert.AreEqual(_expectedHangmanEventArgs.AttemptsUsed, _actualHangmanEventArgs.AttemptsUsed);
            Assert.AreEqual(_expectedHangmanEventArgs.SearchTerm, _actualHangmanEventArgs.SearchTerm);
        }
    }
}