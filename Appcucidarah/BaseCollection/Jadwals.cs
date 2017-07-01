using Appcucidarah.Models.Data;
using Appcucidarah.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using FirstFloor.ModernUI.Windows.Controls;

namespace Appcucidarah.BaseCollection
{
    public class Jadwals : IBaseViewModel<jadwal>
    {

        public ObservableCollection<jadwal> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public jadwal Selected { get; set; }

        public Jadwals()
        {
            Source = new ObservableCollection<jadwal>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private async void CompleteTask(Task<List<jadwal>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task<List<jadwal>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = db.Jadwals.Select().OrderBy(O=>O.HariPertama).ToList();
             
                return Task.FromResult(res);
            }
        }

        public bool Delete(jadwal selected)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    db.Jadwals.Delete(O => O.IdJadwal == selected.IdJadwal);
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

        internal bool Insert(jadwal dok)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    dok.IdJadwal= db.Jadwals.InsertAndGetLastID(dok);
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

        internal bool Update(jadwal v)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    if (db.Jadwals.Update(O => new { O.HariPertama, O.HariKedua, O.JamMulai, O.JamAkhir, O.Shif },
                         v, O => O.IdJadwal == v.IdJadwal))
                    {
                        var item = Source.Where(O => O.IdJadwal == v.IdJadwal).FirstOrDefault();
                        item.HariKedua = v.HariKedua;
                        item.HariPertama = v.HariPertama;
                        item.JamAkhir = v.JamAkhir;
                        item.JamMulai = v.JamMulai;
                        item.Shif = v.Shif;
                        this.SourceView.Refresh();
                        trans.Commit();
                        return true;
                    }

                    else
                        return false;

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
            }
        }
    }
}