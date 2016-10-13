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
    /// Interaction logic for cruise_order.xaml
    /// </summary>
    public partial class cruise_order : Window
    {
        private string cruiseNo;
        private string cruiseName;
        private int TourDay;
        private DateTime StartDate;
        private string staffID;
        private double adult_price;
        private double child_price;

        public cruise_order(string cruiseNo, string cruiseName, int TourDay, double price, DateTime startdate, string staffID)
        {
            this.cruiseNo = cruiseNo;
            this.cruiseName = cruiseName;
            this.TourDay = TourDay;
            this.StartDate = startdate;
            this.staffID = staffID;
            this.adult_price = price;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_cruiseNo.Text = cruiseNo;
            txt_cruiseName.Text = cruiseName;
            txt_tourDay.Text = TourDay.ToString();
            txt_adult_tourfare.Text = "$" + adult_price;
            child_price = (adult_price * 0.6);
            txt_child_tourfare.Text = "$" + child_price;
            txt_StartDay.Text = StartDate.ToShortDateString();
            txt_adult_number.Text = "1";
            txt_child_number.Text = "0";
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //Alert user if he/her has not select customer
            if (lb_custresult.Items.Count == 0)
            {
                MessageBox.Show("A customer is not be selected!, Please Try Again", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txt_adult_number.Text == null || txt_adult_number.Text == "")
            {
                MessageBox.Show("Adult number is **required** to enter , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txt_child_number.Text == null || txt_child_number.Text == "")
            {
                MessageBox.Show("Child number is **required** to enter , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Insert a hotel order record in flightorder table
            else
            {
                string n_cruiseNo = cruiseNo;
                string n_cruiseName = cruiseName;
                int n_tourday = TourDay;
                DateTime n_startdate = StartDate.Date;
                int n_childNum = int.Parse(txt_child_number.Text);
                int n_adultNum = int.Parse(txt_adult_number.Text);
                int n_adultPrice = Convert.ToInt32(adult_price * n_adultNum);
                decimal n_childPrice = Convert.ToDecimal(child_price * n_childNum);
                decimal n_totalAmt = Convert.ToDecimal(n_childPrice + n_adultPrice);
                string staffid = staffID;
                string non_process_custID = lb_custresult.Items[0].ToString();
                string custID = non_process_custID.Substring(8, 4);
                DateTime orderdate = DateTime.Now.Date;

                using (var classicContext = new project_dbEntities())
                {
                    var cruisebookings = classicContext.Set<cruisebooking>();
                    cruisebookings.Add(new cruisebooking
                    {
                        cruiseNo = n_cruiseNo,
                        cruiseName = n_cruiseName,
                        TourDay = n_tourday,
                        StartDate = n_startdate,
                        ChildNum = n_childNum,
                        AdultNum = n_adultNum,
                        ChildPrice = n_childPrice,
                        AdultPrice = n_adultPrice,
                        TotalAmt = n_totalAmt,
                        staffID = staffid,
                        custID = custID,
                        orderDate = orderdate
                    });
                    Console.WriteLine();
                    classicContext.SaveChanges();

                }
                MessageBox.Show(" A cruise order has been successfully processed! You can view a order history by going to Menu and click [Order]->[History]", "Order successful message");
                this.Hide();
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txt_adult_number_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_adult_subtotal.Clear();
            int parsedValue;
            int child_number = 0;

            if (txt_adult_number.Text == null || txt_adult_number.Text == "")
            {
                txt_adult_number.Text = "0";

            }
            else if (!int.TryParse(txt_adult_number.Text, out parsedValue))
            {
                txt_adult_number.Text = "";
                MessageBox.Show("You Entered Is Not a Number , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    child_number = int.Parse(txt_child_number.Text);
                }
                catch (FormatException)
                {

                }
                int adult_number = int.Parse(txt_adult_number.Text);
                txt_adult_subtotal.Text = "$" + adult_price * adult_number;

                txt_GrandTotal.Text = "$" + (adult_price * adult_number + child_price * child_number);
            }
        }

        private void txt_child_number_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_child_subtotal.Clear();
            int parsedValue;
            int adult_number = 0;

            if (txt_child_number.Text == null || txt_child_number.Text == "")
            {
                txt_child_number.Text = "0";
            }
            else if (!int.TryParse(txt_child_number.Text, out parsedValue))
            {
                txt_child_number.Text = "";
                MessageBox.Show("You Entered Is Not a Number , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int child_number = int.Parse(txt_child_number.Text);
                try
                {
                    adult_number = int.Parse(txt_adult_number.Text);
                }
                catch (FormatException)
                {

                }

                txt_child_subtotal.Text = "$" + child_price * child_number;

                txt_GrandTotal.Text = "$" + (adult_price * adult_number + child_price * child_number);
            }
        }

        private void txt_cust_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if user selected a customer show custID, CustName, Brithday, MobileNo in 'lb_custresult'
            lb_custresult.Items.Clear();
            string search_cust = txt_cust.Text;
            using (var classicContext = new project_dbEntities())
            {
                var getcustinfo = (from list in classicContext.customers where list.CustID.Equals(search_cust) || list.MobileNo.Equals(search_cust) select new { list.CustID, list.Surname, list.GivenName, list.DateOfBirth, list.MobileNo });
                foreach (var custinfo in getcustinfo.ToList())
                {
                    lb_custresult.Items.Add("CustID: " + custinfo.CustID);
                    lb_custresult.Items.Add("CustName: " + custinfo.Surname + " " + custinfo.GivenName);
                    lb_custresult.Items.Add("Brithday: " + custinfo.DateOfBirth.Date.ToShortDateString());
                    lb_custresult.Items.Add("MobileNo: " + custinfo.MobileNo);

                }
            }
            if (lb_custresult.Items.Count != 0)
            {
                txt_child_number.IsEnabled = true;
                txt_adult_number.IsEnabled = true;
            }
            else
            {
                txt_child_number.IsEnabled = false;
                txt_adult_number.IsEnabled = false;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 532.624 / 856.65;
            if (e.HeightChanged)
                this.Width = this.Height * 856.65 / 532.624;
        }

        private void btn_addCust_Click(object sender, RoutedEventArgs e)
        {
            add_customer next = new add_customer();
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            next.Top = (primScreenHeight - next.Height) / 2;
            next.Left = (primScreenWidth - next.Width) / 2;
            next.Show();
        }
    }
}
