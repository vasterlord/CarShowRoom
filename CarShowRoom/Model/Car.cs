using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Model
{
    public class Car: ObservableObject
    {
        private string _model;
        private double _load;
        private string _axel;
        private string _transmission;
        private double _engineСapacity;
        private double _fuelPerHundredKm;
        private int _productionYear;
        private double _price;
        private byte[] _photo;

        public int Id { get; set; }
        public int CarBrandId { get; set; }

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                Set<string>(() => this.Model, ref _model, value);
            }
        } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        public double Load
        {
            get
            {
                return _load;
            }
            set
            {
                Set<double>(() => this.Load, ref _load, value);
            }
        }

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string Axel
        {
            get
            {
                return _axel;
            }
            set
            {
                Set<string>(() => this.Axel, ref _axel, value);
            }
        } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string Transmission
        {
            get
            {
                return _transmission;
            }
            set
            {
                Set<string>(() => this.Transmission, ref _transmission, value);
            }
        }

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        public double EngineCapacity
        {
            get
            {
                return _engineСapacity;
            }
            set
            {
                Set<double>(() => this.EngineCapacity, ref _engineСapacity, value);
            }
        } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        public double FuelPerHunderdKm
        {
            get
            {
                return _fuelPerHundredKm;
            }
            set
            {
                Set<double>(() => this.FuelPerHunderdKm, ref _fuelPerHundredKm, value);
            }
        }

        [ConcurrencyCheck]
        [StringLength(4, ErrorMessage = "The length of the date must be correctly")]
        public int ProductionYear
        {
            get
            {
                return _productionYear;
            }
            set
            {
                Set<int>(() => this.ProductionYear, ref _productionYear, value);
            }
        } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                Set<double>(() => this.Price, ref _price, value);
            }
        } 

        [ConcurrencyCheck]
        public byte[] Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                Set(ref _photo, value);
            }
        } 
         
        public CarBrand CarBrands { get; set; }

        public Car()
        {
            Clients = new ObservableCollection<Client>();
        } 

        public ObservableCollection<Client> Clients { get; set; }
    }
}
