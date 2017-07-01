using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;

namespace Appcucidarah.ViewModels
{
    public class PasienVM
    {
        #region Properties
        public IBaseViewModel<pasien> CollectionData { get; set; }

        public CommandHandler TambahCommand { get; set; }
        public CommandHandler EditCommand { get; private set; }
        public CommandHandler HapusCommand { get; private set; }
        #endregion

        #region Construktor

        public PasienVM()
        {

            CollectionData = Helper.GetMainContex().Pacients;

            TambahCommand = new CommandHandler { CanExecuteAction = TambahCommandValidate, ExecuteAction = x => TambahCommandAction() };
            EditCommand = new CommandHandler
            {
                CanExecuteAction = x => { return CollectionData.Selected != null ? true : false; },
                ExecuteAction = x => EditCommandAction()
            };

            HapusCommand = new CommandHandler { CanExecuteAction = x => { return CollectionData.Selected != null ? true : false; }, ExecuteAction = HapusCommandAction };

        }
        #endregion

        #region CommandAction
        private void HapusCommandAction(object obj)
        {
            var dlg = new ModernDialog { Title = "Pertanyaan", Content = "Yakin Menghapus Data ?" };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {
                var ctx = Helper.GetMainContex().Pacients;
                try
                {
                    ctx.Delete(CollectionData.Selected);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message,"Error", System.Windows.MessageBoxButton.OK);
                }
               
            }


        }
        private void EditCommandAction()
        {
            var form = new Forms.TambahPasien();
            var vm = new ViewModels.TambahPasienVM(CollectionData.Selected) { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
        }

        private void TambahCommandAction()
        {
            var form = new Forms.TambahPasien();
            var vm = new ViewModels.TambahPasienVM() { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
        }

        #endregion

        #region CommandValidation


        private bool TambahCommandValidate(object obj)
        {
            return true;
        }
        #endregion

    }
}
