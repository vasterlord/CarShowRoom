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
using System.Windows.Data;
namespace CarShowRoom.ViewModel
{

    public class CarBrandViewModel : ViewModelBase, INotifyPropertyChanged
    { 
        //<intr:Interaction.Triggers>
        //        <intr:EventTrigger EventName = "AutoGeneratingColumn" >
        //            < cmd:EventToCommand Command = "{Binding Auto}"
        //                                PassEventArgsToCommand="True"/>
        //        </intr:EventTrigger>
        //    </intr:Interaction.Triggers>  

        public ICommand WindowLoaded { get; set; }

        public ObservableCollection<CarBrand> _carBrands;
        private CarBrand _selectedCarBrand;  

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
                this.RaisePropertyChanged("SelectedCarBrand");
            }
        } 

        public CarBrandViewModel()
        {
            WindowLoaded = new RelayCommand(onLoad);
        }

        public void onLoad()
        {
            LoadCarBrand();
        } 
        /// <summary>
        /// Helped logic
        /// </summary>
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