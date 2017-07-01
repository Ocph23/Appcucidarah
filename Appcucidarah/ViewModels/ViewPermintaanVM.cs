using Appcucidarah.Models.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Appcucidarah.ViewModels
{
    public class ViewPermintaanVM:DAL.BaseNotifyProperty
    {
        private string _alasan;

        public CollectionView SourceView { get; set; }

        public Models.Data.pasien SelectedItem { get; set; }

        public CommandHandler TidakIzinCommand { get; set; }
        public CommandHandler IzinCommand { get; set; }
        public string Alasan
        {
            get { return _alasan; }
            set
            {
                _alasan = value;
                OnPropertyChange("Alasan");
            }
        }
      
        private bool CommandValidation(object obj)
        {
            if (SelectedItem != null && SelectedItem.JadwalPasien !=null && SelectedItem.JadwalPasien.Pindah!=null && SelectedItem.JadwalPasien.Status == StatusJadwal.Normal)
            { return true; }
            else
            {
                return false;
            }
        }
        private void IzinCommandAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
                var pindah = db.PermintaanPindahs.Where(O => O.Id == SelectedItem.JadwalPasien.Temp).FirstOrDefault();
                var jadwalpindah = Helper.GetMainContex().Jadwals.Source.Where(O => O.IdJadwal == pindah.Ke).FirstOrDefault();
                if (pindah != null && jadwalpindah!=null && db.JadwalPasients.Update(O=> new { O.IdJadwal,O.Status},new jadwalpasien { IdJadwal=pindah.Ke, Status= StatusJadwal.Pindah },O=>O.IdJadwalPasien==SelectedItem.JadwalPasien.IdJadwalPasien))
                {
                    var msg = "Permintaan diizinkan Pindah ke Shif " + jadwalpindah.Shif + " " + jadwalpindah.JamMulai + "-" + jadwalpindah.JamAkhir;
                    Helper.GetMainContex().Modem.SendMessage(new OcphSMSLib.Models.SMSMessage { DestinationNumber = SelectedItem.Kontak.NomorTelepon, MessageText = msg });
                    SelectedItem.JadwalPasien.Status= StatusJadwal.Pindah;
                    this.SourceView.Refresh();
                    SelectedItem = null;
                }
            }

        }

        private void TidakIzinCommandAction(object obj)
        {
            using (var db = new OcphDbContext())
            {
             //   var pindah = db.PermintaanPindahs.Where(O => O.Id == SelectedItem.JadwalPasien.JadwalPindah).FirstOrDefault();


                if (db.JadwalPasients.Update(O => new { O.Temp }, new jadwalpasien { Temp=0 }, O => O.IdJadwalPasien == SelectedItem.JadwalPasien.IdJadwalPasien))
                {
                    Helper.GetMainContex().Modem.SendMessage(new OcphSMSLib.Models.SMSMessage { DestinationNumber = SelectedItem.Kontak.NomorTelepon, MessageText = "Maaf Permintaan Anda Tidak Kami Diizinkan "+this.Alasan });
                    this.SelectedItem.JadwalPasien.Temp = 0;
                    this.SourceView.Refresh();
                }
                else
                {
                    Helper.GetMainContex().Modem.SendMessage(new OcphSMSLib.Models.SMSMessage { DestinationNumber =SelectedItem.Kontak.NomorTelepon, MessageText = "Maaf Terjadi Kesalahan" });
                }
            }

        }



        public ViewPermintaanVM()
        {
            IzinCommand = new CommandHandler { CanExecuteAction = CommandValidation, ExecuteAction = IzinCommandAction };
            TidakIzinCommand = new CommandHandler { CanExecuteAction = CommandValidation, ExecuteAction = TidakIzinCommandAction };
            SourceView = Helper.GetMainContex().Pacients.SourceView;
            SourceView.Filter = this.Filter;
            SourceView.Refresh();
        }

        public bool Filter(object obj)
        {
            var x = (pasien)obj;
            if (x.JadwalPasien != null && x.JadwalPasien.Pindah != null && x.JadwalPasien.JadwalDari!=null)
            {
                return true;
            }
            else
                return false;
        }


    }
}
