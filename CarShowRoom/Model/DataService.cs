using System;

namespace CarShowRoom.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            DataItem item = new DataItem(); 
            item.ToolBarDescription = "Date and time:  ";
            callback(item, null);
        }
    }
}