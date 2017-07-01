using Appcucidarah.BaseCollection;
using Appcucidarah.Models.Data;
using DAL;
using FirstFloor.ModernUI.Windows.Controls;
using System;

namespace Appcucidarah.ViewModels.Messages
{
    internal class NewMessageVM:kotakterkirim
    {
        private string _nt;
        private kontak _selected;

        public NewMessageVM()
        {
            this.Collection = Helper.GetMainContex().Contacts;
            this.SendCommand = new CommandHandler { CanExecuteAction = x => { return !string.IsNullOrEmpty(NomorTelepon); }, ExecuteAction = x => {
                var pesan = new OcphSMSLib.Models.SMSMessage { DestinationNumber = this.NomorTelepon, MessageText = this.IsiPesan , DateTime=DateTime.Now};
                Helper.GetMainContex().Modem.SendMessage(pesan);
                ModernDialog.ShowMessage("Pesan Dikirim","Perhatian", System.Windows.MessageBoxButton.OK);
            } };
        }

        public Contacts Collection { get;  set; }
        public string NomorTelepon
        {
            get { return _nt; }
            set { _nt = value;
                OnPropertyChange("NomorTelepon");
            }
        }

        public kontak SelectedItem
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if(value!=null)
                {
                    this.IdKontak= _selected.IdKontak;
                    this.NomorTelepon = _selected.NomorTelepon;
                }else
                {
                    this.IdKontak = 0;
                }
                OnPropertyChange("SelectedItem");
            }
        }

        public CommandHandler SendCommand { get; private set; }
    }
}