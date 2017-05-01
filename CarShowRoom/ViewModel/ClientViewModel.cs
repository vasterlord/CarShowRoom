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
    public class ClientViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<ClientShowing> _clients;
        private ClientShowing _selectedClient;
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
        public List<string> CarNameItems { get; set; }

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

        public ObservableCollection<ClientShowing> Clients
        {
            get
            {
                return _clients;
            }
        }

        public ClientShowing SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                _selectedClient = value;
                this.RaisePropertyChanged("SelectedClient");
            }
        } 

        public ClientViewModel()
        {
            SelectedIndexValue = 0;
            WindowLoaded = new RelayCommand(onLoad);
            //NewClick = new RelayCommand(newClick);
            //SaveClick = new RelayCommand(saveClick);
            //DeleteClick = new RelayCommand(deleteClick);
            //SearchPress = new RelayCommand(searchPress);
            CarNameItems = new List<string>();
            using (var context = new Context())
            {
                CarNameItems = context.Cars.Include(client => client.Clients)
                          .Join(context.CarBrands,
                                cars => cars.CarBrandId,
                                brands => brands.Id,
                                (cars, brands) => new { cars, brands })
                          // .Where(t => t.cars.CarBrandId==t.brands.Id)
                          .OrderBy(x => x.cars.Id)
                          .Select(x => string.Concat(x.brands.Brand, " ", x.cars.Model)).ToList();
            } 
            Options = new List<string>();
            Options.Add("Id");
            Options.Add("Car name");
            Options.Add("Full name");
            Options.Add("Buy price");
            _findingOption = Options[0];
        }

        public void onLoad()
        {
            LoadClients ();
        }

        /// <summary>
        /// Helped logic
        /// </summary>
        public void LoadClients()
        {
            using (var context = new Context())
            {
                var tempClient = from car in context.Cars
                                 join client in context.Clients on car.Id equals client.CarId
                                 join brand in context.CarBrands on car.CarBrandId equals brand.Id
                                 orderby client.Id
                                 select new
                                 {
                                     client.Id,
                                     client.FullName,
                                     carName = string.Concat(brand.Brand, " " , car.Model),
                                     client.HomeAddress,
                                     client.PhoneNumber,
                                     client.DateBuy,
                                     buyPrice = (car.Price + (car.Price * client.ServicePercent) / 100),
                                     client.ServicePercent
                                 };
                _clients = new ObservableCollection<ClientShowing>(tempClient.ToList().Select(r => new ClientShowing
                {
                    Id = r.Id,
                    FullName = r.FullName,
                    CarName = r.carName,  
                    HomeAddress = r.HomeAddress, 
                    PhoneNumber = r.PhoneNumber, 
                    DateBuy = r.DateBuy, 
                    BuyPrice = r.buyPrice, 
                    ServicePercent = r.ServicePercent
                }).ToList());
                this.RaisePropertyChanged(() => this.Clients);
                SelectedIndexValue = 0;
                SelectedClient = _clients[0];
            }
        }
    }
}