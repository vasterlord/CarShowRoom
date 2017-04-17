using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarShowRoom.ViewModel
{
    interface IGeneralComands
    {
        ICommand WindowLoaded { get; set; }
        void OnLoaded(); 

    }
}
