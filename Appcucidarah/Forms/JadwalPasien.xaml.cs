using FirstFloor.ModernUI.Windows;
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

namespace Appcucidarah.Forms
{
    /// <summary>
    /// Interaction logic for JadwalPasien.xaml
    /// </summary>
    public partial class JadwalPasien : UserControl,IContent
    {
        public JadwalPasien()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.JadwalPasienVM();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
           
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            var f = (JadwalPasien)e.Content;
            var vm = (ViewModels.JadwalPasienVM)f.DataContext;
            vm.SourceView.Filter = vm.Filter;

            vm.SourceView.Refresh();
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
           
        }
    }
}
