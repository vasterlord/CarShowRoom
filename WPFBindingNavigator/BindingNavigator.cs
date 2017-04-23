using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.ComponentModel;

namespace BindingNavigator
{
    public class BindingNavigator : Control, INotifyPropertyChanged
    {
        private int currentItemIndex;
        private int itemsCount;

        static BindingNavigator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BindingNavigator), new FrameworkPropertyMetadata(typeof(BindingNavigator)));
        }

        public BindingNavigator()
        {
            this.Commands = new InternalCommands();
            this.Commands.PreviousItemCommand = new RelayCommand(this.GoToPreviousItem, this.CanGoToPreviousItem);
            this.Commands.NextItemCommand = new RelayCommand(this.GoToNextItem, this.CanGoToNextItem);
            this.Commands.FirstItemCommand = new RelayCommand(this.GoToFirstItem, this.CanGoToFirstItem);
            this.Commands.LastItemCommand = new RelayCommand(this.GoToLastItem, this.CanGoToLastItem);
            this.Commands.AddNewItemCommand = new RelayCommand(this.AddNewItem, this.CanAddNewItem);
            this.Commands.DeleteItemCommand = new RelayCommand(this.DeleteItem, this.CanDeleteItem);
        }

        private void OnItemsSourceChanged()
        {
            this.UpdateItemsCount();

            if (this.ItemsCount > 0)
            {
                this.CurrentItemIndex = 1;
            }
            else
            {
                this.CurrentItemIndex = 0;
            }
        }

        public int ItemsCount
        {
            get
            {
                return this.itemsCount;
            }
            private set
            {
                this.itemsCount = value;
                this.OnPropertyChanged("ItemsCount");
                this.RefreshNavigationCommands();
            }
        }

        public object CurrentItem
        {
            get
            {
                int i = 1;
                foreach (var item in this.ItemsSource)
                {
                    if (i == this.CurrentItemIndex)
                    {
                        return item;
                    }
                    i++;
                }

                return null;
            }
        }

        public int CurrentItemIndex
        {
            get
            {
                return this.currentItemIndex;
            }
            set
            {
                if (value > this.ItemsCount)
                {
                    return;
                }
                if (value < 1)
                {
                    return;
                }

                this.UnsafelyAlterItemIndex(value);
            }
        }

        public bool IsItemInContext
        {
            get
            {
                return this.CurrentItem != null;
            }
        }

        #region Dependency properties
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(BindingNavigator), new PropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var navigator = d as BindingNavigator;
            navigator.OnItemsSourceChanged();
        }

        public ICommand AddNewItemCommand
        {
            get { return (ICommand)GetValue(AddNewItemCommandProperty); }
            set { SetValue(AddNewItemCommandProperty, value); }
        }

        public static readonly DependencyProperty AddNewItemCommandProperty =
            DependencyProperty.Register("AddNewItemCommand", typeof(ICommand), typeof(BindingNavigator), new PropertyMetadata(null, new PropertyChangedCallback(OnAddNewItemCommandChanged)));

        private static void OnAddNewItemCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (e.NewValue as ICommand).CanExecuteChanged += (d as BindingNavigator).Commands.OnAddNewItemCanExecuteChanged;
            if (e.OldValue != null)
            {
                (e.OldValue as ICommand).CanExecuteChanged -= (d as BindingNavigator).Commands.OnAddNewItemCanExecuteChanged;
            }
        }

        public ICommand DeleteItemCommand
        {
            get { return (ICommand)GetValue(DeleteItemCommandProperty); }
            set { SetValue(DeleteItemCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteItemCommandProperty =
            DependencyProperty.Register("DeleteItemCommand", typeof(ICommand), typeof(BindingNavigator), new PropertyMetadata(null, new PropertyChangedCallback(OnDeleteItemCommandChanged)));

        private static void OnDeleteItemCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (e.NewValue as ICommand).CanExecuteChanged += (d as BindingNavigator).Commands.OnDeleteItemCanExecuteChanged;
            if (e.OldValue != null)
            {
                (e.OldValue as ICommand).CanExecuteChanged -= (d as BindingNavigator).Commands.OnDeleteItemCanExecuteChanged;
            }
        }

        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register("SaveCommand", typeof(ICommand), typeof(BindingNavigator), new UIPropertyMetadata(null));

        #endregion

        #region Item Navigation members

        public InternalCommands Commands
        {
            get;
            private set;
        }

        private bool CanGoToNextItem(object parameter)
        {
            return this.CurrentItemIndex < this.ItemsCount;
        }

        private void GoToNextItem(object parameter)
        {
            this.CurrentItemIndex++;
        }

        private bool CanGoToPreviousItem(object parameter)
        {
            return this.CurrentItemIndex > 1;
        }

        private void GoToPreviousItem(object parameter)
        {
            this.CurrentItemIndex--;
        }

        private bool CanGoToFirstItem(object parameter)
        {
            return this.ItemsCount > 0 && this.CurrentItemIndex > 1;
        }

        private void GoToFirstItem(object parameter)
        {
            this.CurrentItemIndex = 1;
        }

        private bool CanGoToLastItem(object parameter)
        {
            return this.ItemsCount > 0 && this.CurrentItemIndex < this.ItemsCount;
        }

        private void GoToLastItem(object parameter)
        {
            this.CurrentItemIndex = this.ItemsCount;
        }

        private bool CanAddNewItem(object parameter)
        {
            if (this.AddNewItemCommand != null)
            {
                return this.AddNewItemCommand.CanExecute(parameter);
            }
            else
            {
                return IsCollection(this.ItemsSource);
            }
        }

        private void AddNewItem(object parameter)
        {
            if (this.AddNewItemCommand != null)
            {
                this.AddNewItemCommand.Execute(parameter);
            }
            else
            {
                var list = this.ItemsSource as IList;
                var newItem = Activator.CreateInstance(GetEnumerableGenericArgument(this.ItemsSource.GetType()));
                list.Add(newItem);
            }
            this.UpdateItemsCount();
            this.GoToLastItem(null);
        }

        private bool CanDeleteItem(object parameter)
        {
            if (this.DeleteItemCommand != null)
            {
                return this.CurrentItemIndex > 0
                    && this.DeleteItemCommand.CanExecute(parameter);
            }
            else
            {
                return this.CurrentItemIndex > 0
                    && IsCollection(this.ItemsSource);
            }
        }

        private void DeleteItem(object parameter)
        {
            if (this.DeleteItemCommand != null)
            {
                this.DeleteItemCommand.Execute(parameter);
            }
            else
            {
                (this.ItemsSource as IList).Remove(this.CurrentItem);
            }
            if ( (this.CurrentItemIndex != 1 && this.ItemsCount > 1) || (this.CurrentItemIndex == 1 && this.ItemsCount == 1))
            {
                this.currentItemIndex--;
            }
            this.UpdateItemsCount();
            this.UnsafelyAlterItemIndex(this.currentItemIndex);
        }

        private void RefreshNavigationCommands()
        {
            this.Commands.PreviousItemCommand.RaiseCanExecuteChanged();
            this.Commands.NextItemCommand.RaiseCanExecuteChanged();
            this.Commands.FirstItemCommand.RaiseCanExecuteChanged();
            this.Commands.LastItemCommand.RaiseCanExecuteChanged();
            this.Commands.AddNewItemCommand.RaiseCanExecuteChanged();
            this.Commands.DeleteItemCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 
        #endregion

        private void UpdateItemsCount()
        {
            int i = 0;
            foreach (var item in this.ItemsSource)
            {
                i++;
            }

            this.ItemsCount = i;
        }

        private void UnsafelyAlterItemIndex(int index)
        {
            this.currentItemIndex = index;
            this.OnPropertyChanged("CurrentItemIndex");
            this.OnPropertyChanged("CurrentItem");
            this.OnPropertyChanged("IsItemInContext");
            this.RefreshNavigationCommands();
        }

        private static Type GetEnumerableGenericArgument(Type type)
        {
            if (type == typeof(string))
            {
                return null;
            }

            var enumerableGenericArgument = type.GetInterfaces()
                .Where(i => i.Name.Contains("IEnumerable") && i.IsGenericType)
                .FirstOrDefault();

            if (enumerableGenericArgument != null)
            {
                return enumerableGenericArgument.GetGenericArguments().FirstOrDefault();
            }

            return null;
        }

        private static bool IsCollection(IEnumerable obj)
        {
            return GetEnumerableGenericArgument(obj.GetType()) != null
                   && obj is IList;
        }
    }
}
