using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("jadwal")] 
     public class jadwal:BaseNotifyProperty  
   {
          [PrimaryKey("IdJadwal")] 
          [DbColumn("IdJadwal")] 
          public int IdJadwal 
          { 
               get{return _idjadwal;} 
               set{ 
                      _idjadwal=value; 
                     OnPropertyChange("IdJadwal");
                     }
          } 

          [DbColumn("HariPertama")] 
          public  Day HariPertama 
          { 
               get{return _haripertama;} 
               set{ 
                      _haripertama=value; 
                     OnPropertyChange("HariPertama");
                     }
          } 

          [DbColumn("HariKedua")] 
          public Day HariKedua 
          { 
               get{return _harikedua;} 
               set{ 
                      _harikedua=value; 
                     OnPropertyChange("HariKedua");
                     }
          } 

          [DbColumn("Shif")] 
          public Shif Shif 
          { 
               get{return _shif;} 
               set{ 
                      _shif=value; 
                     OnPropertyChange("Shif");
                     }
          } 

          [DbColumn("JamMulai")] 
          public TimeSpan JamMulai 
          { 
               get{return _jammulai;} 
               set{ 
                      _jammulai=value; 
                     OnPropertyChange("JamMulai");
                     }
          } 

          [DbColumn("JamAkhir")] 
          public TimeSpan JamAkhir 
          { 
               get{return _jamakhir;} 
               set{ 
                      _jamakhir=value; 
                     OnPropertyChange("JamAkhir");
                     }
          } 

          private int  _idjadwal;
           private Day  _haripertama;
           private Day  _harikedua;
           private Shif  _shif;
           private TimeSpan _jammulai;
           private TimeSpan _jamakhir;
      }
}


