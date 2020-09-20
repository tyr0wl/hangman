using System.Collections.Generic;
using HangmanGame.Exceptions;
using NUnit.Framework;

namespace HangmanGame.Tests
{
    public class When_trying_to_solve
    {
        private const int AttemptCount = 3;
        private readonly Term _expectedWord = new Term("test");

        private Hangman _hangman;

        [SetUp]
        public void SetUp()
        {
            _hangman = new Hangman(new List<string> { _expectedWord }, AttemptCount);
            _hangman.StartGame();
        }

        [Test]
        public void Game_Should_count_attempts_down_if_not_successful()
        {
            _hangman.TryToSolve("1");
            Assert.AreEqual(AttemptCount - 1, _hangman.AttemptsLeft);
        }

        [Test]
        public void Game_Should_be_won_if_successful()
        {
            _hangman.TryToSolve(_expectedWord);
            Assert.That(_hangman.IsWon);
        }

        [Test]
        public void Should_Throw_when_game_is_not_started()
        {
            _hangman.StopGame();
            Assert.Throws<GameNotStartedException>(() => _hangman.TryToSolve(_expectedWord));
        }
    }
}