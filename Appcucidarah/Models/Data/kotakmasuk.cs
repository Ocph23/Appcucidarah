using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("kotakmasuk")] 
     public class kotakmasuk:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("IdKontak")] 
          public int IdKontak 
          { 
               get{return _idkontak;} 
               set{ 
                      _idkontak=value; 
                     OnPropertyChange("IdKontak");
                     }
          } 

          [DbColumn("WaktuTerima")] 
          public DateTime WaktuTerima 
          { 
               get{return _waktuterima;} 
               set{ 
                      _waktuterima=value; 
                     OnPropertyChange("WaktuTerima");
                     }
          } 

          [DbColumn("IsiPesan")] 
          public string IsiPesan 
          { 
               get{return _isipesan;} 
               set{ 
                      _isipesan=value; 
                     OnPropertyChange("IsiPesan");
                     }
          }


        [DbColumn("Pengirim")]
        public string Pengirim
        {
            get { return _pengirim; }
            set
            {
                _pengirim = value;
                OnPropertyChange("Pengirim");
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

          private int  _id;
           private int  _idkontak;
           private DateTime  _waktuterima;
           private string  _isipesan;
           private bool _status;
        private string _pengirim;
    }
}


