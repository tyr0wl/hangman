using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HangmanGame.Extensions;
using HangmanGame.UI.Commands;
using HangmanGame.UI.Helper;

namespace HangmanGame.UI.Controls
{
    /// <summary>
    ///     Interaction logic for LetterBox.xaml
    /// </summary>
    public partial class LetterBox : UserControl
    {
        /// <summary>
        ///     Text inside the Letterbox.
        /// </summary>
        public static readonly DependencyProperty TextProperty = For<LetterBox>.Register(o => o.Text);

        /// <summary>
        ///     Command fired when Letterbox is pressed.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = For<LetterBox>.Register(o => o.Command);

        public LetterBox()
        {
            InitializeComponent();
            LetterButton.Command = new GenericRelayCommand<string>(LetterButtonCommand);
        }

        /// <summary>
        ///     Text inside the Letterbox.
        /// </summary>
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        ///     Command fired when Letterbox is pressed.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        private void LetterButtonCommand(string stringObject)
        {
            ButtonClicked();
            Command?.Execute(stringObject.ToChar());
        }

        private void ButtonClicked()
        {
            LetterButton.IsEnabled = false;
            LetterButton.Foreground = Brushes.Red;
        }

        /// <summary>
        ///     Resets the visual indication that the button was pressed.
        /// </summary>
        public void Reset()
        {
            LetterButton.IsEnabled = true;
            LetterButton.ClearValue(ForegroundProperty);
        }
    }
}