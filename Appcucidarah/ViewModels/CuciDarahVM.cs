using Appcucidarah.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Appcucidarah.Models.Data;
using DAL;

namespace Appcucidarah.ViewModels
{
    public class CuciDarahVM : BaseNotifyProperty
    {
        private DateTime _today;

      //  public CuciDarahView Selected { get; set; }
        public CollectionView SourceView { get; set; }
        public pasien PacientSelected { get; internal set; }
      
        public DateTime Today
        {
            get { return _today; }
            set { _today = value;
                this.SourceView.Refresh();
                OnPropertyChange("Today");

            }
           
        }

        public CuciDarahVM()
        {
            var pasientContext = Helper.GetMainContex().Pacients.Source.Where(O=>O.JadwalPasien!=null).OrderBy(O=>O.JadwalPasien.Jadwal.Shif);
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(pasientContext);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("JadwalPasien.Jadwal.HeaderView");
            PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("JadwalPasien.Jadwal.Shif");
            SourceView.GroupDescriptions.Add(groupDescription);
            SourceView.GroupDescriptions.Add(groupDescription2);
            SourceView.Filter = FilterHari;

            Today = DateTime.Now;
          
            SourceView.Refresh();
        }

        public bool FilterHari(object obj)
        {
            var hari = (Day)Today.DayOfWeek;
            var pas = obj as pasien;
            if (pas != null &&pas.JadwalPasien!=null && pas.JadwalPasien.Jadwal!=null)
            {
                if (pas.JadwalPasien.Jadwal.HariPertama==hari ||pas.JadwalPasien.Jadwal.HariKedua==hari)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

    
    }
}
