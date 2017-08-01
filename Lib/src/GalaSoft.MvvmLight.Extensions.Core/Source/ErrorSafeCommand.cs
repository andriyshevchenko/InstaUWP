using Cactoos;
using Cactoos.Scalar;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// ICommand, which doesn't throw <see cref="Exception"/>.
    /// </summary>
    public class ErrorSafeCommand : ICommand, IAttempt
    {
        private ICommand _source;

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorSafeCommand"/>.
        /// </summary>
        /// <param name="source">The source command.</param>
        public ErrorSafeCommand(ICommand source)
        {
            _source = source;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorSafeCommand"/>.
        /// </summary>
        /// <param name="execute">Defines the method to be called when the command is invoked.</param>
        /// <param name="canExecute">Defines the method that determines whether the command can execute in its current state.</param>
        public ErrorSafeCommand(Action execute, Func<bool> canExecute)
            : this(new RelayCommand(execute, canExecute))
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorSafeCommand"/> that can always execute.
        /// </summary>
        /// <param name="execute">Defines the method to be called when the command is invoked.</param>
        public ErrorSafeCommand(Action execute)
          : this(new RelayCommand(execute))
        {

        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { _source.CanExecuteChanged += value; }
            remove { _source.CanExecuteChanged -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>True, if command can execute.</returns>
        public bool CanExecute(object parameter)
        {
            return _source.CanExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Execute(object parameter)
        {
            new ErrorSafeScalar<Unit>(
                fun(() => _source.Execute(parameter)),
                () => default(Unit)
            ).Value();
        }

        public bool HasErrors()
        {
            throw new NotImplementedException();
        }

        public Exception[] Errors()
        {
            throw new NotImplementedException();
        }
    }
}
