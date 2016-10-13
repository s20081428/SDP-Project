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

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for report.xaml
    /// </summary>
    public partial class report : Page
    {
        private string staffID;
        public report(string staffID)
        {
            this.staffID = staffID;
            InitializeComponent();
        }

        private void btn_dataReport_Click(object sender, RoutedEventArgs e)
        {
            report_Cust nextPage = new report_Cust();
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            nextPage.Top = (primScreenHeight - nextPage.Height) / 2;
            nextPage.Left = (primScreenWidth - nextPage.Width) / 2;
            nextPage.Show();
        }

        private void btn_flightbooking_confirm_Click(object sender, RoutedEventArgs e)
        {
            label8.Visibility = Visibility.Collapsed;
            btn_dataReport.Visibility = Visibility.Collapsed;
            btn_flightbooking_confirm.Visibility = Visibility.Collapsed;

            flight_searchForCust nextPage = new flight_searchForCust();
            frame.Content = nextPage;
        }
    }
}
