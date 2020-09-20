using System.Windows;

namespace HangmanGame.UI.Helper
{
    public readonly struct DependencyPropertyChangedEventArgs<T>
    {
        public DependencyPropertyChangedEventArgs(DependencyPropertyChangedEventArgs source)
        {
            Property = source.Property;
            OldValue = (T) source.OldValue;
            NewValue = (T) source.NewValue;
        }

        public DependencyProperty Property { get; }

        public T OldValue { get; }

        public T NewValue { get; }
    }
}