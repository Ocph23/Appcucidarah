using Appcucidarah.Models.Views;
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

namespace Appcucidarah.Forms
{
    /// <summary>
    /// Interaction logic for CuciDarah.xaml
    /// </summary>
    public partial class CuciDarah : Page
    {
        private ViewModels.CuciDarahVM vm = new ViewModels.CuciDarahVM();
        public CuciDarah()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeItem = sender as TreeView;
       
            var TreeItemSelected = treeItem.SelectedItem;
            
            if (treeItem.SelectedItem.GetType()==typeof(Models.Data.pasien))
            {
                vm.PacientSelected = (Models.Data.pasien)treeItem.SelectedItem;
                foreach(CuciDarahView item in treeItem.ItemsSource)
                {
                    foreach(var item2 in item.Pacients)
                    {
                        if(item2.Equals(vm.PacientSelected))
                        {
                            vm.Selected = item;
                        }
                    }
                }
            }else
            {
                vm.PacientSelected = null;
                vm.Selected = (CuciDarahView)treeItem.SelectedItem;
            }
           

        }
    
    }
}
