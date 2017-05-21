using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowRoom.Model
{
    public class CarsShowing : ObservableObject
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public double Load { get; set; }
        public string Axel { get; set; }
        public string GearBox { get; set; }
        public double EngineCapacity { get; set; }
        public double FuelPerHunderdKm { get; set; }
        public int ProductionYear { get; set; }
        public double Price { get; set; }

    }
}
