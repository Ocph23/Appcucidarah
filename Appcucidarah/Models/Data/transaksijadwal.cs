using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("transaksijadwal")] 
     public class transaksijadwal:BaseNotifyProperty  
   {
          [PrimaryKey("IdTransaksiJadwal")] 
          [DbColumn("IdTransaksiJadwal")] 
          public int IdTransaksiJadwal 
          { 
               get{return _idtransaksijadwal;} 
               set{ 
                      _idtransaksijadwal=value; 
                     OnPropertyChange("IdTransaksiJadwal");
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
          public string Status 
          { 
               get{return _status;} 
               set{ 
                      _status=value; 
                     OnPropertyChange("Status");
                     }
          } 

          private int  _idtransaksijadwal;
           private int  _idpasien;
           private int  _idjadwal;
           private string  _status;
      }
}


