using System;
using System.Windows;
using HangmanGame.UI.ViewModel;

namespace HangmanGame.UI
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            var wordListPath = Properties.Settings.Default.WordlistPath;
            var viewModel = new MainViewModel(new Hangman(new WordListReader().GetWordList(wordListPath), 11));
            DataContext = viewModel;

            viewModel.AttemptFailed += AttemptFailed;
            viewModel.NewGameStarted += NewGameStarted;
        }

        private static void ExistOnInvalidWordListPath(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show(Properties.Resources.WordlistNotFound);
                Environment.Exit(1);
            }
        }

        private void NewWordButtonPressed(object sender, RoutedEventArgs e)
        {
            KeyboardField.ResetLetterBoxes();
        }

        private void AttemptFailed(object sender, EventArgs e)
        {
            HangmanControl.ShowNextPart();
        }

        private void NewGameStarted(object sender, EventArgs e)
        {
            HangmanControl.HideAllParts();
        }
    }
}