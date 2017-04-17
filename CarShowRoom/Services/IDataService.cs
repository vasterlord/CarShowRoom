using CarShowRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShowRoom.Services
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
