using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Appcucidarah.ViewModels
{
    public class JadwalPasienVM
    {

        #region properties
        public IBaseViewModel<jadwalpasien> CollectionData { get; set; }

        public CommandHandler TambahCommand { get; set; }
        public CommandHandler EditCommand { get; private set; }
        public CommandHandler HapusCommand { get; private set; }
        #endregion

        public JadwalPasienVM()
        {
            CollectionData = Helper.GetMainContex().JadwalPasiens;

            TambahCommand = new CommandHandler { CanExecuteAction = TambahCommandValidate, ExecuteAction = x => TambahCommandAction() };
            EditCommand = new CommandHandler
            {
                CanExecuteAction = x => { return CollectionData.Selected != null ? true : false; },
                ExecuteAction = x => EditCommandAction()
            };

            HapusCommand = new CommandHandler { CanExecuteAction = x => { return CollectionData.Selected != null ? true : false; }, ExecuteAction = HapusCommandAction };
        }

        #region Action

        
        private void EditCommandAction()
        {
            var form = new Forms.TambahJadwalPasien();
            var vm = new ViewModels.TambahJadwalPasienVM(CollectionData.Selected) { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
        }

        private void HapusCommandAction(object obj)
        {
            var dlg = new ModernDialog { Title = "Ask", Content = "Yakin Menghapus Data ?" };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {

                if (CollectionData.Delete(CollectionData.Selected))
                {
                    ModernDialog.ShowMessage("Data Berhasil Dihapus", "Success", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Data Gagal Dihapus", "Error", System.Windows.MessageBoxButton.OK);
                }
            }

        }

        private void TambahCommandAction()
        {
            var form = new Forms.TambahJadwalPasien();
            var vm = new ViewModels.TambahJadwalPasienVM() { WindowClose=form.Close};
            form.DataContext = vm;
            form.ShowDialog();
        }
        #endregion


        private bool TambahCommandValidate(object obj)
        {
            return true;
        }
    }
}
