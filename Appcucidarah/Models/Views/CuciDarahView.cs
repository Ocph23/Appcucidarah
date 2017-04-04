using Appcucidarah.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah.Models.Views
{
    public class CuciDarahView:jadwal
    {

        public List<pasien> Pacients { get; set; }
        public CuciDarahView() { Pacients = new List<pasien>(); }

        public CuciDarahView(jadwal item)
        {
            Pacients = new List<pasien>();
            this.HariKedua = item.HariKedua;
            this.HariPertama = item.HariPertama;
            this.IdJadwal = item.IdJadwal;
            this.Shif = item.Shif;
            this.JamAkhir = item.JamAkhir;
            this.JamMulai = item.JamMulai;
        }

        public pasien Selected { get; set; }
    }
}
