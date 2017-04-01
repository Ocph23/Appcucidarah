using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah.ViewModels
{
    public class JadwalVM
    {
        public JadwalVM()
        {
            TambahCommand = new CommandHandler { CanExecuteAction = TambahCommandValidate, ExecuteAction = x => TambahCommandAction() };
        }

        private void TambahCommandAction()
        {
            var form = new Forms.TambahJadwal();
            var vm = new ViewModels.TambahJadwalVM();
            form.DataContext = vm;
            form.ShowDialog();
        }

        private bool TambahCommandValidate(object obj)
        {
            return true;
        }

        public CommandHandler TambahCommand { get; set; }
    }
}
