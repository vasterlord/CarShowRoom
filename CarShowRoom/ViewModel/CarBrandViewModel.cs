using CarShowRoom.DataSource;
using CarShowRoom.Model;
using CarShowRoom.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarShowRoom.ViewModel
{

    public class CarBrandViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<CarBrand> _carBrands;
        private CarBrand _selectedCarBrand; 

        public ICommand DatagridAutoGenColumns { get; set; }
        public ICommand WindowLoaded { get; set; } 
         
        public ObservableCollection<CarBrand> CarBrands
        {
            get
            {
                return _carBrands;
            }
        }  

        public CarBrand SelectedCarBrand
        {
            get
            {
                return _selectedCarBrand;
            } 
            set
            {
                _selectedCarBrand = value;
                RaisePropertyChanged("SelectedCarBrand");
            }
        } 

        public CarBrandViewModel()
        {
            WindowLoaded = new RelayCommand(onLoad);
            DatagridAutoGenColumns = new RelayCommand<DataGridAutoGeneratingColumnEventArgs>(dataGrid_AutoGeneratingColumn); 
        }

        private void dataGrid_AutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        public void onLoad()
        {
            LoadCarBrand();
            if (_carBrands != null)
            {
                ConfigDataGrid();
            }
        } 
        /// <summary>
        /// Dont work!!!
        /// </summary>
        public void ConfigDataGrid()
        {
            CarBrandView carBrandView = new CarBrandView();
            //carBrandView.dataGrid.Columns[0].Visibility = Visibility.Collapsed;
            //carBrandView.dataGrid.Columns[4].Visibility = Visibility.Collapsed;
            //carBrandView.dataGrid.Columns[5].Visibility = Visibility.Collapsed;
            carBrandView.dataGrid.SelectedIndex = 0;
        } 
        public void LoadCarBrand()
        {
            using (var context = new Context())
            {
                _carBrands = new ObservableCollection<CarBrand>(context.CarBrands.Include(car => car.Cars).OrderBy(val => val.Id));
                this.RaisePropertyChanged(() => this.CarBrands);
            }
        }

        public void AddCarBrand()
        {

        }
    }
}