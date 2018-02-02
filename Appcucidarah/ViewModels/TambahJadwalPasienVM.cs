using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Linq;
using System.Windows.Data;
using Appcucidarah.Models.Data;

namespace Appcucidarah.ViewModels
{
    internal class TambahJadwalPasienVM:Models.Data.jadwalpasien
    {
        private bool IsNew;
        private pasien _pacient;

        public pasien Pacient
        {
            get { return _pacient; }
            set { _pacient = value; }
        }


        public TambahJadwalPasienVM()
        {
            this.Load();
            this.IsNew = true;
        }

        public TambahJadwalPasienVM(jadwalpasien selected)
        {
            this.IsNew = false;
            Load();
            this.Selected = selected;
            var jpCloned = selected.Clone();
            this.IdJadwal = jpCloned.IdJadwal;
            this.IdJadwalPasien = jpCloned.IdJadwalPasien;
            this.IdPasien = jpCloned.IdPasien;
            this.Status = jpCloned.Status;
        }

        private void Load()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
            var mw = Helper.GetMainContex();
            this.Pasients =mw.Pacients.SourceView;
            this.Jadwals = mw.Jadwals.SourceView;
            this.Pasients.Filter = null;
            this.Jadwals.Filter = null;
        }

        private void SaveCommandAction(object obj)
        {
            
            Models.Data.jadwalpasien p = (Models.Data.jadwalpasien)this;
            
            if(Pacient!=null && p.IdJadwal>0)
            {
                
                var mw = Helper.GetMainContex();
                if(mw.Pacients.Source.Where(O=>O.JadwalPasien!=null&& O.JadwalPasien.IdJadwal==p.IdJadwal).Count()>=Helper.MaxSlot)
                {
                    ModernDialog.ShowMessage("Jumlah Maximum Telah Terpenuhi", "Error", System.Windows.MessageBoxButton.OK);
                }else
                {
                    using (var db = new OcphDbContext())
                    {
                        if (this.IsNew)
                        {
                           p.IdJadwalPasien = db.JadwalPasients.InsertAndGetLastID(p);
                            if (p.IdJadwalPasien>0)
                            {
                                var jadwalPasienCloned = p.Clone();
                                Pacient.JadwalPasien = jadwalPasienCloned;
                                ModernDialog.ShowMessage("Data Berhasil Ditambah", "Success", System.Windows.MessageBoxButton.OK);
                                this.Clear();
                            }
                            else
                            {
                                ModernDialog.ShowMessage("Data Gagal Ditambah", "Error", System.Windows.MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            var jadwalPasienCloned = p.Clone();
                            if (db.JadwalPasients.Update(O=> new {O.IdJadwal,O.IdPasien,O.Status,O.Temp},jadwalPasienCloned,O=>O.IdJadwalPasien==jadwalPasienCloned.IdJadwalPasien))
                            {
                                Selected.IdJadwal = jadwalPasienCloned.IdJadwal;
                                Selected.IdPasien = jadwalPasienCloned.IdPasien;
                                Selected.Status = jadwalPasienCloned.Status;
                                ModernDialog.ShowMessage("Data Berhasil Diubah", "Success", System.Windows.MessageBoxButton.OK);
                            }
                            else
                            {
                                ModernDialog.ShowMessage("Data Gagal Diubah", "Error", System.Windows.MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
            else
            {
                ModernDialog.ShowMessage("Pasien dan Jadwal Tidak Boleh Kosong", "Error", System.Windows.MessageBoxButton.OK);
            }

            

           
        }

        private void Clear()
        {
            this.IdJadwal = 0;
            this.IdJadwalPasien = 0;
            this.IdPasien = 0;
            this.Status = StatusJadwal.None;
            this.Temp=0;
            
        }

        private bool SaveCommandValidate(object obj)
        {
            if (this.Status!= StatusJadwal.None && this.IdJadwal > 0 && this.IdPasien > 0)
                return true;
            else
                return false;
        }

        public CommandHandler SaveCommand { get; private set; }
        public CommandHandler CancelCommand { get; private set; }
        public CollectionView Jadwals { get; private set; }
        public CollectionView Pasients { get; private set; }
        public Action WindowClose { get; internal set; }
        public jadwalpasien Selected { get; private set; }
    }
}