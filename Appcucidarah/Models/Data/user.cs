using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("user")] 
     public class user:BaseNotifyProperty  
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

          [DbColumn("NamaLengkap")] 
          public string NamaLengkap 
          { 
               get{return _namalengkap;} 
               set{ 
                      _namalengkap=value; 
                     OnPropertyChange("NamaLengkap");
                     }
          } 

          [DbColumn("UserName")] 
          public string UserName 
          { 
               get{return _username;} 
               set{ 
                      _username=value; 
                     OnPropertyChange("UserName");
                     }
          } 

          [DbColumn("Password")] 
          public string Password 
          { 
               get{return _password;} 
               set{ 
                      _password=value; 
                     OnPropertyChange("Password");
                     }
          } 

          private int  _id;
           private string  _namalengkap;
           private string  _username;
           private string  _password;
      }
}


