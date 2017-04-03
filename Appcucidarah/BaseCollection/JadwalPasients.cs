using Appcucidarah.Models.Data;
using Appcucidarah.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Appcucidarah.BaseCollection
{
    public class JadwalPasients:IBaseViewModel<jadwalpasien>
    {

        public ObservableCollection<jadwalpasien> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public jadwalpasien Selected { get; set; }

        public JadwalPasients()
        {
            Source = new ObservableCollection<jadwalpasien>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private async void CompleteTask(Task<List<jadwalpasien>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task<List<jadwalpasien>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = from a in db.JadwalPasients.Select()
                          join b in db.Jadwals.Select() on a.IdJadwal equals b.IdJadwal
                          join c in db.Pacients.Select() on a.IdPasien equals c.IdPasien
                          select new jadwalpasien { IdJadwal=a.IdJadwal, IdJadwalPasien=a.IdJadwalPasien, IdPasien=a.IdPasien,
                           Jadwal=b, Pacient=c, Status=a.Status};

                return Task.FromResult(res.ToList());
            }
        }

        internal bool Delete(jadwalpasien selected)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    db.JadwalPasients.Delete(O => O.IdJadwalPasien == selected.IdJadwalPasien);
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

        internal bool Insert(jadwalpasien jad)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    jad.IdJadwal = db.JadwalPasients.InsertAndGetLastID(jad);
                    this.Source.Add(jad);
                    this.SourceView.Refresh();
                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
                    return false;
                }
            }
        }

        internal bool Update(jadwalpasien v)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    if (db.JadwalPasients.Update(O => new {O.IdJadwal,O.IdPasien,O.Status },
                         v, O => O.IdJadwalPasien == v.IdJadwalPasien))
                    {
                        var item = Source.Where(O => O.IdJadwalPasien == v.IdJadwalPasien).FirstOrDefault();
                        item.IdJadwal = v.IdJadwal;
                        item.IdPasien = v.IdPasien;
                        item.Status = v.Status;
                        this.SourceView.Refresh();
                        trans.Commit();
                        return true;
                    }

                    else
                        return false;

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
