using System;
using System.Windows.Input;

namespace MVVM_Starter
{
   public class DelegateCommand : ICommand
    {

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        private readonly Action _exec;

        #region Construction

        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public DelegateCommand(Action execute) : this(execute, null)
        {

        }
        public DelegateCommand(Action execute, Predicate<object> CanExecute)
        {
            _exec = execute;
            _canExecute = CanExecute;

        }
        #endregion


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

       public void Execute()
        {
            if (_exec != null)
                _exec.Invoke();
            else
                _execute(null);
        }

        public void Execute(object parameter)
        {
            if(parameter == null && _exec != null)
            {
                _exec.Invoke();
                return;
            }
            _execute(parameter);
        }

        public void RaiseExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
