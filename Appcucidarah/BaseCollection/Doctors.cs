using Appcucidarah.Models.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Appcucidarah.ViewModels;

namespace Appcucidarah.BaseCollection
{
    public class Doctors :ViewModels.IBaseViewModel<dokter>
    {

        public ObservableCollection<dokter> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public dokter Selected { get; set; }

        public Doctors()
        {
            Source = new ObservableCollection<dokter>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
           var task=  GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private async void CompleteTask(Task<List<dokter>> task)
        {
            var x = await task;
            foreach(var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task <List<dokter>>  GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = db.Doctors.Select();
                foreach(var item in res)
                {
                    var t = ContactType.Dokter;
                    item.Kontak = db.Contacts.Where(O => O.UserId == item.IdDokter && O.TipeKontak==t).FirstOrDefault();
                }
                return Task.FromResult(res.OrderBy(O=>O.Nama).ToList());
            }
        }

        internal bool Delete(dokter selected)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    db.Contacts.Delete(O => O.IdKontak == selected.Kontak.IdKontak);
                    db.Doctors.Delete(O => O.IdDokter == selected.IdDokter);
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

        internal bool Insert(dokter dok)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    dok.IdDokter = db.Doctors.InsertAndGetLastID(dok);
                    dok.Kontak.UserId = dok.IdDokter;

                    dok.Kontak.TipeKontak = ContactType.Dokter;
                    dok.Kontak.UserId = dok.IdDokter;
                    dok.Kontak.NamaKontak = dok.Nama;
                    dok.Kontak.IdKontak = db.Contacts.InsertAndGetLastID(dok.Kontak);
                    this.Source.Add(dok);
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

        internal bool Update(dokter dok)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    db.Contacts.Update(O => new { O.NamaKontak, O.NomorTelepon }, dok.Kontak, O => O.UserId== dok.IdDokter && O.TipeKontak== ContactType.Dokter);
                    db.Doctors.Update(O => new { O.Agama, O.Alamat, O.JenisKelamin, O.Nama, O.TanggalLahir, O.TempatLahir },
                        dok, O => O.IdDokter == dok.IdDokter);
                    trans.Commit();
                    var item = Source.Where(O => O.IdDokter == dok.IdDokter).FirstOrDefault();
                    item.Agama = dok.Agama;
                    item.Alamat = dok.Alamat;
                    item.JenisKelamin = dok.JenisKelamin;
                    item.Kontak = dok.Kontak;
                    
                    item.Nama = dok.Nama;
                    item.TanggalLahir = dok.TanggalLahir;
                    item.TempatLahir = dok.TempatLahir;
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
