using Appcucidarah.Models.Views;
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
    /// Interaction logic for CuciDarah.xaml
    /// </summary>
    public partial class CuciDarah : UserControl,IContent
    {
        private ViewModels.CuciDarahVM vm = new ViewModels.CuciDarahVM();
        public CuciDarah()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            var f = (CuciDarah)e.Content;
            var vm = (CuciDarahVM)f.DataContext;
            if(vm.SourceView.Filter==null)
            {
                vm.SourceView.Filter = vm.FilterHari;
            }
            if(vm.Today.Date!=DateTime.Now.Date)
            {
                vm.Today = DateTime.Now;
            }

        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
           // throw new NotImplementedException();
        }
    }
}
