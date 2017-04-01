using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;

namespace Appcucidarah.ViewModels
{
  
    public class DokterVM
    {
        #region Properties
        public IBaseViewModel<dokter> CollectionData { get; set; }

        public CommandHandler TambahCommand { get; set; }
        public CommandHandler EditCommand { get; private set; }
        public CommandHandler HapusCommand { get; private set; }
        #endregion

        #region Construktor

        public DokterVM()
        {

            CollectionData = Helper.GetMainContex().Doctors;

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

            if(dlg.DialogResult==true)
            {
                var ctx = Helper.GetMainContex().Doctors;
                ctx.Delete(CollectionData.Selected);
            }

       
        }
        private void EditCommandAction()
        {
            var form = new Forms.TambahDokter();
            var vm = new ViewModels.TambahDokterVM(CollectionData.Selected) { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
        }

        private void TambahCommandAction()
        {
            var form = new Forms.TambahDokter();
            var vm = new ViewModels.TambahDokterVM() { WindowClose = form.Close };
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
