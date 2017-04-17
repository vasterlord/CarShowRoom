using System;
using CarShowRoom.Model;
using CarShowRoom.Services;

namespace CarShowRoom.Design
{
    public class DesignMainWinService : IMainWinService
    {
        public void GetData(Action<MainWinData, Exception> callback)
        {
            // Use this to create design time data
            var mainWinData = new MainWinData();
            mainWinData.ToolBarDescription = "Date and time:  ";
            callback(mainWinData, null);
        }
    }
}