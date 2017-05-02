using GalaSoft.MvvmLight;
using CarShowRoom.Model;
using CarShowRoom.View;
using System.Windows.Input;
using System.Windows.Threading;
using System;
using GalaSoft.MvvmLight.Command;
using Xceed.Wpf.Toolkit;
using System.ComponentModel;
using System.Diagnostics;

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
        public ICommand CarBrandLoad { get; set; }
        public ICommand CarBrandReportLoad { get; set; }
        public ICommand CarReportLoad { get; set; }
        public ICommand CarLoad { get; set; }
        public ICommand ClientLoad { get; set; }
        public ICommand ClientReportLoad { get; set; }

        public MainViewModel()
        {
            MainWinowModel item = new MainWinowModel();
            item.ToolBarDescription = "Date and time:  ";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            ToolBarDescriptionItem = item.ToolBarDescription;   

            WindowLoaded = new RelayCommand(OnLoaded);
            CarBrandLoad = new RelayCommand(CarBrandStart); 
            CarBrandReportLoad = new RelayCommand(CarBrandReportStart); 
            CarReportLoad = new RelayCommand(CarReportStart); 
            ClientReportLoad = new RelayCommand(ClientReportStart);
            ClientLoad = new RelayCommand(ClientStart);
            CarLoad = new RelayCommand(CarStart);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            ToolBarValueItem = DateTime.Now.ToString();
        }

        public void OnLoaded()
        {
            Debug.Print("Program loaded");
        }

        public void CarBrandStart()
        {
            CarBrandView carBrandView = new CarBrandView();
            carBrandView.ShowDialog();
        }
        public void CarBrandReportStart()
        {
            BrandsReportView brandsReportView = new BrandsReportView();
            brandsReportView.ShowDialog();
        }
        public void CarReportStart()
        {
            CarReportView carReportView = new CarReportView();
            carReportView.ShowDialog();
        }
        public void ClientReportStart()
        {
            ClientReportView clientReportView = new ClientReportView();
            clientReportView.ShowDialog();
        }
        public void CarStart()
        {
            CarView carView = new CarView();
            carView.ShowDialog();
        }
        public void ClientStart()
        {
            ClientView clientView = new ClientView();
            clientView.ShowDialog();
        }


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}