using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("jadwalpasien")] 
     public class jadwalpasien:BaseNotifyProperty  
   {
          [PrimaryKey("IdJadwalPasien")] 
          [DbColumn("IdJadwalPasien")] 
          public int IdJadwalPasien
        { 
               get{return _idtransaksijadwal;} 
               set{ 
                      _idtransaksijadwal=value; 
                     OnPropertyChange("IdJadwalPasien");
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
               get{return _idjadwal;} 
               set{ 
                      _idjadwal=value; 
                     OnPropertyChange("IdJadwal");
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



        public pasien Pacient { get; set; }

        public jadwal Jadwal { get; set; }


        private int  _idtransaksijadwal;
           private int  _idpasien;
           private int  _idjadwal;
           private bool  _status;
      }
}


