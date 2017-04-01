using Appcucidarah.BaseCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah.ViewModels
{
    public class MainWindowVM
    {
        public MainWindowVM()
        {
            this.Doctors = new Doctors();
            Nurses = new Perawats();
            this.Pacients = new Pacients();
            this.Jadwals = new Jadwals();
        }

        public Doctors Doctors { get; set; }
        public BaseCollection.Jadwals Jadwals { get;  set; }
        public Perawats Nurses { get;  set; }
        public Pacients Pacients { get; set; }
    }
}
