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

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for cruise_PDF.xaml
    /// </summary>
    public partial class cruise_PDF : Window
    {

        private string pdf;
        public cruise_PDF(string pdf)
        {
            this.pdf = pdf;
            InitializeComponent();
        }


       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wb_cruise.Navigate(new Uri("http://localhost/pdf/" + pdf));
        }
    }
}
