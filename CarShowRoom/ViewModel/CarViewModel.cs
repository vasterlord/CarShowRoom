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
        private ObservableCollection<CarsShowing> _cars;
        private CarsShowing _selectedCar;
        private string _findingOption = string.Empty;
        private string _findingValue = string.Empty;
         
        public object temp { get; set; }
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

        public ObservableCollection<CarsShowing> Cars
        {
            get
            {
                return _cars;
            }
        }

        public CarsShowing SelectedCar
        {
            get
            {
                return _selectedCar;
            }
            set
            {
                _selectedCar = value;
                this.RaisePropertyChanged("SelectedCar");
            }
        }
        public CarViewModel()
        {
            SelectedIndexValue = 0;
            WindowLoaded = new RelayCommand(onLoad);
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
            LoadCars();
        }

        /// <summary>
        /// Helped logic
        /// </summary>

        public void LoadCars()
        {
            using (var context = new Context())
            {
                var tempCars = context.Cars
                          .Join(context.CarBrands,
                                cars => cars.CarBrandId,
                                brands => brands.Id,
                                (cars, brands) => new { cars, brands })
                          // .Where(t => t.cars.CarBrandId==t.brands.Id)
                          .OrderBy(x => x.cars.Id)
                          .Select(x => new
                          {
                              x.cars.Id,
                              x.brands.Brand,
                              x.cars.Model,
                              x.brands.CountryProducing,
                              x.cars.Load,
                              x.cars.Axel,
                              x.cars.Transmission,
                              x.cars.EngineCapacity,
                              x.cars.FuelPerHunderdKm,
                              x.cars.ProductionYear,
                              x.cars.Price
                          }).ToList();
                _cars = new ObservableCollection<CarsShowing>(tempCars.ToList().Select(r => new CarsShowing
                {
                    Id = r.Id,
                    Brand = r.Brand,
                    Model = r.Model,
                    Country = r.CountryProducing,
                    Load = r.Load,
                    Axel = r.Axel,
                    Transmission = r.Transmission,
                    EngineCapacity = r.EngineCapacity,
                    FuelPerHunderdKm = r.FuelPerHunderdKm,
                    ProductionYear = r.ProductionYear,
                    Price = r.Price
                }).ToList());
                this.RaisePropertyChanged(() => this.Cars);
                //SelectedIndexValue = 0; 
                //SelectedCar = _cars[0];
            }
        }
    }
}