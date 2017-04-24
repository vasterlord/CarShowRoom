using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Model
{
    public class CarBrand : ObservableObject
    {
        private string _brand;
        private string _countryProducing; 
        private DateTime _foundationYear;

        public int Id { get; set; }

        [ConcurrencyCheck]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                Set<string>(() => this.Brand, ref _brand, value);
            }
        }

        [ConcurrencyCheck]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string CountryProducing
        {
            get
            {
                return _countryProducing;
            }
            set
            {
                Set<string>(() => this.CountryProducing, ref _countryProducing, value);
            }
        } 

        [ConcurrencyCheck]
        [StringLength(10, ErrorMessage = "The length of the date must be correctly")]
        public DateTime FoundationYear
        {
            get
            {
                return _foundationYear; 
            } 
            set
            {
                Set<DateTime>(() => this.FoundationYear, ref _foundationYear, value);
            }
        }

        public CarBrand()
        {
            Car = new ObservableCollection<Car>();
        } 

        public ObservableCollection<Car> Car { get; set; }

    }
}
