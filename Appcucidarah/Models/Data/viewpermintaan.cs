using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah.Models.Data
{
    [TableName("viewpermintaan")]
    public class viewpermintaan : BaseNotifyProperty
    {
        [DbColumn("NomorPasien")]
        public string NomorPasien
        {
            get { return _nomorpasien; }
            set
            {
                _nomorpasien = value;
                OnPropertyChange("NomorPasien");
            }
        }

        [DbColumn("Nama")]
        public string Nama
        {
            get { return _nama; }
            set
            {
                _nama = value;
                OnPropertyChange("Nama");
            }
        }

        [DbColumn("Status")]
        public StatusJadwal Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChange("Status");
            }
        }

        [DbColumn("IdPindah")]
        public int IdPindah
        {
            get { return _idpindah; }
            set
            {
                _idpindah = value;
                OnPropertyChange("IdPindah");
            }
        }

        [DbColumn("HariPertama")]
        public string HariPertama
        {
            get { return _haripertama; }
            set
            {
                _haripertama = value;
                OnPropertyChange("HariPertama");
            }
        }

        [DbColumn("HariKedua")]
        public string HariKedua
        {
            get { return _harikedua; }
            set
            {
                _harikedua = value;
                OnPropertyChange("HariKedua");
            }
        }

        [DbColumn("Awal")]
        public Shif Awal
        {
            get { return _awal; }
            set
            {
                _awal = value;
                OnPropertyChange("Awal");
            }
        }

        [DbColumn("Permintaan")]
        public Shif Permintaan
        {
            get { return _permintaan; }
            set
            {
                _permintaan = value;
                OnPropertyChange("Permintaan");
            }
        }

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

        [PrimaryKey("IdJadwalPasien")]
        [DbColumn("IdJadwalPasien")]
        public int IdJadwalPasien
        {
            get { return _idjadwalpasien; }
            set
            {
                _idjadwalpasien = value;
                OnPropertyChange("IdJadwalPasien");
            }
        }

        private string _nomorpasien;
        private string _nama;
        private StatusJadwal _status;
        private int _idpindah;
        private string _haripertama;
        private string _harikedua;
        private Shif _awal;
        private Shif _permintaan;
        private int _id;
        private int _idpasien;
        private int _idjadwalpasien;
    }

}
