using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Model
{
    public class Client : ObservableObject
    {
        private string _fullName;
        private string _homeAddress;
        private string _phoneNumber;
        private DateTime _dateBuy;
        private double _buyPrice;

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
        public DateTime DateBuy
        {
            get
            {
                return _dateBuy;
            }
            set
            {
                Set<DateTime>(() => this.DateBuy, ref _dateBuy, value);
            }
        } 

        [ConcurrencyCheck]
        [Required(ErrorMessage = "Field can't be null")]
        public double BuyPrice
        {
            get
            {
                return _buyPrice;
            }
            set
            {
                Set<double>(() => this.BuyPrice, ref _buyPrice, value);
            }
        } 
         
        public Car Cars { get; set; }
    }
}
