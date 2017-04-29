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
        private int? _foundationYear;
        private byte[] _logo;

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
        [Required(ErrorMessage = "Field can't be null")]
        [Range(1700, 2025, ErrorMessage = "Incorrect year")]
        public int? FoundationYear
        {
            get
            {
                return _foundationYear; 
            } 
            set
            {
                Set<int?>(() => this.FoundationYear, ref _foundationYear, value);
            }
        }

        [ConcurrencyCheck]
        public byte[] Logo
        {
            get
            {
                return _logo;
            }
            set
            {
                Set(ref _logo, value);
            }
        }

        public CarBrand()
        {
            Cars = new ObservableCollection<Car>();
        } 

        public ObservableCollection<Car> Cars { get; set; }

    }
}
