using Appcucidarah.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Appcucidarah.BaseCollection;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;
using System.Windows;

namespace Appcucidarah.ViewModels
{
    public class JadwalVM
    {

        #region Properties


        public CommandHandler TambahCommand { get; set; }
        public CommandHandler EditCommand { get; private set; }
        public CommandHandler DeleteCommand { get; private set; }
        public Jadwals CollectionData { get; private set; }
        #endregion

        #region Constructor
        public JadwalVM()
        {
            this.CollectionData = Helper.GetMainContex().Jadwals;
            TambahCommand = new CommandHandler { CanExecuteAction = TambahCommandValidate, ExecuteAction = x => TambahCommandAction() };
            EditCommand = new CommandHandler { CanExecuteAction = EditCommandValidate, ExecuteAction = EditCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => { return CollectionData.Selected != null; }, ExecuteAction = DeleteCommandAction };
        }
        #endregion

        #region Validate
        private bool EditCommandValidate(object obj)
        {
            if (CollectionData.Selected != null)
                return true;
            else
                return false;
        }

        private bool TambahCommandValidate(object obj)
        {
            return true;
        }

        #endregion

        #region Action
        private void DeleteCommandAction(object obj)
        {
            var dlg = new ModernDialog { Title = "Pertanyaan", Content = "Yakin Menghapus Data ?" };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {
                if (CollectionData.Delete(CollectionData.Selected))
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Data Berhasil Dihapus", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
           
        }

        private void EditCommandAction(object obj)
        {
            var form = new Forms.TambahJadwal();
            var vm = new ViewModels.TambahJadwalVM(CollectionData.Selected) { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
        }

        private void TambahCommandAction()
        {
            var form = new Forms.TambahJadwal();
            var vm = new ViewModels.TambahJadwalVM() { WindowClose=form.Close};
            form.DataContext = vm;
            form.ShowDialog();
        }
        #endregion






    }
}
