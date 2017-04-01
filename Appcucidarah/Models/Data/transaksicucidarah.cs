using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("transaksicucidarah")] 
     public class transaksicucidarah:BaseNotifyProperty  
   {
          [PrimaryKey("IdTransaksiCuciDarah")] 
          [DbColumn("IdTransaksiCuciDarah")] 
          public int IdTransaksiCuciDarah 
          { 
               get{return _idtransaksicucidarah;} 
               set{ 
                      _idtransaksicucidarah=value; 
                     OnPropertyChange("IdTransaksiCuciDarah");
                     }
          } 

          [DbColumn("IdCuciDarah")] 
          public int IdCuciDarah 
          { 
               get{return _idcucidarah;} 
               set{ 
                      _idcucidarah=value; 
                     OnPropertyChange("IdCuciDarah");
                     }
          } 

          [DbColumn("IdPerawat")] 
          public int IdPerawat 
          { 
               get{return _idperawat;} 
               set{ 
                      _idperawat=value; 
                     OnPropertyChange("IdPerawat");
                     }
          } 

          private int  _idtransaksicucidarah;
           private int  _idcucidarah;
           private int  _idperawat;
      }
}


