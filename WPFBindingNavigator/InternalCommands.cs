using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BindingNavigator
{
    public class InternalCommands
    {
        internal InternalCommands()
        {
        }

        public RelayCommand NextItemCommand
        {
            get;
            internal set;
        }

        public RelayCommand PreviousItemCommand
        {
            get;
            internal set;
        }

        public RelayCommand FirstItemCommand
        {
            get;
            internal set;
        }

        public RelayCommand LastItemCommand
        {
            get;
            internal set;
        }

        public RelayCommand AddNewItemCommand
        {
            get;
            internal set;
        }

        public RelayCommand DeleteItemCommand
        {
            get;
            internal set;
        }

        internal void OnAddNewItemCanExecuteChanged(object sender, EventArgs e)
        {
            this.AddNewItemCommand.RaiseCanExecuteChanged();
        }

        internal void OnDeleteItemCanExecuteChanged(object sender, EventArgs e)
        {
            this.DeleteItemCommand.RaiseCanExecuteChanged();
        }
    }
}
