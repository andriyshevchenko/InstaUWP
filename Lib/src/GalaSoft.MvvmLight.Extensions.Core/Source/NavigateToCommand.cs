using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Command, which will navigate given <see cref="IRedirectViewModel"/> to a new view model.
    /// </summary>
    public class NavigateToCommand : ICommand
    {
        private ICommand handler;
        private IRedirectViewModel _source;
        private object _viewmodel;

        private void Execute()
        {
            _source.NavigateTo(_viewmodel);
        }

        private bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NavigateToCommand"/>.
        /// </summary>
        /// <param name="source">The source view model.</param>
        /// <param name="viewModel">The view model to navigate.</param>
        public NavigateToCommand(IRedirectViewModel source, object viewModel)
        {
            _source = source;
            _viewmodel = viewModel;
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
