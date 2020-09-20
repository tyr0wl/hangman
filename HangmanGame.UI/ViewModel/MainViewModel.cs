using System;
using System.Windows.Input;
using HangmanGame.EventArgsObjects;
using HangmanGame.Extensions;
using HangmanGame.UI.Commands;

namespace HangmanGame.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Hangman _hangman;

        private string _foundWord;

        private string _gameStatusText;

        private bool? _isWon;

        private string _soughtWord;
        public EventHandler AttemptFailed;
        public EventHandler NewGameStarted;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(Hangman hangman)
        {
            _hangman = hangman ?? throw new ArgumentNullException(nameof(hangman));

            Init();
        }

        public bool GameStarted => _hangman.GameStarted;

        public string GameStatusText
        {
            get => _gameStatusText;
            set
            {
                _gameStatusText = value;
                Notify(() => GameStatusText);
            }
        }

        public string SoughtWord
        {
            get => _soughtWord;
            set
            {
                _soughtWord = value;
                Notify(() => SoughtWord);
            }
        }

        public string FoundWord
        {
            get => _foundWord;
            set
            {
                _foundWord = value;
                Notify(() => FoundWord);
            }
        }

        public bool? IsWon
        {
            get => _isWon;
            set
            {
                _isWon = value;
                Notify(() => IsWon);
            }
        }

        public ICommand NewWordCommand { get; set; }
        public ICommand LetterBoxCommand { get; set; }

        private void Init()
        {
            InitCommands();
            InitEvents();
            IsWon = null;
        }

        private void InitCommands()
        {
            NewWordCommand = new RelayCommand(NewWord);
            LetterBoxCommand = new GenericRelayCommand<char>(Attempt);
        }

        private void InitEvents()
        {
            _hangman.Won += OnWon;
            _hangman.Lost += OnLost;
        }

        private void OnLost(object s, HangmanEventArgs e)
        {
            var statusText = string.Format(Properties.Resources.GameLostMessage, e.SearchTerm.Sought);
            UpdateGameStatus(statusText);
        }

        private void OnWon(object s, HangmanEventArgs e)
        {
            var statusText = string.Format(Properties.Resources.GameWonMessage, e.AttemptsUsed);
            UpdateGameStatus(statusText);
        }

        public void UpdateGameStatus(string gameStatusText)
        {
            Notify(() => GameStarted);
            IsWon = _hangman.IsWon;
            GameStatusText = gameStatusText;
        }

        public void NewWord()
        {
            Reset();
            _hangman.StartGame();
            Notify(() => GameStarted);

            SoughtWord = _hangman.SelectedTerm.Sought;
            FoundWord = _hangman.SelectedTerm.Found.InsertBackspaces();

            RaiseEvent(NewGameStarted);
        }

        private void Reset()
        {
            UpdateGameStatus(string.Empty);
            IsWon = null;
        }

        public void Attempt(char attemptChar)
        {
            var success = _hangman.Attempt(attemptChar);
            if (!success)
            {
                RaiseEvent(AttemptFailed);
            }

            UpdateFoundWord();
        }

        private void RaiseEvent(EventHandler eventHandler)
        {
            eventHandler?.Invoke(this, new EventArgs());
        }

        private void UpdateFoundWord()
        {
            FoundWord = _hangman.SelectedTerm.Found.InsertBackspaces();
        }
    }
}