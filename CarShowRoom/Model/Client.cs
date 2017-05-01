using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel; 

namespace CarShowRoom.Model
{
    public class Client : ObservableObject
    {
        private string _fullName;
        private string _homeAddress;
        private string _phoneNumber;
        private string _dateBuy;
        private double? _buyPrice;
        private double? _servicePercent;

        public int Id { get; set; }
        public int CarId { get; set; } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                Set<string>(() => this.FullName, ref _fullName, value);
            }
        }
        
        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string HomeAddress
        {
            get
            {
                return _homeAddress;
            }
            set
            {
                Set<string>(() => this.HomeAddress, ref _homeAddress, value);
            }
        } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                Set<string>(() => this.PhoneNumber, ref _phoneNumber, value);
            }
        } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        [StringLength(10, ErrorMessage = "The length of the string must be 10 characters")] 
        [DefaultValue("dd/MM/YY")]
        public string DateBuy
        {
            get
            {
                return _dateBuy;
            }
            set
            {
                Set<string>(() => this.DateBuy, ref _dateBuy, value);
            }
        } 

        [ConcurrencyCheck]
        public double? BuyPrice
        {
            get
            {
                return _buyPrice;
            }
            set
            {
                Set<double?>(() => this.BuyPrice, ref _buyPrice, value);
            }
        }

        [ConcurrencyCheck]  
        [DefaultValue(5)]
        public double? ServicePercent
        {
            get
            {
                return _servicePercent;
            }
            set
            {
                Set<double?>(() => this.ServicePercent, ref _servicePercent, value);
            }
        } 

        public Car Cars { get; set; }
    }
}
