using GalaSoft.MvvmLight;
using System;
namespace CarShowRoom.Model
{
    public class MainWinData
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
        public MainWinData()
        {
            ToolBarDescription = "Date and time:  ";
        }
    }
}