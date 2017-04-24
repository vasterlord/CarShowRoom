using CarShowRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowRoom.DesignMode
{
    class ClientDesign
    {
        public void GetData(Action<Client, Exception> callback)
        {
            var clientModel = new Client();
            callback(clientModel, null);
        }
    }
}
