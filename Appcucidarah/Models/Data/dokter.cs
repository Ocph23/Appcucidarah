using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("dokter")] 
     public class dokter:BaseNotifyProperty  
   {
          [PrimaryKey("IdDokter")] 
          [DbColumn("IdDokter")] 
          public int IdDokter 
          { 
               get{return _iddokter;} 
               set{ 
                      _iddokter=value; 
                     OnPropertyChange("IdDokter");
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("TempatLahir")] 
          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{ 
                      _tempatlahir=value; 
                     OnPropertyChange("TempatLahir");
                     }
          } 

          [DbColumn("TanggalLahir")] 
          public DateTime TanggalLahir 
          { 
               get{
                if (_tanggallahir == new DateTime())
                {
                    _tanggallahir = new DateTime(1970, 1, 1);
                }
                return _tanggallahir;} 
               set{ 
                      _tanggallahir=value; 
                     OnPropertyChange("TanggalLahir");
                     }
          } 

          [DbColumn("JenisKelamin")] 
          public Gender JenisKelamin 
          { 
               get{return _jeniskelamin;} 
               set{ 
                      _jeniskelamin=value; 
                     OnPropertyChange("JenisKelamin");
                     }
          } 

          [DbColumn("Agama")] 
          public Religion Agama 
          { 
               get{return _agama;} 
               set{ 
                      _agama=value; 
                     OnPropertyChange("Agama");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
                     }
          } 

        



        public kontak Kontak { get; set; }


        private string _ttl;

        public string TTL
        {
            get {
                if (!string.IsNullOrEmpty(TempatLahir))
                    _ttl = string.Format("{0},{1}-{2}-{3}",TempatLahir, this.TanggalLahir.Day, TanggalLahir.Month, TanggalLahir.Year);
                return _ttl; }
            set { _ttl = value; }
        }



        private int  _iddokter;
           private string  _nama;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private Gender  _jeniskelamin;
           private Religion  _agama;
           private string  _alamat;
    }
}


