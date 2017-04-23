using GalaSoft.MvvmLight;
using System;
namespace CarShowRoom.Model
{
    public class MainWinowData
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
        public MainWinowData()
        {
            ToolBarDescription = "Date and time:  ";
        }
    }
}