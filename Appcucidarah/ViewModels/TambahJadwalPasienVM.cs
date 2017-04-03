using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows.Data;

namespace Appcucidarah.ViewModels
{
    internal class TambahJadwalPasienVM:Models.Data.jadwalpasien
    {
        private bool IsNew;

        public TambahJadwalPasienVM()
        {
            this.Load();
            this.IsNew = true;
        }

        private void Load()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
            var mw = Helper.GetMainContex();
            this.Pasients = mw.Pacients.SourceView;
            this.Jadwals = mw.Jadwals.SourceView;
        }

        private void SaveCommandAction(object obj)
        {
            var context = Helper.GetMainContex().JadwalPasiens;
            if(this.IsNew)
            {
                if(context.Insert(this))
                {
                    ModernDialog.ShowMessage("Data Berhasil Ditambah","Success", System.Windows.MessageBoxButton.OK);
                }else
                {
                    ModernDialog.ShowMessage("Data Gagal Ditambah", "Error", System.Windows.MessageBoxButton.OK);
                }
            }else
            {
                if (context.Update(this))
                {
                    ModernDialog.ShowMessage("Data Berhasil Diubah", "Success", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Data Gagal Diubah", "Error", System.Windows.MessageBoxButton.OK);
                }
            }
        }

        private bool SaveCommandValidate(object obj)
        {
            if (this.Status && this.IdJadwal > 0 && this.IdPasien > 0)
                return true;
            else
                return false;
        }

        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }
        public CollectionView Jadwals { get; private set; }
        public CollectionView Pasients { get; private set; }
        public Action WindowClose { get; internal set; }
    }
}