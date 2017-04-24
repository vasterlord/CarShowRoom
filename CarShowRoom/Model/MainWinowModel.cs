using GalaSoft.MvvmLight;
using System;
namespace CarShowRoom.Model
{
    public class MainWinowModel
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
        public MainWinowModel()
        {
            ToolBarDescription = "Date and time:  ";
        }
    }
}