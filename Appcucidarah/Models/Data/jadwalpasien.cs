using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using DAL;
 
 namespace Appcucidarah.Models.Data 
{ 
     [TableName("jadwalpasien")] 
     public class jadwalpasien:BaseNotifyProperty,ICloneable
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
            get { return _idjadwal; }
            set
            {
                _idjadwal = value;
                OnPropertyChange("IdJadwal");
            }
        }
        [DbColumn("Status")] 
          public StatusJadwal Status 
          { 
               get{return _status;} 
               set{ 
                      _status=value; 
                     OnPropertyChange("Status");
                     }
          }

        [DbColumn("IdPindah")]
        public int Temp
        {
            get { return _temp; }
            set
            {
                _temp = value;
                OnPropertyChange("Temp");
            }
        }

        public jadwal Jadwal {
            get
            {
                using(var db=new OcphDbContext())
                {
                    return db.Jadwals.Where(O => O.IdJadwal == IdJadwal).FirstOrDefault();
                }
               
            }    
        }

        public jadwal JadwalDari
        {
            get
            {
                using (var db = new OcphDbContext())
                {
                    if(Pindah != null)
                    {
                        return db.Jadwals.Where(O => O.IdJadwal == Pindah.Dari).FirstOrDefault();
                    }

                    return null;
                }
            }
        }

        public jadwal JadwalKe
        {
            get
            {
                using (var db = new OcphDbContext())
                {
                    if (Pindah != null)
                        return db.Jadwals.Where(O => O.IdJadwal == Pindah.Ke).FirstOrDefault();
                    return null;
                }
            }
        }


        public jadwalpasien Clone()
        {
            return (jadwalpasien)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public permintaanpindah Pindah
        {
            get
            {
                using(var db = new OcphDbContext())
                {
                    if (this.Temp > 0)
                        return db.PermintaanPindahs.Where(O => O.Id == Temp).FirstOrDefault();
                    else
                        return null;
                }
               
            }
        }




        private int  _idtransaksijadwal;
           private int  _idpasien;
           private int  _idjadwal;
           private StatusJadwal  _status;
        private int _temp;
    }
}


