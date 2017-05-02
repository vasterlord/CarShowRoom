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
         
        public ICommand WindowLoaded { get; set; }
        public ICommand NewClick { get; set; }
        public ICommand SaveClick { get; set; }
        public ICommand DeleteClick { get; set; }
        public ICommand SearchPress { get; set; }
        public ICommand MaxPrice { get; set; }
        public int SelectedIndexValue { get; set; }
        public List<string> Options { get; set; }
        public List<string> ListGears { get; set; } 
        public List<string> BrandItems { get; set; }
        public List<KeyValuePair<string, double>> ChartCars { get; set; }
        public ICommand ChartCarsLoad { get; set; }

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
            NewClick = new RelayCommand(newClick);
            SaveClick = new RelayCommand(saveClick);
            DeleteClick = new RelayCommand(deleteClick);
            SearchPress = new RelayCommand(searchPress);
            MaxPrice = new RelayCommand(maxPrice);
            BrandItems = new List<string>();
            ChartCarsLoad = new RelayCommand(chartCarLoad);
            using (var context = new Context())
            {
                BrandItems = context.CarBrands.AsEnumerable().Select(x => x.Brand).ToList();
            }
            ListGears = new List<string>();
            ListGears.Add("mechanical");
            ListGears.Add("automatic");
            ListGears.Add("robotic");
            ListGears.Add("variative");
            Options = new List<string>();
            Options.Add("Brand");
            Options.Add("Country");
            Options.Add("Price");
            _findingOption = Options[0];
        }

        public void chartCarLoad()
        {
            using (var context = new Context())
            {
                onLoad();
                ChartCars = new List<KeyValuePair<string, double>>();
                var carData = context.Cars
                        .Join(context.CarBrands,
                            cars => cars.CarBrandId,
                            brands => brands.Id,
                            (cars, brands) => new { cars, brands })
                        .Select(x => new { carName = string.Concat(x.brands.Brand, " ", x.cars.Model), x.cars.Price }).ToList();
                foreach (var item in carData)
                {
                    ChartCars.Add(new KeyValuePair<string, double>(item.carName, item.Price));
                }
                this.RaisePropertyChanged(() => this.ChartCars);
            }
        }
         
        public void maxPrice()
        {
            using (var context = new Context())
            {
                if (_cars.Count == 0)
                {
                    LoadCars();
                }
                var max = _cars.Max(x => x.Price);
                _cars = new ObservableCollection<CarsShowing>(_cars.Where(r => r.Price == max) 
                .ToList());
                this.RaisePropertyChanged(() => this.Cars);
                if (_cars.Count > 0)
                {
                    SelectedIndexValue = 0;
                    SelectedCar = _cars[0];
                }
            }
        } 

        public void searchPress()
        {
            try
            {
            using (var context = new Context())
            {
                switch (_findingOption)
                {
                    case "Brand":
                        if (_cars.Count==0)
                        {
                            LoadCars();
                        }
                        _cars = new ObservableCollection<CarsShowing>(_cars.Where(r => r.Brand == FindingValue) 
                        .ToList());
                        this.RaisePropertyChanged(() => this.Cars);
                        if (_cars.Count > 0)
                        {
                            SelectedIndexValue = 0;
                            SelectedCar = _cars[0];
                        }
                        break;
                    case "Country":
                        if (_cars.Count == 0)
                        {
                            LoadCars();
                        }
                        _cars = new ObservableCollection<CarsShowing>(_cars.Where(r => r.Country == FindingValue) 
                        .ToList());
                        this.RaisePropertyChanged(() => this.Cars);
                        if (_cars.Count > 0)
                        {
                            SelectedIndexValue = 0;
                            SelectedCar = _cars[0];
                        }
                        break;
                    case "Price":
                        if (_cars.Count == 0)
                        {
                            LoadCars();
                        }
                        _cars = new ObservableCollection<CarsShowing>(_cars.Where(r => r.Price == Convert.ToDouble(FindingValue)) 
                        .ToList());
                        this.RaisePropertyChanged(() => this.Cars);
                        if (_cars.Count > 0)
                        {
                            SelectedIndexValue = 0;
                            SelectedCar = _cars[0];
                        }
                        break;
                }
            }
        }
            catch (FormatException f)
            {
                MessageBox.Show(f.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void newClick()
        {
            Cars.Add(new CarsShowing() { Id = Cars[Cars.Count - 1].Id + 1 });
            SelectedIndexValue = Cars.Count - 1;
            SelectedCar = _cars[_cars.Count - 1];
        }

        public void saveClick()
        {
            try
            {
                using (var context = new Context())
                {
                    ObservableCollection<Car> tempId = new ObservableCollection<Car>(context.Cars.Where(x => x.Id == SelectedCar.Id));
                    if (tempId.Count != 0)
                    {
                        int id = Convert.ToInt32(SelectedCar.Id);
                        var updatedCar = context.Cars.FirstOrDefault(car => car.Id == id);
                        List<string> idList = context.CarBrands
                        .Where(x => x.Brand == SelectedCar.Brand)
                        .AsEnumerable()
                        .Select(x => x.Id.ToString()).ToList();
                        int idUpdate = Convert.ToInt32(idList[0]);
                        context.Cars.Attach(updatedCar);
                        context.Entry(updatedCar).State = EntityState.Modified;
                        updatedCar.CarBrandId = idUpdate;
                        updatedCar.Model = SelectedCar.Model;
                        updatedCar.Load = SelectedCar.Load;
                        updatedCar.Axel = SelectedCar.Axel;
                        updatedCar.GearBox = SelectedCar.GearBox;
                        updatedCar.EngineCapacity = SelectedCar.EngineCapacity;
                        updatedCar.FuelPerHunderdKm = SelectedCar.FuelPerHunderdKm;
                        updatedCar.ProductionYear = SelectedCar.ProductionYear;
                        updatedCar.Price = SelectedCar.Price;
                        context.SaveChanges();
                    }
                    else
                    {
                        Car addingCar = new Car();
                        List<string> idList = context.CarBrands
                        .Where(x => x.Brand == SelectedCar.Brand)
                        .AsEnumerable()
                        .Select(x => x.Id.ToString()).ToList();
                        int id = Convert.ToInt32(idList[0]);
                        addingCar.Id = SelectedCar.Id;
                        addingCar.CarBrandId = id;
                        addingCar.Model = SelectedCar.Model;
                        addingCar.Load = SelectedCar.Load;
                        addingCar.Axel = SelectedCar.Axel;
                        addingCar.GearBox = SelectedCar.GearBox;
                        addingCar.EngineCapacity = SelectedCar.EngineCapacity;
                        addingCar.FuelPerHunderdKm = SelectedCar.FuelPerHunderdKm;
                        addingCar.ProductionYear = SelectedCar.ProductionYear;
                        addingCar.Price = SelectedCar.Price;
                        if (context.Clients.ToList().Count > 0)
                        {
                            foreach (var item in context.Cars)
                            {
                                item.Clients = new ObservableCollection<Client>(context.Clients.Where(s => s.CarId == item.Id));
                            }
                        }
                        context.Cars.Add(addingCar);
                        
                        context.SaveChanges();
                    }
                    LoadCars();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deleteClick()
        {
            using (var context = new Context())
            {
                ObservableCollection<Car> tempId = new ObservableCollection<Car>(context.Cars.Where(x => x.Id == SelectedCar.Id));
                if (tempId.Count != 0)
                {
                    int id = Convert.ToInt32(SelectedCar.Id);
                    var deletedCar = context.Cars.FirstOrDefault(car => car.Id == id);
                    context.Cars.Attach(deletedCar);
                    context.Cars.Remove(deletedCar);
                    context.SaveChanges();
                }
                LoadCars();
            }
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
                var tempCars = context.Cars.Include(client => client.Clients)
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
                              x.cars.GearBox,
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
                    GearBox = r.GearBox,
                    EngineCapacity = r.EngineCapacity,
                    FuelPerHunderdKm = r.FuelPerHunderdKm,
                    ProductionYear = r.ProductionYear,
                    Price = r.Price
                }).ToList());
                this.RaisePropertyChanged(() => this.Cars);
                this.RaisePropertyChanged(() => this.SelectedCar);
                SelectedIndexValue = 0; 
                SelectedCar = _cars[0];
            }
        }

        //List<string> idList = context.CarBrands
        //.Where(x => x.Brand == SelectedCar.Brand)
        //.AsEnumerable()
        //.Select(x => x.Id.ToString()).ToList();
        //int id = Convert.ToInt32(idList[0]);
        //var tempCars = context.Cars.Include(client => client.Clients)
        //  .Join(context.CarBrands,
        //        cars => cars.CarBrandId,
        //        brands => brands.Id,
        //        (cars, brands) => new { cars, brands })
        //  .Where(t => t.cars.CarBrandId == id)
        //  .OrderBy(x => x.cars.Id)
        //  .Select(x => new
        //  {
        //      x.cars.Id,
        //      x.brands.Brand,
        //      x.cars.Model,
        //      x.brands.CountryProducing,
        //      x.cars.Load,
        //      x.cars.Axel,
        //      x.cars.GearBox,
        //      x.cars.EngineCapacity,
        //      x.cars.FuelPerHunderdKm,
        //      x.cars.ProductionYear,
        //      x.cars.Price
        //  }).ToList(); 

    }
}