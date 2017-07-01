using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("kotakterkirim")] 
     public class kotakterkirim:BaseNotifyProperty  
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

          [DbColumn("WaktuTerkirim")] 
          public DateTime WaktuTerkirim 
          { 
               get{return _waktuterkirim;} 
               set{ 
                      _waktuterkirim=value; 
                     OnPropertyChange("WaktuTerkirim");
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
        [DbColumn("Penerima")]
        public string Penerima
        {
            get { return _penerima; }
            set
            {
                _penerima= value;
                OnPropertyChange("Penerima");
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
           private DateTime  _waktuterkirim;
           private string  _isipesan;
           private bool  _status;
        private string _penerima;
    }
}


