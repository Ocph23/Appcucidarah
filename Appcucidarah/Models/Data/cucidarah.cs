using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("cucidarah")] 
     public class cucidarah:BaseNotifyProperty  
   {
          [PrimaryKey("IdCuciDarah")] 
          [DbColumn("IdCuciDarah")] 
          public int IdCuciDarah 
          { 
               get{return _idcucidarah;} 
               set{ 
                      _idcucidarah=value; 
                     OnPropertyChange("IdCuciDarah");
                     }
          } 

          [DbColumn("IdPasien")] 
          public int IdPasien
          { 
               get{return _idpasien;} 
               set{ 
                      _idpasien=value; 
                     OnPropertyChange("IdPasien");
                     }
          }

        [DbColumn("IdJadwal")]
        public int IdJadwal
        {
            get { return _idjadwal; }
            set
            {
                _idjadwal = value;
                OnPropertyChange("IdJadwal");
            }
        }

        [DbColumn("IdPerawat")]
        public int IdPerawat
        {
            get { return _idperawat; }
            set
            {
                _idperawat = value;
                OnPropertyChange("IdPerawat");
            }
        }

        [DbColumn("JamMulai")] 
          public DateTime JamMulai 
          { 
               get{return _jammulai;} 
               set{ 
                      _jammulai=value; 
                     OnPropertyChange("JamMulai");
                     }
          } 

          [DbColumn("JamAkhir")] 
          public DateTime JamAkhir 
          { 
               get{return _jamakhir;} 
               set{ 
                      _jamakhir=value; 
                     OnPropertyChange("JamAkhir");
                     }
          } 

          private int  _idcucidarah;
           private int  _idpasien;
           private DateTime  _jammulai;
           private DateTime  _jamakhir;
        private int _idjadwal;
        private int _idperawat;
    }
}


