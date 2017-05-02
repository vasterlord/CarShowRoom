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
using System.Windows.Media.Imaging;

namespace CarShowRoom.ViewModel
{
    public class CarBrandViewModel : ViewModelBase, INotifyPropertyChanged
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
        public ICommand LoadLogo { get; set; }
        public ICommand ChartBrandLoad { get; set; }
        public int SelectedIndexValue { get; set; } 
        public List<string> Options { get; set; } 
        public BitmapImage ImageSource { get; set; } 
        public string _imageToDatabase { get; set; }
        public List<KeyValuePair<string, int?>> ChartBands { get; set; }

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

        public CarBrandViewModel()
        {
            SelectedIndexValue = 0;
            WindowLoaded = new RelayCommand(onLoad);
            NewClick = new RelayCommand(newClick);
            SaveClick = new RelayCommand(saveClick); 
            DeleteClick = new RelayCommand(deleteClick);  
            SearchPress = new RelayCommand(searchPress);  
            LoadLogo = new RelayCommand(loadLogo); 
            ChartBrandLoad = new RelayCommand(chartBrandLoad);
            Options = new List<string>();
            Options.Add(nameof(SelectedCarBrand.Id).ToString());
            Options.Add(nameof(SelectedCarBrand.Brand));
            Options.Add(nameof(SelectedCarBrand.CountryProducing));
            Options.Add(nameof(SelectedCarBrand.FoundationYear));
            _findingOption = Options[0];
        }

        public void onLoad()
        {
            LoadCarBrand();
        }

        public void chartBrandLoad()
        {
            using (var context = new Context())
            {
                onLoad();
                ChartBands = new List<KeyValuePair<string, int?>>();
                foreach (var item in _carBrands)
                {
                    ChartBands.Add(new KeyValuePair<string, int?>(item.Brand, item.FoundationYear));
                }
                this.RaisePropertyChanged(()=>this.ChartBands);
            }
        } 

        public void searchPress()
        {
         using (var context = new Context())
           {
                switch (_findingOption)
            {
                case "Id":
                        _carBrands = new ObservableCollection<CarBrand>(context.CarBrands.Include(car => car.Cars).Where(x => x.Id.ToString().Equals(_findingValue.ToString())));
                        this.RaisePropertyChanged(() => this.CarBrands);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand.Cars);
                        if (_carBrands.Count > 0)
                        {
                            SelectedIndexValue = 0;
                            SelectedCarBrand = _carBrands[0];
                        }
                    break;
                case "Brand":
                        _carBrands = new ObservableCollection<CarBrand>(context.CarBrands.Include(car => car.Cars).Where(x => x.Brand.ToLower().Contains(_findingValue.ToLower())));
                        this.RaisePropertyChanged(() => this.CarBrands);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand.Cars);
                        if (_carBrands.Count > 0)
                        {
                            SelectedIndexValue = 0;
                            SelectedCarBrand = _carBrands[0];
                        }
                    break;
                case "CountryProducing":
                        _carBrands = new ObservableCollection<CarBrand>(context.CarBrands.Include(car => car.Cars).Where(x => x.CountryProducing.ToLower().Contains(_findingValue.ToLower())));
                        this.RaisePropertyChanged(() => this.CarBrands);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand.Cars);
                        if (_carBrands.Count > 0)
                        {
                            SelectedIndexValue = 0;
                            SelectedCarBrand = _carBrands[0];
                        }
                    break;
                case "FoundationYear":
                        _carBrands = new ObservableCollection<CarBrand>(context.CarBrands.Include(car => car.Cars).Where(x => x.FoundationYear.ToString().Equals(_findingValue.ToString())));
                        this.RaisePropertyChanged(() => this.CarBrands);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand);
                        this.RaisePropertyChanged(() => this.SelectedCarBrand.Cars);
                        if (_carBrands.Count > 0)
                        {
                            SelectedIndexValue = 0;
                            SelectedCarBrand = _carBrands[0];
                        }
                    break;
            }
          }
        }
        public void newClick()
        {

            CarBrands.Add(new CarBrand() { Id = CarBrands[CarBrands.Count - 1].Id + 1 });
            SelectedIndexValue = CarBrands.Count - 1;
            SelectedCarBrand = _carBrands[_carBrands.Count - 1];
        } 

        public void saveClick()
        {
            try
            {
                using (var context = new Context())
                { 
                    ObservableCollection<CarBrand> tempId = new ObservableCollection<CarBrand>(context.CarBrands.Where(x => x.Id == SelectedCarBrand.Id));
                    if (tempId.Count != 0)
                    {
                        int id = Convert.ToInt32(SelectedCarBrand.Id);
                        var updatedBrand = context.CarBrands.FirstOrDefault(brand => brand.Id == id);
                        context.CarBrands.Attach(updatedBrand);
                        context.Entry(updatedBrand).State = EntityState.Modified;
                        updatedBrand.Brand = SelectedCarBrand.Brand;
                        updatedBrand.CountryProducing = SelectedCarBrand.CountryProducing;
                        updatedBrand.FoundationYear = SelectedCarBrand.FoundationYear;
                        updatedBrand.Logo = SelectedCarBrand.Logo;
                        context.SaveChanges();
                    }
                    else
                    {
                        loadLogo();
                        context.CarBrands.Add(SelectedCarBrand);
                        foreach (var item in context.CarBrands)
                        {
                            item.Cars = new ObservableCollection<Car>(context.Cars.Where(s => s.CarBrandId == item.Id));
                        }
                        context.SaveChanges();
                    }
                    LoadCarBrand();
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
                ObservableCollection<CarBrand> tempId = new ObservableCollection<CarBrand>(context.CarBrands.Where(x => x.Id == SelectedCarBrand.Id));
                if (tempId.Count != 0)
                {
                    int id = Convert.ToInt32(SelectedCarBrand.Id);
                    var deletedBrand = context.CarBrands.FirstOrDefault(brand => brand.Id == id);
                    context.CarBrands.Attach(deletedBrand);
                    context.CarBrands.Remove(deletedBrand);
                    context.SaveChanges();
                }
                LoadCarBrand();
            }
        }

        public void loadLogo()
        {
            _imageToDatabase = null;
            OpenFileDialog oDialog = new OpenFileDialog();
            oDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            Nullable<bool> result = oDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string fileName = oDialog.FileName;
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.UriSource = new Uri(fileName);
                    bi.DecodePixelHeight = 600;
                    bi.DecodePixelWidth = 600; 
                    bi.EndInit();
                    _imageToDatabase = oDialog.FileName;
                    if (_imageToDatabase != null)
                    {
                        SelectedCarBrand.Logo = GetPhoto(_imageToDatabase);
                    }
                    else
                    {
                        SelectedCarBrand.Logo = null;
                    }
                }
                catch
                {
                    MessageBox.Show("Сan not open the selected file",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
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

        public byte[] GetPhoto(string filePath)
        {
            byte[] photo = null;
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(stream))
            {
               photo = reader.ReadBytes((int)stream.Length);
            }
            return photo;
        }

        //private string _filteringOption = string.Empty;
        //private string _filteringValue = string.Empty;
        //private string _operation = string.Empty;
        //public List<string> Operations { get; set; }
        //public void filterClick()
        //{
        //    _carBrands = new ObservableCollection<CarBrand>(CarBrands.Where(x => string.Compare(x.Id.ToString(), "1") == 0));
        //    _carBrands = new ObservableCollection<CarBrand>(CarBrands.Where(x => string.Compare(x.Brand, "Car") > 0));
        //    _carBrands = new ObservableCollection<CarBrand>(CarBrands.Where(x => string.Compare(x.Brand, "Car") < 0));
        //    _carBrands = new ObservableCollection<CarBrand>(CarBrands.Where(x => x.Brand.Contains("1")));
        //    _carBrands = new ObservableCollection<CarBrand>(CarBrands.Where(x => !x.Brand.Contains("1")));
        //}
        //public string FilteringOption
        //{
        //    get { return _filteringOption; }
        //    set { _filteringOption = value; this.RaisePropertyChanged("FilteringOption"); }
        //}

        //public string FilteringValue
        //{
        //    get { return _filteringValue; }
        //    set { _filteringValue = value; this.RaisePropertyChanged("FilteringValue"); }
        //}

        //public string Operation
        //{
        //    get { return _operation; }
        //    set { _operation = value; this.RaisePropertyChanged("Operation"); }
        //}
        //public ICommand FilterClick { get; set; }
        //Operations = new List<string>();
        //    Operations.Add("=");
        //    Operations.Add(">");
        //    Operations.Add("<");
        //    Operations.Add("IN");
        //    Operations.Add("NOT IN");  
        //         FilterClick = new RelayCommand(filterClick);
        //_filteringOption = Options[0];
        //    _operation = Operations[0]; 

    }
}