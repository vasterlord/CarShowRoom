using GalaSoft.MvvmLight;
using CarShowRoom.Model;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Threading;
using System;
using GalaSoft.MvvmLight.Command;
using Xceed.Wpf.Toolkit;
using System.ComponentModel;

namespace CarShowRoom.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase , INotifyPropertyChanged
    {
        private string _toolBarDescriptionItem = string.Empty;
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                Set<string>(() => this.Name, ref name, value);
            }
        } 

        private string userName;
        public string UserName
        {
            get { return this.userName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.userName, value))
                {
                    this.userName = value;
                    Name = value;
                    //this.RaisePropertyChanged(); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }
        public string ToolBarDescriptionItem
        {
            get
            {
                return _toolBarDescriptionItem;
            }
            set
            {
                Set(ref _toolBarDescriptionItem, value);
               
            }
        }
        private string _toolBarValueItem = string.Empty;

        public string ToolBarValueItem
        {
            get
            {
                return _toolBarValueItem;
            }
            set
            {
                Set(ref _toolBarValueItem, value);
            }
        }

        public ICommand WindowLoaded {get; set; }
        public ICommand Changed { get; set; }

        public MainViewModel()
        {
            MainWinData item = new MainWinData();
            item.ToolBarDescription = "Date and time:  ";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            WindowLoaded = new RelayCommand(OnLoaded);
            Changed = new RelayCommand(OnChanged);
            ToolBarDescriptionItem = item.ToolBarDescription; 
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            ToolBarValueItem = DateTime.Now.ToString();
        }

        public void OnLoaded()
        {
            MessageBox.Show("Program loaded", "Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public void OnChanged()
        {
            MessageBox.Show(UserName, "Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            Name = "New User";
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}