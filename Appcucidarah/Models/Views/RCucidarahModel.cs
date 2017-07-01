using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah.Models.Views
{
    public class RCucidarahModel
    {
        public string Tanggal { get; set; }
        public string NomorPasien { get; set; }
        public string Nama { get; set; }
        public string Perawat{ get; set; }
        public TimeSpan Mulai { get; set; }
        public TimeSpan Akhir { get; set; }
    }
}
