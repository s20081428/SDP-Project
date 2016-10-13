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
using System.Windows.Forms;

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for dbError.xaml
    /// </summary>
    public partial class dbError : Window
    {

       



        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;
        const uint MF_ENABLED = 0x00000000;

        const uint SC_CLOSE = 0xF060;

        const int WM_SHOWWINDOW = 0x00000018;
        const int WM_CLOSE = 0x10;

        private int second;
        private Timer timer1 = new Timer();//global
        public dbError()

        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }


       
    }
}

