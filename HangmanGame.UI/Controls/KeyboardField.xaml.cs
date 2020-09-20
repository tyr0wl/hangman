using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HangmanGame.UI.Helper;

namespace HangmanGame.UI.Controls
{
    /// <summary>
    ///     Interaktionslogik für KeyboardField.xaml
    /// </summary>
    public partial class KeyboardField : UserControl
    {
        public static readonly DependencyProperty TemplateCommandProperty =
            For<KeyboardField>.Register(o => o.TemplateCommand);

        public KeyboardField()
        {
            InitializeComponent();
        }

        public ICommand TemplateCommand
        {
            get => (ICommand) GetValue(TemplateCommandProperty);
            set => SetValue(TemplateCommandProperty, value);
        }

        /// <summary>
        ///     Resets the visual indications that buttons were pressed.
        /// </summary>
        public void ResetLetterBoxes()
        {
            foreach (LetterBox letterBox in KeyboardGrid.Children)
            {
                letterBox.Reset();
            }
        }
    }
}