using System;
using System.Diagnostics;
using System.Windows.Input;

namespace HangmanGame.UI.Commands
{
    /// <summary>
    ///     Generic RelayCommand that that can execute any action it is passed and uses a type safe parameter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;

        private readonly Action<T> _execute;

        public GenericRelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public GenericRelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return TryCastToValueType(parameter) ? _canExecute(default) : _canExecute((T) parameter);
        }

        public void Execute(object parameter)
        {
            if (TryCastToValueType(parameter))
            {
                _execute(default);
            }
            else
            {
                _execute((T) parameter);
            }
        }

        private static bool TryCastToValueType(object parameter)
        {
            return parameter == null && typeof(T).IsValueType;
        }
    }
}