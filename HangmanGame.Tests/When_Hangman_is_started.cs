using System.Collections.Generic;
using NUnit.Framework;

namespace HangmanGame.Tests
{
    [TestFixture]
    public class When_Hangman_is_started
    {
        [SetUp]
        public void SetUp()
        {
            _hangman = new Hangman(new List<string> { new Term("test") }, AttemptCount);
            _hangman.StartGame();
        }

        [TearDown]
        public void TearDown()
        {
            _hangman.Reset();
        }

        private const int AttemptCount = 3;
        private Hangman _hangman;

        [Test]
        public void Game_Should_be_started()
        {
            Assert.IsTrue(_hangman.GameStarted);
        }

        [Test]
        public void Game_should_not_be_lost_or_won()
        {
            Assert.IsFalse(_hangman.IsLost);
            Assert.IsFalse(_hangman.IsWon);
        }

        [Test]
        public void Should_choose_a_single_word_in_wordlist()
        {
            Assert.IsNotNull(_hangman.SelectedTerm.Sought);
        }

        [Test]
        public void Game_should_be_lost_when_stopped()
        {
            _hangman.StopGame();
            Assert.IsTrue(_hangman.IsLost);
        }
    }
}