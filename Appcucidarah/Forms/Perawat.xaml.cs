﻿using System;
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
    /// Interaction logic for Perawat.xaml
    /// </summary>
    public partial class Perawat : Page
    {
        public Perawat()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.PerawatVM();
        }
    }
}