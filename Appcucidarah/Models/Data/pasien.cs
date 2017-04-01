using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("pasien")] 
     public class pasien:BaseNotifyProperty  
   {
          [PrimaryKey("IdPasien")] 
          [DbColumn("IdPasien")] 
          public int IdPasien 
          { 
               get{return _idpasien;} 
               set{ 
                      _idpasien=value; 
                     OnPropertyChange("IdPasien");
                     }
          } 

          [DbColumn("NomorPasien")] 
          public string NomorPasien 
          { 
               get{return _nomorpasien;} 
               set{ 
                      _nomorpasien=value; 
                     OnPropertyChange("NomorPasien");
                     }
          } 

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

      

          [DbColumn("Status")] 
          public bool Status 
          { 
               get{return _status;} 
               set{ 
                      _status=value; 
                     OnPropertyChange("Status");
                     }
          }

        public kontak Kontak { get; internal set; }

        private int  _idpasien;
           private string  _nomorpasien;
           private int  _iddokter;
           private string  _nama;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private Gender  _jeniskelamin;
           private Religion  _agama;
           private string  _alamat;
           private bool  _status;
      }
}


