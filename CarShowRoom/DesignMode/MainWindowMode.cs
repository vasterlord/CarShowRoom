using System;
using CarShowRoom.Model;

namespace CarShowRoom.DesignMode
{
    public class MainWindowMode
    { 
        public void GetData(Action<MainWinowData, Exception> callback)
        {
            var mainWinData = new MainWinowData();
            callback(mainWinData, null);
        }
    }
}