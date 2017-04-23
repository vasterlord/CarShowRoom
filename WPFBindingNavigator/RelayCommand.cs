using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BindingNavigator
{
    public class RelayCommand : ICommand
    {
        private Action<object> relayedMethod;
        private Func<object, bool> relayedCanExecute;

        public RelayCommand(Action<object> relayedMethod)
        {
            this.relayedMethod = relayedMethod;
        }

        public RelayCommand(Action<object> relayedMethod, Func<object, bool> relayedCanExecute)
            : this(relayedMethod)
        {
            this.relayedCanExecute = relayedCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.relayedMethod == null)
            {
                return false;
            }

            if (this.relayedCanExecute != null)
            {
                return this.relayedCanExecute.Invoke(parameter);
            }

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            if (this.relayedMethod != null)
            {
                this.relayedMethod.Invoke(parameter);
            }
        }
    }
}
