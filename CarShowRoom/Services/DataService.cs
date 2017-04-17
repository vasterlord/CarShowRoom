using CarShowRoom.Model;
using System;

namespace CarShowRoom.Services
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem(); 
            item.ToolBarDescription = "Date and time:  ";
            callback(item, null);
        }
    }
}