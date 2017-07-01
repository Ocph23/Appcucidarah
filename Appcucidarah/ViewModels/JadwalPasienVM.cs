using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Appcucidarah.ViewModels
{
    public class JadwalPasienVM:DAL.BaseNotifyProperty
    {

        #region properties
        public CollectionView SourceView { get; set; }
        public pasien Selected { get; set; }
        public CommandHandler TambahCommand { get; set; }
        public CommandHandler EditCommand { get; private set; }
        public CommandHandler HapusCommand { get; private set; }

        private string _search;

        public string Search
        {

            get { return _search; }
            set
            {
                _search = value;
                this.SourceView.Refresh();
                OnPropertyChange("Search");
            }
        }

        #endregion

        public JadwalPasienVM()
        {
            SourceView = Helper.GetMainContex().Pacients.SourceView;
            SourceView.Filter = Filter;
            SourceView.Refresh();
            TambahCommand = new CommandHandler { CanExecuteAction = TambahCommandValidate, ExecuteAction = x => TambahCommandAction() };
            EditCommand = new CommandHandler
            {
                CanExecuteAction = x => { return Selected != null && Selected.JadwalPasien!=null ? true : false; },
                ExecuteAction = x => EditCommandAction()
            };

            HapusCommand = new CommandHandler { CanExecuteAction = x => { return Selected != null ? true : false; }, ExecuteAction = HapusCommandAction };
        }

        #region Action
        public bool Filter(object obj)
        {
            var pas = obj as pasien;
            if (pas.JadwalPasien == null)
                return false;
            if (!string.IsNullOrEmpty(Search))
            {
                if (pas.NomorPasien.ToLower().Contains(Search.ToLower()) || pas.Nama.ToLower().Contains(Search.ToLower()))
                {
                    return true;
                } else 
                {
                    if (pas.JadwalPasien != null)
                    {
                        if (pas.JadwalPasien.Jadwal.HariPertama.ToString().ToLower().Contains(Search.ToLower()) ||
                            pas.JadwalPasien.Jadwal.HariKedua.ToString().ToLower().Contains(Search.ToLower()) || 
                            pas.JadwalPasien.Jadwal.Shif.ToString().ToLower().Contains(Search.ToLower()))
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                        else
                        return false;
                }
            }
            else
                return true;
        }

        private void EditCommandAction()
        {
            var form = new Forms.TambahJadwalPasien();
            var vm = new ViewModels.TambahJadwalPasienVM(Selected.JadwalPasien) { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
            this.SourceView.Refresh();
        }

        private void HapusCommandAction(object obj)
        {
            var dlg = new ModernDialog { Title = "Ask", Content = "Yakin Menghapus Data ?" };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

        }

        private void TambahCommandAction()
        {
            var form = new Forms.TambahJadwalPasien();
            var vm = new ViewModels.TambahJadwalPasienVM() { WindowClose=form.Close};
            form.DataContext = vm;
            form.ShowDialog();
            this.SourceView.Refresh();
        }
        #endregion


        private bool TambahCommandValidate(object obj)
        {
            return true;
        }
    }
}
