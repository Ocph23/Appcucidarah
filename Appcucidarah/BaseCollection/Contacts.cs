namespace Appcucidarah.BaseCollection
{
    using Appcucidarah.Models.Data;
    using Appcucidarah.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;
    using System.Windows.Data;

    public class Contacts : IBaseViewModel<kontak>
    {
        public kontak Selected { get; set; }
        public ObservableCollection<kontak> Source { get; set; }
        public CollectionView SourceView { get; set; }
        public Contacts()
        {
            Source = new ObservableCollection<kontak>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }
        
        private async void CompleteTask(Task<List<kontak>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task<List<kontak>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = db.Contacts.Select().OrderBy(O => O.NamaKontak).ToList();
                foreach(var item in res)
                {
                    if(item.TipeKontak== ContactType.Dokter)
                    {
                        var r=db.Doctors.Where(O => O.IdDokter == item.UserId).FirstOrDefault();
                        item.NamaKontak = r.Nama;
                    }else if(item.TipeKontak== ContactType.Pasient)
                    {
                        var r = db.Pacients.Where(O => O.IdPasien == item.UserId).FirstOrDefault();
                        item.NamaKontak = r.Nama;
                    }else if(item.TipeKontak== ContactType.Perawat)
                    {
                        var r = db.Nurses.Where(O => O.IdPerawat == item.UserId).FirstOrDefault();
                        item.NamaKontak = r.Nama;
                    }
                }
                return Task.FromResult(res);
            }
        }

        public bool Delete(kontak t)
        {
            throw new NotImplementedException();
        }
    }
}
