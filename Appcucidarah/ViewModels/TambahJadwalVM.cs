using FirstFloor.ModernUI.Windows.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using Appcucidarah.Models.Data;

namespace Appcucidarah.ViewModels
{
   public class TambahJadwalVM : Models.Data.jadwal, IDataErrorInfo
    {
        #region Properties
        public List<Day> Days { get; set; }
        public List<Shif> Shifs { get; set; }

        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }
        #endregion


        #region Constructor
        public TambahJadwalVM()
        {
            this.Load();
            this.IsNew = true;
        }

        public TambahJadwalVM(jadwal selected)
        {
            this.Load();
            this.IsNew = false;
            this.Akhir = new DateTime().Add(selected.JamAkhir);
            this.Mulai = DateTime.Now.Add(selected.JamMulai);
            this.HariKedua = selected.HariKedua;
            this.HariPertama = selected.HariPertama;
            this.Shif = selected.Shif;
            this.IdJadwal = selected.IdJadwal;
            this.JamAkhir = selected.JamAkhir;
            this.JamMulai = selected.JamMulai;
        }

        private void Load()
        {
            Days = Helper.GetEnumCollection<Day>();
            Shifs = Helper.GetEnumCollection<Shif>();
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };

        }

        #endregion


        #region Action
        private void SaveCommandAction(object obj)
        {
            var context = Helper.GetMainContex().Jadwals;


            try
            {
                if (IsNew)
                {
                    if (context.Insert(this))
                    {
                        var dlg = new ModernDialog { Title = "Success", Content = "Data Berhasil Diperbaharui " };
                        dlg.ShowDialog();
                    }
                    else
                    {
                        var dlg = new ModernDialog { Title = "Error", Content = "Data Gagal Ditambah " };
                        dlg.ShowDialog();
                    }
                }else
                {
                    if(context.Update(this))
                    {
                        var dlg = new ModernDialog { Title = "Info", Content = "Data Berhasil Diperbaharui " };
                        dlg.ShowDialog();
                    }else
                    {
                        ModernDialog.ShowMessage("Data Gagal Diperbaharui", "Error", MessageBoxButton.OK);
                    }
                }
            }
            catch (MySqlException ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Error", MessageBoxButton.OK);
            }
          
        }
        #endregion

        #region Validated
        private bool SaveCommandValidate(object obj)
        {
            if (this.HariPertama ==  Day.None)
                return false;

            if (this.HariKedua == Day.None)
                return false;

            if (this.HariPertama == this.HariKedua)
                return false;

            if (this.Shif== Shif.None)
                return false;

            if (this.JamMulai == new TimeSpan(0, 0, 0) || this.JamAkhir == new TimeSpan(0, 0, 0) || this.JamMulai > this.JamAkhir)
            {
                return false;
            }


                return true;
        }
        

        public Action WindowClose { get; set; }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsNew { get; private set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "HariPertama")
                    return (this.HariPertama== Day.None || this.HariPertama == this.HariKedua )? "Pilih hari" : null;
                if (columnName == "HariKedua")
                    return (this.HariKedua == Day.None ||this.HariPertama==this.HariKedua) ? "Pilih hari" : null;

                if (columnName == "Shif")
                    return (this.Shif== Shif.None )? "Pilih Shif" : null;

                if (columnName == "Mulai")
                    return this.JamMulai ==new TimeSpan(0,0,0) ? "Tentukan Jam Mulai" : null;

                if (columnName == "Akhir")
                    return (this.JamAkhir == new TimeSpan(0, 0, 0)||this.JamMulai>this.JamAkhir) ? "Tentukan Jam Berakhir" : null;
                return null;
            }
        }
        #endregion
    }
}
