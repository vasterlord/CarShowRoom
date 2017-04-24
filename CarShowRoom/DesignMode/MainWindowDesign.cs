using System;
using CarShowRoom.Model;

namespace CarShowRoom.DesignMode
{
    public class MainWindowDesign
    { 
        public void GetData(Action<MainWinowModel, Exception> callback)
        {
            var mainWinModel = new MainWinowModel();
            callback(mainWinModel, null);
        }
    }
}