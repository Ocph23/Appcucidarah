using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appcucidarah.ViewModels;
using OcphSMSLib;
using OcphSMSLib.Models;
using System.Globalization;
using Appcucidarah.Models.Data;

using FirstFloor.ModernUI.Windows.Controls;

namespace Appcucidarah
{
    public class SMSCommandHandler
    {
        private Modem modem;
        private MainWindowVM mainVM;

        public SMSCommandHandler(Modem modem, MainWindowVM mainWindowVM)
        {
            this.modem = modem;
            this.mainVM = mainWindowVM;
        }

        public void ReciveMessage(Inbox inbox)
        {

            var messageparts = inbox.MessageText.Split('#');
            var cmd = messageparts[0].ToUpper();
            switch (cmd)
            {
                case "INFO":
                    this.Info_Action(inbox, messageparts[1]);
                    break;
                case "PINDAHKAN":
                    this.Approved_MoveAsync(inbox);
                    break;
                case "PINDAH":
                    this.Pindah_Action(inbox, messageparts[1]);
                    break;
                default:
                    this.AddMessageToDatabase(inbox);
                    break;
            }

        }

        private async void Approved_MoveAsync(Inbox inbox)
        {
            var messageparts = inbox.MessageText.Split('#');
            string number = "0" + inbox.SenderNumber.Substring(3);

            var admin = mainVM.Contacts.Source.Where(O => O.NomorTelepon == number).FirstOrDefault();

            var messsage = new SMSMessage { DateTime = DateTime.Now, DestinationNumber = inbox.SenderNumber };
            try
            {
                if (admin == null || (admin != null && admin.TipeKontak != ContactType.Admin))
                {
                    messsage.MessageText = "Anda Tidak Memiliki Akses";
                }
                else
                {
                    var nopasient = messageparts[1].Trim();
                    var pacient = mainVM.Pacients.Source.Where(O => O.NomorPasien.ToUpper() == nopasient.ToUpper()).FirstOrDefault();
                    if (pacient == null)
                    {
                        messsage.MessageText = "Pasien Dengan Nomor Pasien " + nopasient + " tidak ditemukan";
                    }
                    else if (pacient.JadwalPasien != null)
                    {
                        var jp = pacient.JadwalPasien;
                        if (jp.Temp == 0 || jp.Status == StatusJadwal.None)
                        {
                            messsage.MessageText = "Pasien Dengan Nomor Pasien " + nopasient + "tidak memiliki permintaan pindah";

                        }
                        else if (jp.Status == StatusJadwal.Pindah)
                        {
                            messsage.MessageText = "Pasien Dengan Nomor Pasien " + nopasient + "telah dipindahkan";
                        }
                        else
                        {
                            var cmd = messageparts[2].Trim().ToUpper();
                            if (cmd == "IZINKAN")
                            {
                                using (var db = new OcphDbContext())
                                {
                                    if (db.JadwalPasients.Update(O => new { O.IdJadwal, O.Status },
                                         new jadwalpasien { IdJadwal = jp.JadwalKe.IdJadwal, Status = StatusJadwal.Pindah }, O => O.IdJadwalPasien == jp.IdJadwalPasien))
                                    {
                                        jp.Status = StatusJadwal.Pindah;
                                        var topasient = messsage.GetClone();
                                        topasient.MessageText = "Permintaan diizinkan Pindah ke Shif " + jp.JadwalKe.Shif + " " + jp.JadwalKe.JamMulai + "-" + jp.JadwalKe.JamAkhir;
                                        topasient.DestinationNumber = pacient.Kontak.NomorTelepon;
                                        modem.SendMessage(topasient);
                                        messsage.MessageText = "Proses Pemindahan Berhasil";

                                    }
                                    else
                                    {
                                        messsage.MessageText = "Terjadi Kesalahan , Hubungi Administrator";
                                    }
                                }
                            }
                            else if (cmd == "TOLAK")
                            {
                                using (var db = new OcphDbContext())
                                {
                                    if (db.JadwalPasients.Update(O => new { O.Temp }, new jadwalpasien { Temp = 0 }, O => O.IdJadwalPasien == jp.IdJadwalPasien))
                                    {
                                        jp.Temp = 0;
                                        var topasient = messsage.GetClone();
                                        topasient.MessageText = "Permintaan Anda Tidak Diizinkan";
                                        topasient.DestinationNumber = pacient.Kontak.NomorTelepon;
                                        modem.SendMessage(topasient);
                                        messsage.MessageText = "Proses Penolakan Berhasil";
                                    }
                                    else
                                    {
                                        messsage.MessageText = "Terjadi Kesalahan , Hubungi Administrator";
                                    }
                                }
                            }
                            else
                            {
                                messsage.MessageText = "Perintah Salah;" + Environment.NewLine + "(PINDAHKAN#NOPASIEN#COMMAND)" + Environment.NewLine + " Ex:PINDAHKAN#12345#IZINKAN";
                            }

                        }
                    }
                    else
                    {
                        messsage.MessageText = "Pasien Dengan Nomor Pasien " + nopasient + "belum memiliki jadwal cuci darah";
                    }

                }

                modem.SendMessage(messsage);

            }
            catch (Exception ex)
            {
                await Task.Delay(1000);
                messsage.MessageText = ex.Message;
                modem.SendMessage(messsage);


            }
        }

        private void AddMessageToDatabase(Inbox inbox)
        {
            string number = string.Empty;
            if (inbox.SenderNumber.Substring(1, 1) == "+")
            {
                number = "0" + inbox.SenderNumber.Substring(3);
            }
            else
            {
                number = inbox.SenderNumber;
            }
            var contact = mainVM.Contacts.Source.Where(O => O.NomorTelepon.Trim() == number.Trim()).FirstOrDefault();
            var messsage = new kotakmasuk { Id = 0, IsiPesan = inbox.MessageText, WaktuTerima = inbox.ReciveDateTime, Status = false };
            if (contact != null)
            {
                messsage.IdKontak = contact.IdKontak;
                messsage.Pengirim = contact.NamaKontak;
            }
            else
            {
                messsage.Pengirim = number;
            }
            using (var db = new OcphDbContext())
            {
                try
                {
                    messsage.Id = db.Inboxs.InsertAndGetLastID(messsage);
                    if (messsage.Id >= 0)
                    {
                        mainVM.Inboxs.AddNewItem(messsage);
                    }
                }
                catch (Exception)
                {

                   // ModernDialog.ShowMessage(ex.Message, "Error", MessageBoxButton.OK);
                }

            }
        }

        public async void  Pindah_Action(Inbox inbox, string req)
        {
            var number = "0" + inbox.SenderNumber.Substring(3);
            try
            {
                using (var db = new OcphDbContext())
                {
                    var pacient = mainVM.Pacients.Source.Where(O => O.Kontak.NomorTelepon.Trim() == number.Trim()).FirstOrDefault();
                    jadwal shifChoice = null;
                    if (pacient != null)
                    {
                        if (pacient.JadwalPasien != null)
                        {
                            jadwalpasien jp = pacient.JadwalPasien;
                            var jadwals = db.Jadwals.Where(O => O.HariPertama == jp.Jadwal.HariPertama && O.HariKedua == jp.Jadwal.HariKedua).ToList();
                            if (jadwals.Count > 0)
                            {
                                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                Shif shif = (Shif)Enum.Parse(typeof(Shif), textInfo.ToTitleCase(req));
                                shifChoice = jadwals.Where(O => O.Shif == shif).FirstOrDefault();
                                if (shifChoice != null && shifChoice.Shif != pacient.JadwalPasien.Jadwal.Shif)
                                {
                                    if (jp != null && jp.Temp <= 0)
                                    {
                                       
                                        var trans = db.Connection.BeginTransaction();
                                        try
                                        {
                                            var countpasient = mainVM.Pacients.Source.Where(O => O.JadwalPasien != null && O.JadwalPasien.IdJadwal == shifChoice.IdJadwal).Count();
                                            if (countpasient >= Helper.MaxSlot)
                                            {
                                                modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = pacient.Kontak.NomorTelepon,
                                                    MessageText = "Maaf Permintaan Pindah Anda, Tidak Dizinkan Karena "+
                                                           shifChoice.Shif.ToString() + " Telah Penuh"
                                                });
                                            }
                                            else
                                            {
                                                jp.Temp = pacient.JadwalPasien.IdJadwal;
                                                var p = new permintaanpindah { Dari = jp.Temp, Ke = shifChoice.IdJadwal, IdPasien = pacient.IdPasien, TanggalPengajuan = DateTime.Now };
                                                var id = db.PermintaanPindahs.InsertAndGetLastID(p);
                                                jp.Temp = id;
                                                jp.Status = StatusJadwal.Normal;
                                                if (id > 0 && db.JadwalPasients.Update(O => new { O.Temp, O.Status }, jp, O => O.IdJadwalPasien == jp.IdJadwalPasien))
                                                {
                                                    modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = pacient.Kontak.NomorTelepon, MessageText = "Menunggu Persetujuan Admin" });
                                                    var admins = mainVM.Contacts.Source.Where(O => O.TipeKontak == ContactType.Admin);
                                                    foreach (var item in admins)
                                                    {
                                                        await Task.Delay(1000);
                                                        modem.SendMessage(new SMSMessage
                                                        {
                                                            DateTime = DateTime.Now,
                                                            DestinationNumber = item.NomorTelepon,
                                                            MessageText = "Ada Permintaan Pindah, Nomor Pasien " + pacient.NomorPasien + Environment.NewLine +
                                                            "Jumlah Pasient di" + shifChoice.Shif.ToString() + " sebanyak " + countpasient + " Orang"
                                                        });
                                                    }
                                                    trans.Commit();
                                                }
                                                else
                                                {
                                                    throw new SystemException("Permintaan Anda Tidak Dapat Diproses, Hubungi Petugas");
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            trans.Rollback();
                                            modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = pacient.Kontak.NomorTelepon, MessageText = ex.Message });
                                        }

                                    }else if (jp != null && jp.Temp > 0)
                                    {
                                        throw new SystemException("Anda Telah Mengajukan Permohona Pindah, Tunggu Proses Selanjutnya");
                                    }
                                    else
                                        throw new SystemException("Shif " + shif.ToString() + " Tidak Tersedia");
                                }
                                else if (shifChoice != null && shifChoice.Shif == pacient.JadwalPasien.Jadwal.Shif)
                                {
                                    throw new SystemException("Anda Sudah Ada di Shif " + shif.ToString());
                                }
                                else
                                {
                                    throw new SystemException("Jadwal Pilihan Pindah Anda Tidak Ditemukan");
                                }
                            }
                            else
                            {
                                throw new SystemException("Anda Belum Memiliki Jadwal, Hubungi Petugas");
                            }


                        }
                        else
                        {
                            throw new SystemException("Anda Belum Memiliki Jadwal, Hubungi Petugas");
                        }
                    }
                    else
                    {
                        throw new SystemException("Nomor Anda Belum Terdaftar");
                    }
                }

            }
            catch (Exception ex)
            {
                modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = number, MessageText = ex.Message });
            }
        }
        private void Info_Action(Inbox inbox, string req)
        {

            switch (req.ToUpper())
            {
                case "JADWAL":
                    this.CekJadwal(inbox); ;
                    break;
                case "MINUMOBAT":
                    this.MinumObat(inbox);
                    break;
                default:
                    this.SalahPerintah(inbox);
                    break;
            }


            // pacient.Jadwal.Shif== Shif
        }

        private void MinumObat(Inbox inbox)
        {
            var number = "0" + inbox.SenderNumber.Substring(3);
            var pacient = mainVM.Pacients.Source.Where(O => O.Kontak.NomorTelepon.Trim() == number.Trim()).FirstOrDefault();

            if (pacient != null && pacient.GetLastCuciDarah != null)
            {
                var StartDate = pacient.GetLastCuciDarah.JamAkhir.TimeOfDay;
                List<TimeSpan> list = new List<TimeSpan>();
                var lengt = (6 * 60);
                var d = new TimeSpan(6, 0, 0);
                var count = (24 * 60) / lengt;
                StringBuilder sb = new StringBuilder();
                sb.Append("Jam Mimum Obat :" + Environment.NewLine);
                for (int i = 0; i < count; i++)
                {
                    StartDate = StartDate.Add(d);
                    var no = i + 1;
                    sb.Append(no + ". " + StartDate.ToString() + Environment.NewLine);

                }
                modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = number, MessageText = sb.ToString() });
            }
            else
                modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = number, MessageText = "Anda Belum Memiliki Jadwal Minum Obat" });
        }

        private void SalahPerintah(Inbox inbox)
        {
            var number = "0" + inbox.SenderNumber.Substring(3);
            modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = number, MessageText = "Perintah Anda Salah" });
        }

        private void CekJadwal(Inbox inbox)
        {
            var number = "0" + inbox.SenderNumber.Substring(3);
            try
            {
                var pacient = mainVM.Pacients.Source.Where(O => O.Kontak.NomorTelepon.Trim() == number.Trim()).FirstOrDefault();
                if (pacient != null && pacient.JadwalPasien != null && pacient.JadwalPasien.Jadwal != null)
                {
                    modem.SendMessage(new SMSMessage
                    {
                        DateTime = DateTime.Now,
                        DestinationNumber = number,
                        MessageText = "Jadwal Anda Setiap Hari : " +
                        pacient.JadwalPasien.Jadwal.HariPertama.ToString() + "&" + pacient.JadwalPasien.Jadwal.HariKedua + "Shif " +
                        pacient.JadwalPasien.Jadwal.Shif.ToString()
                    });
                }
                else if (pacient == null)
                    throw new SystemException("Nomor Anda Belum Terdaftar");
                else if (pacient != null && pacient.JadwalPasien == null)
                    throw new SystemException("Anda Belum Memiliki Jadwal, Hubungi Petugas");
            }
            catch (Exception)
            {
                modem.SendMessage(new SMSMessage { DateTime = DateTime.Now, DestinationNumber = number, MessageText = "Terjadi Kesalahan, Hubungi Petugas" });

            }


        }
    }
}
