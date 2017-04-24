using CarShowRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowRoom.DesignMode
{
    class CarDesign
    {
        public void GetData(Action<Car, Exception> callback)
        {
            var carModel = new Car();
            callback(carModel, null);
        }
    }
}
