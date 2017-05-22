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
        private string _gearBox;
        private double _engineСapacity;
        private double _fuelPerHundredKm;
        private int? _productionYear;
        private double _price;

        public int Id { get; set; }
        public int CarBrandId { get; set; }

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
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
        public string GearBox
        {
            get
            {
                return _gearBox;
            }
            set
            {
                Set<string>(() => this.GearBox, ref _gearBox, value);
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
        [Required(ErrorMessage = "Field can't be null")]
        [Range(1700, 2025, ErrorMessage = "Incorrect year")]
        public int? ProductionYear
        {
            get
            {
                return _productionYear;
            }
            set
            {
                Set<int?>(() => this.ProductionYear, ref _productionYear, value);
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
         
        public CarBrand CarBrands { get; set; }

        public Car()
        {
            Clients = new ObservableCollection<Client>();
        } 

        public ObservableCollection<Client> Clients { get; set; }
    }
}
