﻿using Appcucidarah.Models.Data;
using Appcucidarah.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Appcucidarah.BaseCollection
{
    public class Pacients : IBaseViewModel<pasien>
    {

        public ObservableCollection<pasien> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public pasien Selected { get; set; }

        public Pacients()
        {
            Source = new ObservableCollection<pasien>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private async void CompleteTask(Task<List<pasien>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task<List<pasien>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = db.Pacients.Select();
                foreach (var item in res)
                {
                    var t = ContactType.Pasient;
                    item.Kontak = db.Contacts.Where(O => O.UserId == item.IdPasien && O.TipeKontak== t).FirstOrDefault();
                    item.JadwalPasien = db.JadwalPasients.Where(O => O.IdPasien == item.IdPasien).FirstOrDefault();
                }
                return Task.FromResult(res.OrderBy(O => O.Nama).ToList());
            }
        }

        public bool Delete(pasien selected)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    db.Contacts.Delete(O => O.IdKontak == selected.Kontak.IdKontak);
                    db.Pacients.Delete(O => O.IdDokter == selected.IdDokter);
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

       

        internal bool Insert(pasien p)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    p.IdPasien = db.Pacients.InsertAndGetLastID(p);
                    p.Kontak.UserId = p.IdPasien;

                    p.Kontak.TipeKontak = ContactType.Pasient;
                    p.Kontak.NamaKontak = p.Nama;
                    p.Kontak.IdKontak = db.Contacts.InsertAndGetLastID(p.Kontak);
                    this.Source.Add(p);
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

        internal bool Update(pasien p)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    var cType = ContactType.Pasient;
                   var a = db.Contacts.Update(O => new { O.NamaKontak, O.NomorTelepon }, p.Kontak, O => O.UserId == p.IdPasien && O.TipeKontak== cType);
                    var b=db.Pacients.Update(O => new { O.Agama, O.Alamat, O.JenisKelamin, O.Nama, O.TanggalLahir, O.TempatLahir,O.IdDokter,O.NomorPasien,O.Status, },
                        p, O => O.IdPasien == p.IdPasien);
                    if (a && b)
                    {
                        var item = Source.Where(O => O.IdPasien == p.IdPasien).FirstOrDefault();
                        item.Agama = p.Agama;
                        item.Alamat = p.Alamat;
                        item.JenisKelamin = p.JenisKelamin;
                        item.Kontak = p.Kontak;
                        item.IdDokter = p.IdDokter;
                        item.NomorPasien = p.NomorPasien;
                        item.Nama = p.Nama;
                        item.TanggalLahir = p.TanggalLahir;
                        item.TempatLahir = p.TempatLahir;
                        this.SourceView.Refresh();
                        trans.Commit();
                        return true;
                    }
                    else
                        throw new SystemException("Terjadi Kesalahan");

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
