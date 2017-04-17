using GalaSoft.MvvmLight;
using CarShowRoom.Model;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Threading;
using System;
using GalaSoft.MvvmLight.Command;
using Xceed.Wpf.Toolkit;

namespace CarShowRoom.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private string _toolBarDescriptionItem = string.Empty;

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
 

        public MainViewModel(IDataService dataService)
        { 
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            WindowLoaded = new RelayCommand(OnLoaded);
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    ToolBarDescriptionItem = item.ToolBarDescription;
                });
        }

        public ICommand WindowLoaded
        {
            get; private set;
        }

        private void OnLoaded()
        {
            MessageBox.Show("Program loaded", "Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ToolBarValueItem = DateTime.Now.ToString();
        }
         
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}