using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace Appcucidarah.ViewModels
{
    public class TambahPasienVM : Models.Data.pasien, IDataErrorInfo
    {
        #region properties
        public List<Religion> Religions { get; set; }
        public List<Gender> Genders { get; set; }
        public dokter SelectedDokter { get; set; }
        public Action WindowClose { get; set; }
        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }

        #endregion

        #region Constructor
        public TambahPasienVM()
        {
            this.Load();
            this.Kontak = new kontak { TipeKontak = ContactType.Pasient };
            this.IsNew = true;

        }

        private void Load()
        {

            this.Jadwals = Helper.GetMainContex().Jadwals.SourceView;
            this.Dokters = Helper.GetMainContex().Doctors.SourceView;
            Religions = Helper.GetEnumCollection<Religion>();

            Genders = Helper.GetEnumCollection<Gender>();

            SaveCommand = new CommandHandler
            {
                CanExecuteAction = SaveCommandValidate,
                ExecuteAction = SaveCommandAction
            };

            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        //Edit

        public TambahPasienVM(pasien selected)
        {
            this.Load();
            this.Nama = selected.Nama;
            this.Agama = selected.Agama;
            this.Alamat = selected.Alamat;
            this.IdDokter = selected.IdDokter;
            this.IdPasien = selected.IdPasien;
            this.JenisKelamin = selected.JenisKelamin;
            this.Kontak = selected.Kontak;
            this.TanggalLahir = selected.TanggalLahir;
            this.TempatLahir = selected.TempatLahir;
            this.NomorPasien = selected.NomorPasien;
            this.Status = selected.Status;
            this.IsNew = false;
        }


        #endregion

        #region Validate
        private bool SaveCommandValidate(object obj)
        {
            if (this.Agama != Religion.None && !string.IsNullOrEmpty(this.Alamat) && this.Status==true && 
                !string.IsNullOrEmpty(NomorPasien) && !string.IsNullOrEmpty(this.TempatLahir) && 
                !string.IsNullOrEmpty(Kontak.NomorTelepon) && this.JenisKelamin != Gender.None && 
                this.IdDokter>0 && !string.IsNullOrEmpty(Nama) && this.TanggalLahir != new DateTime())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Action
        private void SaveCommandAction(object obj)
        {

            try
            {
                var context = Helper.GetMainContex().Pacients;

                if (IsNew)
                {
                    if (context.Insert(this))
                    {
                        ModernDialog.ShowMessage("Data Berhasil Ditambah", "Info", MessageBoxButton.OK);
                    }
                }
                else if (context.Update(this))
                {
                    ModernDialog.ShowMessage("Data Berhasil Diubah", "Info", MessageBoxButton.OK);
                }

            }
            catch (Exception ex)
            {

                ModernDialog.ShowMessage(ex.Message, "Info", MessageBoxButton.OK);
            }

        }
        #endregion

        #region IDataErrorValidate
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsNew { get; private set; }
        public CollectionView Jadwals { get; private set; }
        public CollectionView Dokters { get; private set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "NomorPasien")
                    return string.IsNullOrEmpty(this.NomorPasien) ? "Nomor Pasien Tidak Boleh Kosong" : null;
                if (columnName == "Nama")
                    return string.IsNullOrEmpty(this.Nama) ? "Nama Pasien Tidak Boleh Kosong" : null;
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
                if (columnName == "Status")
                    return this.Status ? "Aktifkan Pasien" : null;
                if (columnName == "IdDokter")
                    return this.IdDokter<=0 ? "Pilih Dokter Yang Menangani Pasien" : null;


                return null;
            }
        }
        #endregion

    }

}
