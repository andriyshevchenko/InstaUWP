using Cactoos;
using Cactoos.Scalar;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

using static System.Functional.Func;

namespace GalaSoft.MvvmLight.Extensions
{
    public class RetryCommand : ICommand, IAttempt
    {
        private static RetryScalar<Unit> _initial =
            new RetryScalar<Unit>(
                new FuncScalar<Unit>(fun(() => { })),
                1
            );

        private RetryScalar<Unit> _scalar = _initial;
        private ICommand _source;
        private int _attemps;

        public RetryCommand(Action execute, Func<bool> canExecute, int attempts)
            : this(new RelayCommand(execute, canExecute), attempts)
        {

        }

        public RetryCommand(Action execute, int attempts)
          : this(new RelayCommand(execute), attempts)
        {

        }

        public RetryCommand(ICommand source, int attemps)
        {
            _source = source;
            _attemps = attemps;
        }

        public event EventHandler CanExecuteChanged
        {
            add { _source.CanExecuteChanged += value; }
            remove { _source.CanExecuteChanged -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _source.CanExecute(parameter);
        }

        public Exception[] Errors()
        {
            return _scalar.Errors();
        }

        public void Execute(object parameter)
        {
            _scalar =
                new RetryScalar<Unit>(
                    new FuncScalar<Unit>(
                        fun(() => _source.Execute(parameter))
                    ),
                    _attemps
                );
            _scalar.Value();
        }

        public bool HasErrors()
        {
            return _scalar.HasErrors();
        }
    }
}
