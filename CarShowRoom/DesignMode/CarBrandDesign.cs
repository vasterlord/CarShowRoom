using CarShowRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowRoom.DesignMode
{
    class CarBrandDesign
    {
        public void GetData(Action<CarBrand, Exception> callback)
        {
            var carBrandModel = new CarBrand();
            callback(carBrandModel, null);
        }
    }
}
