using System;
using System.Windows.Input;

namespace WpfUtility.Services
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _action;
        private readonly Predicate<T> _canExecute;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:WpfUtility.Services.RelayCommand`1" /> class.
        /// </summary>
        /// <param name="action">The execute method.</param>
        public RelayCommand(Action<T> action)
            : this(action, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}" /> class.
        /// </summary>
        /// <param name="action">The execute method.</param>
        /// <param name="canExecute">The can execute method.</param>
        public RelayCommand(Action<T> action, Predicate<T> canExecute)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can action in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.  If the command does not require data to be passed, this object can
        /// be set to null.
        /// </param>
        /// <returns><c>true</c> if this command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T) parameter) ?? true;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should action.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action((T) parameter);
        }
    }
}