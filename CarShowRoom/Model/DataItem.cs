using GalaSoft.MvvmLight;
using System;
namespace CarShowRoom.Model
{
    public class DataItem
    {
        private string _toolBarDescription;
        public string ToolBarDescription
        {
            get
            {
                return _toolBarDescription;
            } 
            set
            {
                _toolBarDescription = value;
            }
        } 
        public DataItem()
        {
            ToolBarDescription = "Date and time:  ";
        }
    }
}