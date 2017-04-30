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
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace CarShowRoom.ViewModel
{
    public class CarViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<CarBrand> _carBrands;
        private CarBrand _selectedCarBrand;
        private string _findingOption = string.Empty;
        private string _findingValue = string.Empty;

        public ICommand WindowLoaded { get; set; }
        public ICommand NewClick { get; set; }
        public ICommand SaveClick { get; set; }
        public ICommand DeleteClick { get; set; }
        public ICommand SearchPress { get; set; }
        public ICommand OptionChange { get; set; }
        public int SelectedIndexValue { get; set; }
        public List<string> Options { get; set; }

        public string FindingOption
        {
            get { return _findingOption; }
            set { _findingOption = value; this.RaisePropertyChanged("FindingOption"); }
        }

        public string FindingValue
        {
            get { return _findingValue; }
            set { _findingValue = value; this.RaisePropertyChanged("FindingValue"); }
        }

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
        public CarViewModel()
        {
            SelectedIndexValue = 0;
            ////WindowLoaded = new RelayCommand(onLoad);
            //NewClick = new RelayCommand(newClick);
            //SaveClick = new RelayCommand(saveClick);
            //DeleteClick = new RelayCommand(deleteClick);
            //SearchPress = new RelayCommand(searchPress);
            //LoadLogo = new RelayCommand(loadLogo);
            Options = new List<string>();
            Options.Add("Car name");
            Options.Add("Country");
            Options.Add("Max speed");
            Options.Add("Price");
            _findingOption = Options[0];
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
                SelectedIndexValue = 0;
                SelectedCarBrand = _carBrands[0];
            }
        } 

        //public void LoadCars()
        //{
        //    using (var context = new Context())
        //    { 
        //        _carBrands = context.Cars
        //                  .Join(context.CarBrands,
        //                        c => c.CarBrandId,
        //                        o => o.Id,
        //                        (c, o) => new { c, o })
        //                  //.Where(t => t.c.CountryProducingId == country.Id)
        //                  .OrderBy(x => x.c.Id)
        //                  .Select(x => new CarBrand { Brand = x.c.Id.ToString(), CarName = string.Concat(x.c.Model, x.o.Brand), x.o.CountryProducing, x.c.Load, x.c.Axel, x.c.Transmission, x.c.EngineCapacity, x.c.FuelPerHunderdKm, x.c.ProductionYear, x.c.Price }).ToList();
        //        this.RaisePropertyChanged(() => this.Cars);
        //        _cars.
        //        SelectedIndexValue = 0;
        //    }
        //} 
    }
}