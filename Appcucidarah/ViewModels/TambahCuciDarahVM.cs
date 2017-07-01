using Appcucidarah.Models.Data;
using System;
using System.Linq;
using System.Collections;
using System.Windows.Data;

namespace Appcucidarah.ViewModels
{
    public class TambahCuciDarahVM
    {
        public perawat Nurse  { get; set; }
        public CommandHandler SaveCommand { get; private set; }
        public Action WindowClose { get; internal set; }
        public CommandHandler CancelCommand { get; private set; }
        public CollectionView NurseSourceView { get; private set; }

        public TambahCuciDarahVM(pasien pacientSelected)
        {
            Pacient= pacientSelected;
            SaveCommand = new CommandHandler { CanExecuteAction = SaveCommandValidate, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = CancelAction };
            NurseSourceView = Helper.GetMainContex().Nurses.SourceView;

        }

        private void CancelAction(object obj)
        {
            Nurse = null;
            WindowClose();
        }

        public pasien Pacient { get; private set; }

        private void SaveCommandAction(object obj)
        {
            WindowClose();
        }

        private bool SaveCommandValidate(object obj)
        {
            if (Nurse != null)
                return true;
            else
                return false;
        }
    }
}