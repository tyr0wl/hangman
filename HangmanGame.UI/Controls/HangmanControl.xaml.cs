using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using HangmanGame.Extensions;

namespace HangmanGame.UI.Controls
{
    /// <summary>
    ///     Interaction logic for HangmanControl.xaml
    /// </summary>
    public partial class HangmanControl : UserControl
    {
        private Queue<Shape> _hangmanPartsQueue;
        private List<Shape> _hangmanStaticPartList;

        public HangmanControl()
        {
            InitializeComponent();

            PopulateStaticList();
            PopulateQueue();
            HideAllParts();
        }

        private void PopulateStaticList()
        {
            _hangmanStaticPartList = new List<Shape>(HangmanGrid.Children.Count);

            foreach (Shape part in HangmanGrid.Children)
            {
                _hangmanStaticPartList.Add(part);
            }
        }

        private void PopulateQueue()
        {
            _hangmanPartsQueue = new Queue<Shape>(_hangmanStaticPartList);
        }

        /// <summary>
        ///     Shows the Next Part of the Hangman guy.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when all Party are already visible.</exception>
        public void ShowNextPart()
        {
            if (_hangmanPartsQueue.IsEmpty())
            {
                throw new InvalidOperationException(Properties.Resources.AllPartsVisible);
            }

            var part = _hangmanPartsQueue.Dequeue();
            part.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Hides all Parts of the Hangman guy.
        /// </summary>
        public void HideAllParts()
        {
            foreach (var part in _hangmanStaticPartList)
            {
                part.Visibility = Visibility.Hidden;
            }

            PopulateQueue();
        }
    }
}