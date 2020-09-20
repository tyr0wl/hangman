using System;
using System.Collections.Generic;
using System.IO;
using HangmanGame.EventArgsObjects;
using HangmanGame.Exceptions;
using HangmanGame.Extensions;
using HangmanGame.Resources;

namespace HangmanGame
{
    /// <summary>
    ///     Class that simulates a hangman game.
    /// </summary>
    public class Hangman
    {
        private int _attemptsLeft;

        /// <summary>
        ///     Term that was selected in the previous game.
        /// </summary>
        private Term _oldTerm;

        private SearchTerm _selectedTerm;

        /// <summary>
        ///     Event raised when the game is lost.
        /// </summary>
        public EventHandler<HangmanEventArgs> Lost;

        /// <summary>
        ///     Event raised when the game is won.
        /// </summary>
        public EventHandler<HangmanEventArgs> Won;

        /// <summary>
        ///     Creates an instance of the hangman class.
        /// </summary>
        /// <param name="termList">Term list that will be used for the games.</param>
        /// <param name="attempts">How much attempts each game has until it is lost.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="termList"/> can not be null</exception>
        /// <exception cref="InvalidDataException">Thrown then <paramref name="attempts"/> are set to zero or below.</exception>
        public Hangman(List<string> termList, int attempts)
        {
            if (attempts <= 0)
            {
                throw new InvalidDataException(ExceptionMessages.AttemptsCanNotBeZeroOrNegative);
            }

            TermList = termList ?? throw new ArgumentNullException(nameof(termList));
            Attempts = attempts;
            AttemptsLeft = Attempts;
        }

        /// <summary>
        ///     Term list used for the WordSelection
        /// </summary>
        public List<string> TermList { get; set; }

        /// <summary>
        ///     Total Amount of Attempts for a game.
        /// </summary>
        public int Attempts { get; }

        /// <summary>
        ///     How much Attempts are left until the game is lost.
        /// </summary>
        /// <exception cref="GameNotStartedException">There can not be attempts left if the game is not started.</exception>
        public int AttemptsLeft
        {
            get
            {
                ThrowWhenGameNotStarted();
                return _attemptsLeft;
            }
            private set => _attemptsLeft = value;
        }

        /// <summary>
        ///     How much Attempts where used in the game.
        /// </summary>
        /// <exception cref="GameNotStartedException">There can not be attempts used if the game is not started.</exception>
        public int AttemptsUsed
        {
            get
            {
                ThrowWhenGameNotStarted();
                return Attempts - AttemptsLeft;
            }
        }

        /// <summary>
        ///     Selected Term for the actual game.
        /// </summary>
        /// <exception cref="GameNotStartedException">There can not be a selected Term if the game is not started.</exception>
        public SearchTerm SelectedTerm
        {
            get
            {
                if (GameNotStartedOnce())
                {
                    Throw(ExceptionMessages.NoSelectedTermIfNotStartedOnce);
                }

                return _selectedTerm;
            }
            private set => _selectedTerm = value;
        }

        /// <summary>
        ///     if the actual game was won.
        /// </summary>
        public bool IsWon { get; private set; }

        /// <summary>
        ///     if the actual game was lost.
        /// </summary>
        public bool IsLost { get; private set; }

        /// <summary>
        ///     States if a game is started and in progress.
        /// </summary>
        public bool GameStarted { get; private set; }

        /// <summary>
        ///     Starts a new game.
        /// </summary>
        public void StartGame()
        {
            SelectNewTerm();
            Reset();
            GameStarted = true;
        }

        /// <summary>
        ///     Starts a new game.
        /// </summary>
        public void NewGame()
        {
            StartGame();
        }

        /// <summary>
        ///     Stops the current game.
        /// </summary>
        public void StopGame()
        {
            if (GameStarted)
            {
                SetGameToLost();
            }
        }

        private void SelectNewTerm()
        {
            var newWord = new Term(TermList.RandomItem());

            if (SameTermIsSelectedAgain(newWord))
            {
                SelectNewTerm();
                return;
            }

            _oldTerm = _selectedTerm == null ? null : SelectedTerm.Sought;
            SelectedTerm = new SearchTerm(newWord);
        }

        private bool SameTermIsSelectedAgain(Term newWord)
        {
            return newWord == _oldTerm && TermList.Count > 1;
        }

        /// <summary>
        ///     Resets current Progress of a game.
        /// </summary>
        public void Reset()
        {
            IsWon = false;
            IsLost = false;
            _attemptsLeft = Attempts;
            _selectedTerm.ResetProgress();
        }

        /// <summary>
        ///     Searches for occurrences of the char in the sought word and inserts it into the found word if it was found.
        /// </summary>
        /// <param name="attemptChar">char to match and insert</param>
        /// <returns>true if char occurs in the sought word.</returns>
        public bool Attempt(char attemptChar)
        {
            if (!GameStarted)
            {
                throw new GameNotStartedException(ExceptionMessages.CanNotAttemptWhenGameNotStarted);
            }

            var attemptSuccessful = SelectedTerm.TryChar(attemptChar);

            if (!attemptSuccessful)
            {
                AttemptsLeft--;
            }

            CheckWinConditions();

            return attemptSuccessful;
        }

        private void CheckWinConditions()
        {
            if (GameIsLost())
            {
                SetGameToLost();
            }
            else if (GameIsWon())
            {
                SetGameToWon();
            }
        }

        public bool TryToSolve(string wordToTry)
        {
            if (!GameStarted)
            {
                throw new GameNotStartedException(ExceptionMessages.CanNotAttemptWhenGameNotStarted);
            }

            var trySuccessful = SelectedTerm.TryWord(wordToTry);

            if (!trySuccessful)
            {
                AttemptsLeft--;
            }

            CheckWinConditions();

            return trySuccessful;
        }

        private void SetGameToLost()
        {
            IsWon = false;
            IsLost = true;
            var hangmanEventArgs = GetHangmanEventArgs();
            GameStarted = false;
            RaiseLostEvent(hangmanEventArgs);
        }

        private void SetGameToWon()
        {
            IsWon = true;
            IsLost = false;
            var hangmanEventArgs = GetHangmanEventArgs();
            GameStarted = false;
            RaiseWonEvent(hangmanEventArgs);
        }

        private HangmanEventArgs GetHangmanEventArgs()
        {
            return new HangmanEventArgs(_selectedTerm, AttemptsUsed, Attempts);
        }

        private bool GameIsLost()
        {
            return _attemptsLeft <= 0 && GameStarted;
        }

        private bool GameIsWon()
        {
            return _selectedTerm.IsWordFound();
        }

        private void RaiseLostEvent(HangmanEventArgs hangmanEventArgs)
        {
            Lost?.Invoke(this, hangmanEventArgs);
        }

        private void RaiseWonEvent(HangmanEventArgs hangmanEventArgs)
        {
            Won?.Invoke(this, hangmanEventArgs);
        }

        private void ThrowWhenGameNotStarted()
        {
            if (!GameStarted)
            {
                Throw(ExceptionMessages.CanNotAccessIfGameNotStarted);
            }
        }

        private static void Throw(string message)
        {
            throw new GameNotStartedException(message);
        }

        private bool GameNotStartedOnce()
        {
            return !GameStarted && !(IsWon ^ IsLost);
        }
    }
}