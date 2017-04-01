using Appcucidarah.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appcucidarah.Models.Data;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Appcucidarah.BaseCollection
{
    public class Perawats : IBaseViewModel<perawat>
    {
        public perawat Selected { get; set; }

        public ObservableCollection<perawat> Source { get; set; }

        public CollectionView SourceView { get; set; }


        public Perawats()
        {
            Source = new ObservableCollection<perawat>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private async void CompleteTask(Task<List<perawat>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task<List<perawat>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = db.Nurses.Select();
                foreach (var item in res)
                {
                    item.Kontak = db.Contacts.Where(O => O.UserId == item.IdPerawat).FirstOrDefault();
                }
                return Task.FromResult(res.OrderBy(O => O.Nama).ToList());
            }
        }

        internal bool Delete(perawat selected)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    db.Contacts.Delete(O => O.IdKontak == selected.Kontak.IdKontak);
                    db.Nurses.Delete(O => O.IdPerawat== selected.IdPerawat);
                    Source.Remove(selected);
                    SourceView.Refresh();

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
            }
        }

        internal bool Insert(perawat nurse)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    nurse.IdPerawat= db.Nurses.InsertAndGetLastID(nurse);
                    nurse.Kontak.UserId = nurse.IdPerawat;

                    nurse.Kontak.TipeKontak = ContactType.Perawat;
                    nurse.Kontak.UserId = nurse.IdPerawat;
                    nurse.Kontak.NamaKontak = nurse.Nama;
                    nurse.Kontak.IdKontak = db.Contacts.InsertAndGetLastID(nurse.Kontak);
                    this.Source.Add(nurse);
                    this.SourceView.Refresh();
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
            }
        }

        internal bool Update(perawat nurse)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    db.Contacts.Update(O => new { O.NamaKontak, O.NomorTelepon }, nurse.Kontak, O => O.IdKontak == nurse.Kontak.IdKontak);
                    db.Nurses.Update(O => new { O.Agama, O.Alamat, O.JenisKelamin, O.Nama, O.TanggalLahir, O.TempatLahir },
                        nurse, O => O.IdPerawat== nurse.IdPerawat);
                    trans.Commit();
                    var item = Source.Where(O => O.IdPerawat == nurse.IdPerawat).FirstOrDefault();
                    item.Agama = nurse.Agama;
                    item.Alamat = nurse.Alamat;
                    item.JenisKelamin =nurse.JenisKelamin;
                    item.Kontak = nurse.Kontak;

                    item.Nama = nurse.Nama;
                    item.TanggalLahir = nurse.TanggalLahir;
                    item.TempatLahir = nurse.TempatLahir;
                    this.SourceView.Refresh();

                    return true;

                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw new SystemException(e.Message);
                }
            }
        }
    }
}
