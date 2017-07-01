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
using System.Windows.Shapes;

namespace Appcucidarah
{
    /// <summary>
    /// Interaction logic for FormLogin.xaml
    /// </summary>
    public partial class FormLogin : ModernWindow
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.Message.Content = "";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var user = db.Users.Where(O => O.UserName == this.textBox.Text && O.Password == this.passwordBox.Password).FirstOrDefault();

                    if (user != null)
                    {
                        user.Kontak = db.Contacts.Where(O => O.UserId == user.Id && O.TipeKontak == ContactType.Admin).FirstOrDefault();
                        MainWindow mv = new MainWindow(user);
                        mv.Show();
                        this.Close();
                    }
                    else
                    {
                        this.Message.Content = "Invalid User Or Password .....";
                    }
                }
                catch (Exception ex)
                {

                    this.Message.Content = this.Message.Content = ex.Message;
                }
            
            }
        }
    }
}
