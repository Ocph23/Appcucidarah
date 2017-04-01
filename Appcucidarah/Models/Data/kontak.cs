using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("kontak")] 
     public class kontak:BaseNotifyProperty  
   {
        [PrimaryKey("IdKontak")]
        [DbColumn("IdKontak")]
        public int IdKontak
        {
            get { return _idkontak; }
            set
            {
                _idkontak = value;
                OnPropertyChange("IdKontak");
            }
        }

        [DbColumn("NomorTelepon")]
        public string NomorTelepon
        {
            get { return _nomortelepon; }
            set
            {
                _nomortelepon = value;
                OnPropertyChange("NomorTelepon");
            }
        }

        [DbColumn("NamaKontak")]
        public string NamaKontak
        {
            get {

                if (string.IsNullOrEmpty(_namakontak))
                    _namakontak = NomorTelepon;
                return _namakontak; }
            set
            {
                _namakontak = value;
                OnPropertyChange("NamaKontak");
            }
        }

        [DbColumn("TipeKontak")]
        public ContactType TipeKontak
        {
            get { return _tipekontak; }
            set
            {
                _tipekontak = value;
                OnPropertyChange("TipeKontak");
            }
        }

        [DbColumn("UserId")]
        public int UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChange("UserId");
            }
        }

        private int _idkontak;
        private string _nomortelepon;
        private ContactType _tipekontak;
        private int _userid;
        private string _namakontak;
    }
}


