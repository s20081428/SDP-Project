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
    /// Interaction logic for flight_confirm.xaml
    /// </summary>
    public partial class flight_confirm : Window
    {
        private string user;
        private int BookingID;
        private DateTime Orderdate;
        private string FlightNo;
        private DateTime DepdateTime;
        private string classtype;
        private string price;
        private int AdultNum;
        private int ChildNum;
        private int InfantNum;
        private string CustID;
        private decimal AdultPrice;
        private decimal ChildPrice;
        private decimal InfantPrice;
        private string from;
        private string to;

        public flight_confirm(int BookingID, DateTime Orderdate, string FlightNo, DateTime DepdateTime, string classtype, string price, int AdultNum, int ChildNum, int InfantNum, string CustID, decimal AdultPrice,decimal ChildPrice, decimal InfantPrice,string from, string to)
        {
            this.BookingID = BookingID;
            this.Orderdate = Orderdate;
            this.FlightNo = FlightNo;
            this.DepdateTime = DepdateTime;
            this.classtype = classtype;
            this.price = price;
            this.AdultNum = AdultNum;
            this.ChildNum = ChildNum;
            this.InfantNum = InfantNum;
            this.CustID = CustID;
            this.AdultPrice = AdultPrice;
            this.ChildPrice = ChildPrice;
            this.InfantPrice = InfantPrice;
            this.from = from;
            this.to = to;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_bookingID.Text =Convert.ToString(BookingID);
            txt_orderDate.Text = Orderdate.Date.ToShortDateString();
            txt_flightNo.Text = FlightNo;
            txt_departDate.Text = DepdateTime.Date.ToShortDateString();
            txt_departTime.Text = DepdateTime.TimeOfDay.ToString();
            txt_Class.Text = classtype;
            txt_total.Text = "$" + price;
            txt_cust.Text = CustID;
            txt_from.Text = from;
            txt_To.Text = to;

            txt_person_Adult.Text = Convert.ToString(AdultNum);
            txt_person_Child.Text = Convert.ToString(ChildNum);
            txt_person_Infant.Text = Convert.ToString(InfantNum);

            txt_price_Adult.Text = "$" + AdultPrice;
            txt_price_Child.Text = "$" + ChildPrice;
            txt_price_Infant.Text = "$" + InfantPrice;

            
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            btn_print.Visibility = Visibility.Collapsed;
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(this, "");
            }
            else
            {
                btn_print.Visibility = Visibility.Visible;
            }
            btn_print.Visibility = Visibility.Visible;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 438 / 748;
            if (e.HeightChanged)
                this.Width = this.Height * 748 / 438;
        }
    }
}
