using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Appcucidarah.ViewModels
{
    public class SettingVM : DAL.BaseNotifyProperty
    {

        private string port;

        public string Port
        {
            get { return port; }
            set { port = value;
                OnPropertyChange("Port");
            }
        }

        public CommandHandler SaveCommand { get; private set; }

        private int kuota;

        public int Kuota
        {
            get { return kuota; }
            set { kuota = value; OnPropertyChange("Kuota"); }
            
        }


        AppConfiguration config = new AppConfiguration();
        public SettingVM()
        {
            this.Kuota = config.Kuota;
            this.Port = config.DevicePort;
            this.SaveCommand = new CommandHandler { CanExecuteAction = x => Validate(), ExecuteAction = x => SaveCommandAction() };
        }

        private void SaveCommandAction()
        {
            try
            {
                config.UpdateKey("MaxSlot", Kuota.ToString());
                config.UpdateKey("DevicePort", Port.ToString());
                ModernDialog.ShowMessage("Tersimpan, Silahkan Restart Aplikasi ", "Info", MessageBoxButton.OK);
                
            }
            catch (Exception )
            {
                ModernDialog.ShowMessage("Gagal tersimpan", "Error", MessageBoxButton.OK);
            }
           
        }

        private bool Validate()
        {
            if (Kuota > 0 && !string.IsNullOrEmpty(Port))
            {
                return true;
            }
            else
                return false;
        }
    }
}
