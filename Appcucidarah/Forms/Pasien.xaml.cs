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
using Appcucidarah.ViewModels;

namespace Appcucidarah.Forms
{
    /// <summary>
    /// Interaction logic for Pasien.xaml
    /// </summary>
    public partial class Pasien : Page,IContent
    {
        public Pasien()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.PasienVM();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
           // throw new NotImplementedException();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            var f = (Pasien)e.Content;
            var vm = (PasienVM)f.DataContext;
            vm.CollectionData.SourceView.Filter = null;
            vm.CollectionData.SourceView.Refresh();
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
           // throw new NotImplementedException();
        }
    }
}
