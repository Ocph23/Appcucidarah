using Appcucidarah.BaseCollection;
using OcphSMSLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Appcucidarah.Models.Data;
using System.Configuration;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace Appcucidarah.ViewModels
{
    public class MainWindowVM
    {
        private SMSCommandHandler SMSCommand;
       public  DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindowVM(user userActive)
        {
            var port = ConfigurationManager.AppSettings["DevicePort"];
            this.Modem = new Modem(port);
            this.UserLogin = userActive;
            this.Modem.Signature = "RSUD-JPR";
            this.SMSCommand = new SMSCommandHandler(Modem,this);
            this.Modem.OnReciveSMS += Modem_OnReciveSMS;
            this.Modem.OnSendingSMS += Modem_OnSendingSMS;
            this.Modem.OnError += Modem_OnError;
            this.Modem.Connect();


            this.LoadData();
            dispatcherTimer.Tick += DispatcherTimer_Tick; 
           dispatcherTimer.Interval = new TimeSpan(0, 2, 0);
            dispatcherTimer.Start();
        }

        public void LoadData()
        {
            this.Doctors = new Doctors();
            this.Nurses = new Perawats();
            this.Pacients = new Pacients();
            this.Jadwals = new Jadwals();
            this.Users = new Users();
            this.Contacts = new Contacts();
           // this.JadwalPasiens = new JadwalPasients();
            this.Inboxs = new Inboxs();
            this.Outboxs = new Outboxs();
        }

        private void Modem_OnSendingSMS(OcphSMSLib.Models.SendBox sendbox, EventArgs args)
        {
            var number = "0" + sendbox.DestenationNumber.Substring(3);
            var contact =this.Contacts.Source.Where(O => O.NomorTelepon == number).FirstOrDefault();
            var msg = new kotakterkirim { IsiPesan = sendbox.MessageText, WaktuTerkirim = sendbox.SendingDateTime, Status = true};
            if (contact!=null)
            {
                msg.Penerima = contact.NamaKontak;
                msg.IdKontak = contact.IdKontak;
            }else
            {
                msg.Penerima = number;
            }
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    msg.Id=  db.Outboxs.InsertAndGetLastID(msg);
                    if(msg.Id>0)
                    {
                        Outboxs.AddNewItem(msg);
                        Outboxs.SourceView.Refresh();
                        trans.Commit();
                    }else
                    {
                        throw new SystemException("Data Gagal Ditambahkan Ke Database");
                    }
                    
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
                }
            }
         
           

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
           if(this.ModemLamp== ModemLam.On)
            {
                var date = DateTime.Now;

                foreach (var pacient in this.Pacients.Source.Where(O => O.Status == true))
                {
                    if (date.TimeOfDay >= new TimeSpan(9, 0, 0) && date.TimeOfDay <= new TimeSpan(9, 1, 0))
                    {
                        if (pacient.Kontak != null && pacient.JadwalPasien != null)
                        {
                            var idxDay = 1 + (Int32)date.DayOfWeek;
                            var jp = pacient.JadwalPasien.Jadwal;
                            if (jp != null)
                            {
                                var iH1 = (Int32)jp.HariPertama;
                                var iH2 = (Int32)jp.HariKedua;
                                if (iH1 == idxDay || iH2 == idxDay)
                                {
                                    DateTime tgl = DateTime.Now.Add(TimeSpan.FromDays(1));
                                    Modem.SendMessage(new OcphSMSLib.Models.SMSMessage { DateTime = DateTime.Now, DestinationNumber = pacient.Kontak.NomorTelepon, MessageText = "Anda Harus Melakukan Cuci Darah Besok Tgl " + tgl.ToShortDateString() + "Jam " + jp.JamMulai });
                                }
                            }
                            //  Modem.AddNewMessage()
                        }
                    }

                    var LastCuciDarah = pacient.GetLastCuciDarah;
                    if (LastCuciDarah != null)
                    {
                        var StartDate = LastCuciDarah.JamAkhir.TimeOfDay;
                        List<TimeSpan> list = new List<TimeSpan>();
                        var lengt = (2);
                        var d = new TimeSpan(0, 2, 0);
                        var count = (24 * 60) / lengt;
                        for(int i =0; i<count;i++)
                        {
                           StartDate= StartDate.Add(d);
                            list.Add(StartDate);
                        }
                       

                        var time = DateTime.Now.TimeOfDay;
                        var low = time.Subtract(new TimeSpan(0, 2, 0));
                        var height = time.Add(new TimeSpan(0, 2, 0));
                        var res = list.Where(O => O > low && O < height).FirstOrDefault();
                        if(res!=null && res!= new TimeSpan(0, 0, 0))
                        {
                            Modem.SendMessage(new OcphSMSLib.Models.SMSMessage { DateTime = DateTime.Now, DestinationNumber = pacient.Kontak.NomorTelepon, MessageText = "Saatnya Anda Minum Obat" });

                        }
                    }
                }
            }
        }

        private void Modem_OnError(string message, int ErrorNumber, EventArgs args)
        {
            if(ErrorNumber==13)
            {
                ModemLamp = ModemLam.Warning;
            }else
            {
                ModemLamp = ModemLam.OFF;
                var i = this;
                 Application.Current.Dispatcher.BeginInvoke(
                  DispatcherPriority.Background,
                  new Action(() => {
                      ModernDialog.ShowMessage(message, "Error", System.Windows.MessageBoxButton.OK);
                  }));
                
            }
        }

        public enum ModemLam
        {
            On,OFF,Warning
        }
        private void Modem_OnReciveSMS(OcphSMSLib.Models.Inbox inbox, EventArgs args)
        {
            SMSCommand.ReciveMessage(inbox);
           
        }

        public Doctors Doctors { get; set; }
      //  public JadwalPasients JadwalPasiens { get; private set; }
        public Jadwals Jadwals { get;  set; }
        public Modem Modem { get; private set; }
        public Perawats Nurses { get;  set; }
        public Pacients Pacients { get; set; }
        public ModemLam ModemLamp { get; private set; }
        public user UserLogin { get; private set; }
        public Users Users { get; private set; }
        public Inboxs Inboxs { get; private set; }
        public Outboxs Outboxs { get; private set; }
        public Contacts Contacts { get; private set; }
       
    }
}
