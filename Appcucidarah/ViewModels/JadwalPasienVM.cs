using Appcucidarah.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        private void HapusCommandAction(object obj)
        {
            throw new NotImplementedException();
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
