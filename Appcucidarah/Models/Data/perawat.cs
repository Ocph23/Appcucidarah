using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("perawat")] 
     public class perawat:BaseNotifyProperty  
   {
          [PrimaryKey("IdPerawat")] 
          [DbColumn("IdPerawat")] 
          public int IdPerawat 
          { 
               get{return _idperawat;} 
               set{ 
                      _idperawat=value; 
                     OnPropertyChange("IdPerawat");
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
               get{return _tanggallahir;} 
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




        private string _ttl;

        public string TTL
        {
            get
            {
                if (!string.IsNullOrEmpty(TempatLahir))
                    _ttl = string.Format("{0},{1}-{2}-{3}", TempatLahir, this.TanggalLahir.Day, TanggalLahir.Month, TanggalLahir.Year);
                return _ttl;
            }
            set { _ttl = value; }
        }

        public kontak Kontak { get; set; }
          private int  _idperawat;
           private string  _nama;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private Gender  _jeniskelamin;
           private Religion  _agama;
           private string  _alamat;
      }
}


