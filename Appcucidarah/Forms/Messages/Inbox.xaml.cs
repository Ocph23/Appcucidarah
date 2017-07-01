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
using System.Diagnostics;
using FirstFloor.ModernUI.Windows.Controls;
using Appcucidarah.ViewModels.Messages;

namespace Appcucidarah.Forms.Messages
{
    /// <summary>
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : UserControl,IContent
    {
        private InboxVM viewmodel;

        public Inbox()
        {
            InitializeComponent();
            this.viewmodel = new ViewModels.Messages.InboxVM();
            this.DataContext = viewmodel;
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            Debug.WriteLine("StepsControl- OnNavigatingFragment " + e.Fragment);
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            Debug.WriteLine("StepsControl- OnNavigatingFrom " + e.Source);
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            Debug.WriteLine("StepsControl- OnNavigatingTO " + e.Source);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Debug.WriteLine("StepsControl- OnNavigatingCancelFrom " +e.Source);
        }

    }
}
