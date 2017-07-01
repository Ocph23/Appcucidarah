using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Appcucidarah.ViewModels;
using DAL;

namespace Appcucidarah.Models.Data 
{ 
     [TableName("pasien")] 
     public class pasien:BaseNotifyProperty  
   {
          [PrimaryKey("IdPasien")] 
          [DbColumn("IdPasien")] 
          public int IdPasien 
          { 
               get{return _idpasien;} 
               set{ 
                      _idpasien=value; 
                     OnPropertyChange("IdPasien");
                     }
          } 

          [DbColumn("NomorPasien")] 
          public string NomorPasien 
          { 
               get{return _nomorpasien;} 
               set{ 
                      _nomorpasien=value; 
                     OnPropertyChange("NomorPasien");
                     }
          } 

          [DbColumn("IdDokter")] 
          public int IdDokter 
          { 
               get{return _iddokter;} 
               set{ 
                      _iddokter=value; 
                     OnPropertyChange("IdDokter");
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("TempatLahir")] 
          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{ 
                      _tempatlahir=value; 
                     OnPropertyChange("TempatLahir");
                     }
          } 

          [DbColumn("TanggalLahir")] 
          public DateTime TanggalLahir 
          { 
               get{return _tanggallahir;} 
               set{ 
                      _tanggallahir=value; 
                     OnPropertyChange("TanggalLahir");
                     }
          } 

          [DbColumn("JenisKelamin")] 
          public Gender JenisKelamin 
          { 
               get{return _jeniskelamin;} 
               set{ 
                      _jeniskelamin=value; 
                     OnPropertyChange("JenisKelamin");
                     }
          } 

          [DbColumn("Agama")] 
          public Religion Agama 
          { 
               get{return _agama;} 
               set{ 
                      _agama=value; 
                     OnPropertyChange("Agama");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
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

        public kontak Kontak { get; internal set; }
    

        public cucidarah GetLastCuciDarah
        {
            get
            {
                using (var db = new OcphDbContext())
                {
                    return db.CuciDarahs.Where(O => O.IdPasien == this.IdPasien).OrderByDescending(O => O.JamAkhir).FirstOrDefault();
                }

            }
        }

        public jadwalpasien JadwalPasien {
            get
            {
                return _jp;
            }
            set
            {
                _jp = value;
                OnPropertyChange(" JadwalPasien");
            }

        }
        public TambahCuciDarahVM PasienCuciDarahVM { get; set; }
       
        public CuciDarahV Proccess
        {
            get
            {
                if (_cucidarah == null)
                {
                    _cucidarah = new CuciDarahV(this);
                }

                return _cucidarah;
            }
        }
        private int  _idpasien;
           private string  _nomorpasien;
           private int  _iddokter;
           private string  _nama;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private Gender  _jeniskelamin;
           private Religion  _agama;
           private string  _alamat;
           private bool  _status;
      
        private CuciDarahV _cucidarah;
        private jadwalpasien _jp;
    }

    public class CuciDarahV:cucidarah{

        private bool _todayOk;
        private cucidarah _todayCuciDarah;
        public CuciDarahV(pasien pasient)
        {
            Pacient = pasient;
            this.IdCuciDarah = 0;
            if(pasient.JadwalPasien!=null)
            {
                this.IdJadwal = pasient.JadwalPasien.IdJadwal;
            }
            
            this.IdPasien = pasient.IdPasien;
            this.ProccessStatus = ProccessStatus.New;
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => !TodayIsOk, ExecuteAction =Refresh };


         //   NurseSourceView = Helper.GetMainContex().Nurses.SourceView;
           // this.Doctor = Helper.GetMainContex().Doctors.Source.Where(O => O.IdDokter == Pacient.IdDokter).FirstOrDefault();
        }


        public cucidarah LastCuciDarah
        {
            get { return _todayCuciDarah; }
            set
            {
                _todayCuciDarah = value;
                if (value != null)
                {
                    TodayIsOk = true;
                }
                OnPropertyChange("LastCuciDarah");
            }
        }



        public bool TodayIsOk
        {
            get
            {
                if (LastCuciDarah == null)
                {
                    LastCuciDarah = Pacient.GetLastCuciDarah;
                    if (LastCuciDarah == null)
                    {
                        return false;
                    }
                    else
                    {
                        var t = this.CurrentDate;
                        if (t.Day == LastCuciDarah.JamAkhir.Day && t.Month == LastCuciDarah.JamAkhir.Month && t.Year == LastCuciDarah.JamAkhir.Year)
                        {
                            _todayOk = true;
                        }
                        else
                        {

                            _todayOk = false;
                        }
                    }

                }
                else
                {
                    var t = DateTime.Now;
                    if (t.Day == LastCuciDarah.JamAkhir.Day && t.Month == LastCuciDarah.JamAkhir.Month && t.Year == LastCuciDarah.JamAkhir.Year)
                    {
                        _todayOk = true;
                    }
                    else
                    {

                        _todayOk = false;
                    }
                }
                return _todayOk;
            }
            set
            {
                _todayOk = value;
                OnPropertyChange("TodayIsOk");
            }
        }


        private void Refresh(object obj)
        {
            this.ProccessStatus = ProccessStatus.New;
            Mulai = null;
            Akhir = null;
        }

        private DateTime? _mulai;
        private DateTime? _akhir;
        private string _sContent;
        private pasien Pacient;
        private DateTime _curentDate;

        public DateTime? Mulai
        {
            get { return _mulai; }
            set
            {
                _mulai = value;
                if(value!=null)
                JamMulai = value.Value;
                OnPropertyChange("Mulai");
            }
        }

        public DateTime? Akhir
        {
            get { return _akhir; }
            set
            {
                _akhir = value;
                if(value!=null)
                    JamAkhir = value.Value;
                OnPropertyChange("Akhir");
            }
        }

        public ProccessStatus ProccessStatus { get; private set; }
        public cucidarah cucidarahModel { get; set; }
        public CommandHandler SaveCommand { get; private set; }
        public CollectionView NurseSourceView { get; private set; }
        public CommandHandler CancelCommand { get; private set; }

        private void SaveCommandAction(object obj)
        {
            if (ProccessStatus.New == ProccessStatus)
            {
              
                this.Mulai = DateTime.Now;
                var form = new Forms.TambahCuciDarah();
               var vm = new ViewModels.TambahCuciDarahVM(Pacient) { WindowClose = form.Close };
                form.DataContext = vm;
                form.ShowDialog();
                if(vm.Nurse!=null)
                {
                    this.IdPerawat = vm.Nurse.IdPerawat;
                    this.ProccessStatus = ProccessStatus.Start;
                }
              
                //SaveCommandContent = "Start";
            }
            else
            if (ProccessStatus.Start == ProccessStatus)
            {
                ProccessStatus = ProccessStatus.Stop;
                this.Akhir = DateTime.Now;
            }
            else
            if (ProccessStatus.Stop == ProccessStatus)
            {
                ProccessStatus = ProccessStatus.Save;
                cucidarah item = (cucidarah)this;
                using (var db = new OcphDbContext())
                {
                    var id = db.CuciDarahs.InsertAndGetLastID(item);
                    if(id>0)
                    {
                        this.IdCuciDarah = id;
                        LastCuciDarah = this;
                        TodayIsOk = true;
                    }
                }
            }

        }

        private bool SaveCommandValidate(object obj)
        {
            TimeSpan ts = DateTime.Now.TimeOfDay;
            SaveCommandContent = "Start";
            if (Pacient == null || TodayIsOk || (Pacient!=null&&Pacient.JadwalPasien==null)||
                (Pacient != null && Pacient.JadwalPasien != null&&Pacient.JadwalPasien.Jadwal.JamMulai>ts) ||
                 (Pacient != null && Pacient.JadwalPasien != null && Pacient.JadwalPasien.Jadwal.JamAkhir < ts))
            {
                return false;
            }else
            {
                if (ProccessStatus == ProccessStatus.New)
                    SaveCommandContent = "Start";

                if (ProccessStatus == ProccessStatus.Start)
                    SaveCommandContent = "Stop";
                if (ProccessStatus == ProccessStatus.Stop)
                    SaveCommandContent = "Simpan";
            }
            return true;
        }


        public string SaveCommandContent {
            get { return _sContent; }
            set
            {
                _sContent = value;
                OnPropertyChange("SaveCommandContent");
            }
        }

        public DateTime CurrentDate {
            get { return _curentDate; }
            set
            {
                _curentDate = value;
                OnPropertyChange("CurrentDate");
            }
        }
    }
}


