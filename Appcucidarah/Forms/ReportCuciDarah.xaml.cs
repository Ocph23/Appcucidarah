using Microsoft.Reporting.WinForms;
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
using System.Globalization;
using System.Threading;
using FirstFloor.ModernUI.Windows.Controls;

namespace Appcucidarah.Forms
{
    /// <summary>
    /// Interaction logic for ReportCuciDarah.xaml
    /// </summary>
    public partial class ReportCuciDarah : UserControl
    {
        public List<Models.Views.RCucidarahModel> Source { get; set; }
        ReportDataSource reportDataSource;
        public ReportCuciDarah()
        {
            InitializeComponent();
            this.Loaded += ReportCuciDarah_Loaded;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("id-ID");
        }   

        private void ReportCuciDarah_Loaded(object sender, RoutedEventArgs e)
        {
            Source = new List<Models.Views.RCucidarahModel>();
            reportViewer.LocalReport.ReportEmbeddedResource = "Appcucidarah.LCuciDarah.rdlc";
            reportDataSource =  new  ReportDataSource("DataSet1");
            // Must match the DataSet in the RDLC
            reportDataSource.Value = Source;
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {

            if (mulai.SelectedDate != null && akhir.SelectedDate != null && mulai.SelectedDate < akhir.SelectedDate)
            {
                Source.Clear();
                using (var db = new OcphDbContext())
                {
                    DateTime datemulai = mulai.SelectedDate.Value;
                    DateTime dateakhir = akhir.SelectedDate.Value;
                    var data = from c in db.CuciDarahs.Where(O=>O.JamMulai>=datemulai && O.JamMulai<=dateakhir)
                               join b in db.Pacients.Select() on c.IdPasien equals b.IdPasien
                               join p in db.Nurses.Select() on c.IdPerawat equals p.IdPerawat
                               select new Models.Views.RCucidarahModel
                               {
                                   Akhir = c.JamAkhir.TimeOfDay,
                                   Mulai = c.JamMulai.TimeOfDay,
                                   Nama = b.Nama,
                                   NomorPasien = b.NomorPasien,
                                   Perawat = p.Nama,
                                   Tanggal = c.JamMulai.ToShortDateString()
                               };
                    foreach (var item in data)
                    {
                        Source.Add(item);
                    }
                }
                reportViewer.RefreshReport();
            }
            else
                ModernDialog.ShowMessage("Tentukan Tanggal Mulai Dan tanggal Akhir Laporan", "Error", MessageBoxButton.OK);


           
        }
    }
}
