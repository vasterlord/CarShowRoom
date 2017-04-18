using System;
using CarShowRoom.Model;

namespace CarShowRoom.Design
{
    public class DesignMainWinService
    { 
        public void GetData(Action<MainWinData, Exception> callback)
        {
            var mainWinData = new MainWinData();
            callback(mainWinData, null);
        }
    }
}