using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Command, which will navigate back behind given <see cref="IRedirectViewModel"/>.
    /// </summary>
    public class GoBackCommand : ICommand
    {
        private ICommand handler;
        private IRedirectViewModel _source;
        private int _steps;

        private void Execute()
        {
            _source.GoBack(_steps);
        }

        private bool CanExecute()
        {
            return _source.CanGoBack(_steps);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GoBackCommand"/>.
        /// </summary>
        /// <param name="source">The view model.</param>
        /// <param name="steps">The number of steps in navigation stack.</param>
        public GoBackCommand(IRedirectViewModel source, int steps = 1)
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
