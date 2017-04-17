using System;
using CarShowRoom.Model;
using CarShowRoom.Services;

namespace CarShowRoom.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data
            var item = new DataItem();
            item.ToolBarDescription = "Date and time:  ";
            callback(item, null);
        }
    }
}