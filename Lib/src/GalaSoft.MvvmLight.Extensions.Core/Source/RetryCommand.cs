using Cactoos;
using Cactoos.Scalar;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Command, which will retry N times or throw <see cref="Exception"/>.
    /// </summary>
    public class RetryCommand : ICommand, IAttempt
    {
        private object _parameter;
        private RetryScalar<Unit> _scalar;
        private ICommand _source;

        /// <summary>
        /// Initializes a new instance of <see cref="RetryCommand"/>.
        /// </summary>
        /// <param name="execute">Defines the method to be called when the command is invoked.</param>
        /// <param name="canExecute">Defines the method that determines whether the command can execute in its current state.</param>
        /// <param name="attempts">The number of attemts to make before throw an <see cref="Exception"/>.</param>
        public RetryCommand(Action execute, Func<bool> canExecute, int attempts)
            : this(new RelayCommand(execute, canExecute), attempts)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="RetryCommand"/> that can always execute.
        /// </summary>
        /// <param name="execute">Defines the method to be called when the command is invoked.</param>
        /// <param name="attempts">The number of attemts to make before throw an <see cref="Exception"/>.</param>
        public RetryCommand(Action execute, int attempts)
          : this(new RelayCommand(execute), attempts)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="RetryCommand"/>.
        /// </summary>
        /// <param name="source">The source command.</param>
        /// <param name="attempts">The number of attemts to make before throw an <see cref="Exception"/>.</param>
        public RetryCommand(ICommand source, int attempts)
        {
            _source = source;
          
            _scalar =
              new RetryScalar<Unit>(
                  new FuncScalar<Unit>(
                      fun(() => _source.Execute(_parameter))
                  ),
                  attempts
              );
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
            _parameter = parameter;
            _scalar.Value();
        }


        /// <summary>
        /// Returns the errors.
        /// </summary>
        /// <returns>The errors.</returns>
        public bool HasErrors()
        {
            return _scalar.HasErrors();
        }

        /// <summary>
        /// Determines if attempt wasn't succesful.
        /// </summary>
        /// <returns>True, if attemt has errors.</returns>
        public Exception[] Errors()
        {
            return _scalar.Errors();
        }
    }
}
