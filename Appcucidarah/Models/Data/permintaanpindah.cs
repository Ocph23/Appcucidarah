using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah.Models.Data
{
    [TableName("permintaanpindah")]
    public class permintaanpindah : BaseNotifyProperty
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("IdPasien")]
        public int IdPasien
        {
            get { return _idpasien; }
            set
            {
                _idpasien = value;
                OnPropertyChange("IdPasien");
            }
        }

        [DbColumn("Dari")]
        public int Dari
        {
            get { return _dari; }
            set
            {
                _dari = value;
                OnPropertyChange("Dari");
            }
        }

        [DbColumn("Ke")]
        public int Ke
        {
            get { return _ke; }
            set
            {
                _ke = value;
                OnPropertyChange("Ke");
            }
        }

        [DbColumn("DisetujuiOleh")]
        public int DisetujuiOleh
        {
            get { return _disetujuioleh; }
            set
            {
                _disetujuioleh = value;
                OnPropertyChange("DisetujuiOleh");
            }
        }

        [DbColumn("TanggalPengajuan")]
        public DateTime TanggalPengajuan
        {
            get { return _tanggalpengajuan; }
            set
            {
                _tanggalpengajuan = value;
                OnPropertyChange("TanggalPengajuan");
            }
        }

        private int _id;
        private int _idpasien;
        private int _dari;
        private int _ke;
        private int _disetujuioleh;
        private DateTime _tanggalpengajuan;
    }

}
