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
        public ICommand CalcPrice { get; set; }
        public int SelectedIndexValue { get; set; }
        public List<string> Options { get; set; }
        public List<string> CarNameItems { get; set; }
        public List<KeyValuePair<string, double?>> ChartClients { get; set; }
        public ICommand ChartClientsLoad { get; set; }

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
            NewClick = new RelayCommand(newClick);
            SaveClick = new RelayCommand(saveClick);
            DeleteClick = new RelayCommand(deleteClick);
            SearchPress = new RelayCommand(searchPress);
            CalcPrice = new RelayCommand(calcPrice);
            MaxPrice = new RelayCommand(maxPrice);
            ChartClientsLoad = new RelayCommand(chartClientsLoad);
            CarNameItems = new List<string>();
            using (var context = new Context())
            {
                CarNameItems = context.Cars.Include(client => client.Clients)
                          .Join(context.CarBrands,
                                cars => cars.CarBrandId,
                                brands => brands.Id,
                                (cars, brands) => new { cars, brands })
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

        public void chartClientsLoad()
        {
            using (var context = new Context())
            {
                onLoad();
                ChartClients = new List<KeyValuePair<string, double?>>();
                var clientData = context.Clients.ToList();
                foreach (var item in clientData)
                {
                    ChartClients.Add(new KeyValuePair<string, double?>(item.FullName, item.BuyPrice));
                }
                this.RaisePropertyChanged(() => this.ChartClients);
            }
        } 

        public void maxPrice()
        {
            using (var context = new Context())
            {
                if (_clients.Count == 0)
                {
                    LoadClients();
                }
                var max = _clients.Max(x => x.BuyPrice);
                _clients = new ObservableCollection<ClientShowing>(_clients.Where(r => r.BuyPrice == max)
                .ToList());
                this.RaisePropertyChanged(() => this.Clients);
                if (_clients.Count > 0)
                {
                    SelectedIndexValue = 0;
                    SelectedClient = _clients[0];
                }
            }
        } 

        public void calcPrice()
        {
            using (var context = new Context())
            {
                List<string> carPrice = context.Cars
                        .Join(context.CarBrands,
                            cars => cars.CarBrandId,
                            brands => brands.Id,
                            (cars, brands) => new { cars, brands })
                        .Where(t => string.Concat(t.brands.Brand, " ", t.cars.Model) == SelectedClient.CarName)
                        .AsEnumerable()
                        .Select(x => x.cars.Price.ToString())
                        .ToList();
                double price = Convert.ToDouble(carPrice[0]);
                if (SelectedClient.ServicePercent == 0 || SelectedClient.ServicePercent==null || SelectedClient.ServicePercent < 0)
                {
                    MessageBox.Show("Please, input correct service percent! Calculation done with default percent value 5");
                    SelectedClient.ServicePercent = 5;
                }
                SelectedClient.BuyPrice = (price + (price * SelectedClient.ServicePercent) / 100);
                this.RaisePropertyChanged(() => this.Clients);
                this.RaisePropertyChanged(() => this.SelectedClient);
            }
        } 

        public void onLoad()
        {
            LoadClients ();
        } 

        public void newClick()
        {
            Clients.Add(new ClientShowing() { Id = Clients[Clients.Count - 1].Id + 1 });
            SelectedIndexValue = Clients.Count - 1;
            SelectedClient = _clients[_clients.Count - 1];
        } 

        public void deleteClick()
        {
            using (var context = new Context())
            {
                ObservableCollection<Client> tempId = new ObservableCollection<Client>(context.Clients.Where(x => x.Id == SelectedClient.Id));
                if (tempId.Count != 0)
                {
                    int id = Convert.ToInt32(SelectedClient.Id);
                    var deletedClient = context.Clients.FirstOrDefault(client => client.Id == id);
                    context.Clients.Attach(deletedClient);
                    context.Clients.Remove(deletedClient);
                    context.SaveChanges();
                }
                LoadClients(); 
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
                        case "Id":
                            if (_clients.Count == 0)
                            {
                                LoadClients();
                            }
                            _clients = new ObservableCollection<ClientShowing>(_clients.Where(r => r.Id == Convert.ToInt32(FindingValue))
                           .ToList());
                            this.RaisePropertyChanged(() => this.Clients);
                            if (_clients.Count > 0)
                            {
                                SelectedIndexValue = 0;
                                SelectedClient = _clients[0];
                            }
                            break;
                        case "Car name":
                            if (_clients.Count == 0)
                            {
                                LoadClients();
                            }
                            _clients = new ObservableCollection<ClientShowing>(_clients.Where(r => r.CarName == FindingValue)
                           .ToList());
                            this.RaisePropertyChanged(() => this.Clients);
                            if (_clients.Count > 0)
                            {
                                SelectedIndexValue = 0;
                                SelectedClient = _clients[0];
                            }
                            break;
                        case "Full name":
                            if (_clients.Count == 0)
                            {
                                LoadClients();
                            }
                            _clients = new ObservableCollection<ClientShowing>(_clients.Where(r => r.FullName == FindingValue)
                           .ToList());
                            this.RaisePropertyChanged(() => this.Clients);
                            if (_clients.Count > 0)
                            {
                                SelectedIndexValue = 0;
                                SelectedClient = _clients[0];
                            }
                            break;
                        case "Buy price":
                            if (_clients.Count == 0)
                            {
                                LoadClients();
                            }
                            _clients = new ObservableCollection<ClientShowing>(_clients.Where(r => r.BuyPrice == Convert.ToDouble(FindingValue))
                           .ToList());
                            this.RaisePropertyChanged(() => this.Clients);
                            if (_clients.Count > 0)
                            {
                                SelectedIndexValue = 0;
                                SelectedClient = _clients[0];
                            }
                            break;
                    }
                }
            }
            catch (FormatException f)
            {
                MessageBox.Show(f.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void saveClick()
        {
            try
            {
                using (var context = new Context())
                {
                    ObservableCollection<Client> tempId = new ObservableCollection<Client>(context.Clients.Where(x => x.Id == SelectedClient.Id));
                    if (tempId.Count != 0)
                    {
                        int id = Convert.ToInt32(SelectedClient.Id);
                        var updatedClient = context.Clients.FirstOrDefault(client => client.Id == id);
                        List<string> idList = context.Cars
                        .Join(context.CarBrands,
                            cars => cars.CarBrandId,
                            brands => brands.Id,
                            (cars, brands) => new { cars, brands })
                        .Where(t => string.Concat(t.brands.Brand, " ", t.cars.Model) == SelectedClient.CarName)
                        .AsEnumerable()
                        .Select(x => x.cars.Id.ToString())
                        .ToList();
                        int idUpdate = Convert.ToInt32(idList[0]);
                        context.Clients.Attach(updatedClient);
                        context.Entry(updatedClient).State = EntityState.Modified;
                        updatedClient.CarId = idUpdate;
                        updatedClient.FullName = SelectedClient.FullName;
                        updatedClient.HomeAddress = SelectedClient.HomeAddress;
                        updatedClient.PhoneNumber = SelectedClient.PhoneNumber;
                        updatedClient.DateBuy = SelectedClient.DateBuy;
                        updatedClient.BuyPrice = SelectedClient.BuyPrice;
                        updatedClient.ServicePercent = SelectedClient.ServicePercent;
                        context.SaveChanges();
                    }
                    else
                    {
                        Client addingClient = new Client();
                        List<string> idList = context.Cars
                        .Join(context.CarBrands,
                            cars => cars.CarBrandId,
                            brands => brands.Id,
                            (cars, brands) => new { cars, brands })
                        .Where(t => string.Concat(t.brands.Brand, " ", t.cars.Model) == SelectedClient.CarName)
                        .AsEnumerable()
                        .Select(x => x.cars.Id.ToString())
                        .ToList();
                        int id = Convert.ToInt32(idList[0]);
                        addingClient.Id = SelectedClient.Id;
                        addingClient.CarId = id;
                        addingClient.FullName = SelectedClient.FullName;
                        addingClient.HomeAddress = SelectedClient.HomeAddress;
                        addingClient.PhoneNumber = SelectedClient.PhoneNumber;
                        addingClient.DateBuy = SelectedClient.DateBuy;
                        addingClient.BuyPrice = SelectedClient.BuyPrice;
                        addingClient.ServicePercent = SelectedClient.ServicePercent;
                        context.Clients.Add(addingClient);
                        context.SaveChanges();
                    }
                    LoadClients();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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