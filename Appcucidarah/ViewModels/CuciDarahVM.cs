using Appcucidarah.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Appcucidarah.Models.Data;

namespace Appcucidarah.ViewModels
{
    public class CuciDarahVM : IBaseViewModel<CuciDarahView>
    {
        public CuciDarahView Selected { get; set; }

        public ObservableCollection<CuciDarahView> Source { get; set; }
        public CollectionView SourceView { get; set; }
        public pasien PacientSelected { get; internal set; }
        public CommandHandler ProccessCommand { get; private set; }
        public CommandHandler EditCommand { get; private set; }

        public CuciDarahVM()
        {

            ProccessCommand = new CommandHandler { CanExecuteAction = x =>PacientSelected!=null, ExecuteAction = ProccessAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => PacientSelected!=null, ExecuteAction = EditAction };
            Source = new ObservableCollection<CuciDarahView>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private void EditAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void ProccessAction(object obj)
        {
            var form = new Forms.TambahCuciDarah();
            var vm = new ViewModels.TambahCuciDarahVM(this.PacientSelected);
            form.DataContext = vm;
            form.ShowDialog();
        }

        private async void CompleteTask(Task<List<CuciDarahView>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task<List<CuciDarahView>> GetCollectionFromDatabase()
        {
            List<CuciDarahView> list = new List<CuciDarahView>();
            using (var db = new OcphDbContext())
            {
                var today = DateTime.Now;
                var hari = (Day)today.DayOfWeek;
                var res = db.Jadwals.Where(O => O.HariPertama == hari || O.HariKedua == hari);
                foreach (var item in res)
                {
                    CuciDarahView view = new CuciDarahView(item);
                  
                    view.Pacients = (from a in db.JadwalPasients.Where(O => O.IdJadwal == item.IdJadwal)
                                    join p in db.Pacients.Select() on a.IdPasien equals p.IdPasien
                                    select p).ToList();
                    foreach(var pacient in view.Pacients)
                    {
                        ContactType t = ContactType.Pasient;
                        pacient.Kontak = db.Contacts.Where(O => O.UserId == pacient.IdPasien && O.TipeKontak ==t).FirstOrDefault();
                    }

                    if(view.Pacients.Count>0)
                        list.Add(view);
                }
                return Task.FromResult(list);
            }
        }
        public bool Delete(CuciDarahView t)
        {
            
            throw new NotImplementedException();
        }
    }
}
