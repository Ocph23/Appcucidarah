using Appcucidarah.Models.Data;
using System;

namespace Appcucidarah.ViewModels
{
    public class TambahCuciDarahVM:Models.Data.cucidarah
    {
        private string _saveContent;

        public pasien Pacient { get; set; }
        public CommandHandler SaveCommand { get; private set; }
        public ProccessStatus ProccessStatus { get; private set; }
        public string SaveCommandContent {
            get { return _saveContent; }
            set
            {
                _saveContent = value;
                OnPropertyChange("SaveCommandContent");
            }
        }

        public TambahCuciDarahVM()
        {
        }

        public TambahCuciDarahVM(pasien pacientSelected)
        {
            Pacient= pacientSelected;
            this.Load();
           
        }

        private void Load()
        {
            this.ProccessStatus = ProccessStatus.New;
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
        }

        private void SaveCommandAction(object obj)
        {
            if(ProccessStatus.New== ProccessStatus)
            {
                ProccessStatus = ProccessStatus.Start;
                this.JamMulai = DateTime.Now;
                //SaveCommandContent = "Start";
            }else
            if (ProccessStatus.Start == ProccessStatus)
            {
                ProccessStatus = ProccessStatus.Stop;
                this.JamAkhir = DateTime.Now;
            }
            else
            if (ProccessStatus.Stop == ProccessStatus)
            {
                ProccessStatus = ProccessStatus.Save;
                //SaveCommandContent = "Start";
            }else
            if (ProccessStatus.Save == ProccessStatus)
            {
                ProccessStatus = ProccessStatus.New;
                //SaveCommandContent = "Start";
            }


        }

        private bool SaveCommandValidate(object obj)
        {
           if(ProccessStatus== ProccessStatus.New)
                SaveCommandContent = "Start";
            if (ProccessStatus == ProccessStatus.Start)
                SaveCommandContent = "Stop";
            if (ProccessStatus == ProccessStatus.Stop)
                SaveCommandContent = "Simpan";



            return true;
        }
    }
}