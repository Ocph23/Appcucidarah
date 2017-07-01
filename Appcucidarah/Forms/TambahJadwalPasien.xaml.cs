using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Navigation;
using Appcucidarah.ViewModels;

namespace Appcucidarah.Forms
{
    /// <summary>
    /// Interaction logic for TambahJadwalPasien.xaml
    /// </summary>
    public partial class TambahJadwalPasien : ModernWindow,IContent
    {
        public TambahJadwalPasien()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            var f = (TambahJadwalPasien)e.Content;
            var vm = (TambahJadwalPasienVM)f.DataContext;
            vm.Pasients.Filter = null;
            vm.Jadwals.Filter = null;
            vm.Pasients.Refresh();
            vm.Jadwals.Refresh();
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
           
        }
    }
}
