using System.Collections.Generic;
using HangmanGame.Exceptions;
using NUnit.Framework;

namespace HangmanGame.Tests
{
    [TestFixture]
    public class When_Hangman_is_not_started
    {
        [SetUp]
        public void SetUp()
        {
            _hangman = GetNewHangmanObject();
        }

        [TearDown]
        public void TearDown()
        {
            _hangman = GetNewHangmanObject();
        }

        private const int AttemptCount = 3;
        private Hangman _hangman;

        private static Hangman GetNewHangmanObject()
        {
            return new Hangman(new List<string> { "test" }, AttemptCount);
        }

        [Test]
        public void Should_throw_when_trying_to_do_an_attempt()
        {
            Assert.Throws<GameNotStartedException>(() => _hangman.Attempt(' '));
        }

        [Test]
        public void Should_throw_when_trying_to_access_AttemptsLeft()
        {
            Assert.Throws<GameNotStartedException>(() =>
            {
                var invalidAccess = _hangman.AttemptsLeft;
            });
        }

        [Test]
        public void Should_throw_when_trying_to_access_AttemptsUsed()
        {
            Assert.Throws<GameNotStartedException>(() =>
            {
                var invalidAccess = _hangman.AttemptsUsed;
            });
        }

        [Test]
        public void Should_throw_when_trying_to_access_selectedTerm_and_Game_was_not_started_before()
        {
            Assert.Throws<GameNotStartedException>(() =>
            {
                var invalidAccess = _hangman.SelectedTerm;
            });
            _hangman.StartGame();
            _hangman.StopGame();
            Assert.DoesNotThrow(() =>
            {
                var invalidAccess = _hangman.SelectedTerm;
            });
        }
    }
}