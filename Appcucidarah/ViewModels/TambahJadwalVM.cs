using FirstFloor.ModernUI.Windows.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

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
                if (context.Insert(this))
                {
                    ModernDialog.ShowMessage("Data Berhasil Ditambah", "Seccess", MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Data Gagal Ditambah", "Error", MessageBoxButton.OK);
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


            return true;
        }
        

        public Action WindowClose { get; set; }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }


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

                if (columnName == "JamMulai")
                    return this.JamMulai ==new TimeSpan() ? "Tentukan Jam" : null;
                if (columnName == "JamMulai")
                    return this.JamAkhir <=JamMulai? "Tentukan Jam" : null;

                return null;
            }
        }
        #endregion
    }
}
