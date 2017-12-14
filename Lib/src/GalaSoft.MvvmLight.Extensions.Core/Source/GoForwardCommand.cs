using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Command, which will navigate forward behind given <see cref="IRedirectViewModel"/>.
    /// </summary>
    public class GoForwardCommand : ICommand
    {
        private ICommand handler;
        private IRedirectViewModel _source;
        private int _steps;

        private void Execute()
        {
            _source.GoForward(_steps);
        }

        private bool CanExecute()
        {
            return _source.CanGoForward(_steps);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GoForwardCommand"/>.
        /// </summary>
        /// <param name="source">The view model.</param>
        /// <param name="steps">The number of steps in navigation stack.</param>
        public GoForwardCommand(IRedirectViewModel source, int steps = 1)
        {
            _source = source;
            _steps = steps;
            handler = new RelayCommand(Execute, CanExecute);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { handler.CanExecuteChanged += value; }
            remove { handler.CanExecuteChanged -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>True, if command can execute.</returns>
        public bool CanExecute(object parameter)
        {
            return handler.CanExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Execute(object parameter)
        {
            handler.Execute(parameter);
        }
    }
}
