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

          [DbColumn("IdTransaksiJadwal")] 
          public int IdTransaksiJadwal 
          { 
               get{return _idtransaksijadwal;} 
               set{ 
                      _idtransaksijadwal=value; 
                     OnPropertyChange("IdTransaksiJadwal");
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
           private int  _idtransaksijadwal;
           private DateTime  _jammulai;
           private DateTime  _jamakhir;
      }
}


