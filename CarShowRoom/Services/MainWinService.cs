using CarShowRoom.Model;
using System;

namespace CarShowRoom.Services
{
    public class MainWinService : IMainWinService
    {
        public void GetData(Action<MainWinData, Exception> callback)
        {
            // Use this to connect to the actual data service

            var mainWinData = new MainWinData(); 
            mainWinData.ToolBarDescription = "Date and time:  ";
            callback(mainWinData, null);
        }
    }
}