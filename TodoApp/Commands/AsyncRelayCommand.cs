using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TodoApp.Commands
{
    public class AsyncRelayCommand : ICommand
    {
        #region Constructor
        
        public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null) 
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region Fields

        private readonly Func<Task> _execute;
        private readonly Func<bool> ? _canExecute;
        private bool _isExecuting;

        #endregion

        #region Events

        public event EventHandler? CanExecuteChanged;

        #endregion

        #region Methods
        public bool CanExecute(object? parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async void Execute(object? parameter)
        {
           if(!CanExecute(parameter))
                return;

           _isExecuting = true;
            RaiseCanExecuteChanged();

            try
            {
                await _execute();
            }
            finally 
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
           CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
