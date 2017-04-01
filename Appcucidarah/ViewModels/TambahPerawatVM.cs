using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace Appcucidarah.ViewModels
{
    class TambahPerawatVM:Models.Data.perawat,IDataErrorInfo
    {
        #region Properties
        public List<Religion> Religions { get; set; }
        public List<Gender> Genders { get; set; }
        public Action WindowClose { get; set; }
        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }

        #endregion

        public TambahPerawatVM()
        {
            this.Load();
            this.IsNew = true;
        }

        private void Load()
        {
            Religions = Helper.GetEnumCollection<Religion>();

            Genders = Helper.GetEnumCollection<Gender>();

            SaveCommand = new CommandHandler
            {
                CanExecuteAction = SaveCommandValidate,
                ExecuteAction = SaveCommandAction
            };

            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
            this.Kontak = new kontak { TipeKontak = ContactType.Perawat };
        }

        public TambahPerawatVM(perawat selected)
        {
            this.Load();
            this.Nama = selected.Nama;
            this.Agama = selected.Agama;
            this.Alamat = selected.Alamat;
            this.IdPerawat = selected.IdPerawat;
            this.JenisKelamin = selected.JenisKelamin;
            this.Kontak = selected.Kontak;
            this.TanggalLahir = selected.TanggalLahir;
            this.TempatLahir = selected.TempatLahir;
            this.IsNew = false;
        }
        #region Validate
        private bool SaveCommandValidate(object obj)
        {
            if (this.Agama != Religion.None && !string.IsNullOrEmpty(this.Alamat) &&
                !string.IsNullOrEmpty(this.TempatLahir) && !string.IsNullOrEmpty(Kontak.NomorTelepon) &&
                 this.JenisKelamin != Gender.None && !string.IsNullOrEmpty(Nama) && this.TanggalLahir != new DateTime())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Action
        private void SaveCommandAction(object obj)
        {


            var dokterContext = Helper.GetMainContex().Nurses;

            if (IsNew)
            {
                if (dokterContext.Insert(this))
                {
                    ModernDialog.ShowMessage("Data Berhasil Ditambah", "Info", MessageBoxButton.OK);
                }
            }
            else
                if (dokterContext.Update(this))
            {
                ModernDialog.ShowMessage("Data Berhasil Diubah", "Info", MessageBoxButton.OK);
            }

        }
        #endregion

        #region IDataErrorValidate
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsNew { get; private set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Nama")
                    return string.IsNullOrEmpty(this.Nama) ? "Nama Dokter Tidak Boleh Kosong" : null;
                if (columnName == "TempatLahir")
                    return string.IsNullOrEmpty(this.TempatLahir) ? "Tempat Lahir Tidak Boleh Kosong" : null;
                if (columnName == "Agama")
                    return this.Agama == Religion.None ? "Pilih Agama" : null;
                if (columnName == "JenisKelamin")
                    return this.JenisKelamin == Gender.None ? "Pilih Jenis Kelamin" : null;
                if (columnName == "Alamat")
                    return string.IsNullOrEmpty(this.Alamat) ? "Alamat Tidak Boleh Kosong" : null;
                if (columnName == "NomorTelepon")
                    return string.IsNullOrEmpty(this.Kontak.NomorTelepon) ? "Nomor Telepon Tidak Boleh Kosong" : null;


                return null;
            }
        }
        #endregion

    }
}
