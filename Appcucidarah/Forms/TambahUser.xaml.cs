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
using Appcucidarah.ViewModels;

namespace Appcucidarah.Forms
{
    /// <summary>
    /// Interaction logic for TambahUser.xaml
    /// </summary>
    public partial class TambahUser : Page
    {
        private UserVM vm;

        public TambahUser()
        {
            InitializeComponent();
            this.vm = new ViewModels.UserVM();
            this.DataContext = vm;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.Password = ((PasswordBox)sender).Password;
        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            vm.PasswordConfirm = ((PasswordBox)sender).Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pwsd.Password = string.Empty;
            pwsdconfirm.Password = string.Empty;
        }
    }
}
