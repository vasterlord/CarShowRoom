using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowRoom.Model
{
    public class ClientShowing : ObservableObject
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CarName { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string DateBuy { get; set; }
        public double? BuyPrice { get; set; }
        public double? ServicePercent { get; set; }
    }
}
