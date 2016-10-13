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
    /// Interaction logic for attraction_order.xaml
    /// </summary>
    public partial class attraction_order : Window
    {
        private string attraction_name;
        private string duration;
        private string cancellation;
        private string city;
        private double adult_price;
        private double child_price;
        private string staffID;
        private string photo_attract;

        public attraction_order(string attraction_name, string duration, string cancellation, string city, string adult_price, string child_price,string staffID,string photo_attract)
        {
            this.attraction_name = attraction_name;
            this.duration = duration;
            this.cancellation = cancellation;
            this.city = city;
            this.adult_price = double.Parse(adult_price.Substring(1, adult_price.Length - 1));
            this.child_price = double.Parse(child_price.Substring(1, child_price.Length - 1));
            this.staffID = staffID;
            this.photo_attract = photo_attract;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_attractionName.Text = attraction_name;
            txt_Cancellation.Text = cancellation;
            txt_Duration.Text = duration;
            txt_City.Text = city;
            txt_Adult_Price.Text = "$" + Convert.ToString(adult_price);
            txt_Child_Price.Text = "$" + Convert.ToString(child_price);
            txt_Adult_Number.Text = "1";
            txt_Child_Number.Text = "0";
            im_inorder.Source = new BitmapImage(new Uri("pack://application:,,,/Images/attractionPhotos/" + photo_attract));
        }

        private void txt_Adult_Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_Adult_Subtotal.Clear();
            int parsedValue;
            int child_number = 0;

            if (txt_Adult_Number.Text == null || txt_Adult_Number.Text == "")
            {
                txt_Adult_Number.Text = "0";
            
            }
            else if (!int.TryParse(txt_Adult_Number.Text, out parsedValue))
            {
                txt_Adult_Number.Text = "";
                MessageBox.Show("You Entered Is Not a Number , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try { 
                child_number = int.Parse(txt_Child_Number.Text);
                }
                catch (FormatException)
                {

                }
            int adult_number = int.Parse(txt_Adult_Number.Text);
                txt_Adult_Subtotal.Text = "$" + adult_price * adult_number;

                txt_GrandTotal.Text = "$" + (adult_price * adult_number + child_price * child_number);
            }
        }

        private void txt_Child_Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_Child_Subtotal.Clear();
            int parsedValue;
            int adult_number = 0; 

            if (txt_Child_Number.Text == null || txt_Child_Number.Text == "")
            {
                txt_Child_Number.Text = "0";
            }
            else if (!int.TryParse(txt_Child_Number.Text, out parsedValue))
            {
                txt_Child_Number.Text = "";
                MessageBox.Show("You Entered Is Not a Number , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int child_number = int.Parse(txt_Child_Number.Text);
                try
                {
                    adult_number = int.Parse(txt_Adult_Number.Text);
                }
                catch (FormatException)
                {

                }

                txt_Child_Subtotal.Text = "$" + child_price * child_number;

                txt_GrandTotal.Text = "$" + (adult_price * adult_number + child_price * child_number);
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //Alert user if he/her has not select customer
            if (lb_custresult.Items.Count == 0)
            {
                MessageBox.Show("A customer is not be selected!, Please Try Again", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txt_Adult_Number.Text == null || txt_Adult_Number.Text == "")
            {
                MessageBox.Show("Adult number is **required** to enter , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txt_Child_Number.Text == null || txt_Child_Number.Text == "")
            {
                MessageBox.Show("Child number is **required** to enter , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Insert a hotel order record in flightorder table
            else
            {
                string attractName = attraction_name;
                string non_process_custID = lb_custresult.Items[0].ToString();
                string custID = non_process_custID.Substring(8, 4);
                int totalAmt = int.Parse(txt_GrandTotal.Text.Substring(1, txt_GrandTotal.Text.Length - 1));
                double childPrice = child_price;
                double adultPrice = adult_price;
                int childNum = int.Parse(txt_Child_Number.Text);
                int adultNum = int.Parse(txt_Adult_Number.Text);
                string c_city = city;
                DateTime orderdate = DateTime.Now.Date;

                using (var classicContext = new project_dbEntities())
                {
                    var attractbookings = classicContext.Set<attractbooking>();
                    attractbookings.Add(new attractbooking
                    {
                        Attaction = attractName,
                        City = c_city,
                        adultPrice =Convert.ToInt32(adultPrice),
                        childPrice = Convert.ToInt32(childPrice),
                        adultNum = adultNum,
                        childNum = childNum,
                        TotalAmt = totalAmt,
                        staffID = staffID,
                        custID = custID,
                        status = "Self Organized",
                        orderDate = orderdate

                    });
                    Console.WriteLine();
                    classicContext.SaveChanges();

                }
                MessageBox.Show(" A attraction order has been successfully processed! You can view a order history by going to Menu and click [Order]->[History]", "Order successful message");
                this.Hide();
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
                txt_Adult_Number.IsEnabled = true;
                txt_Child_Number.IsEnabled = true;
            }
            else
            {
                txt_Adult_Number.IsEnabled = false;
                txt_Child_Number.IsEnabled = false;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 583.517 / 944.786;
            if (e.HeightChanged)
                this.Width = this.Height * 944.786 / 583.517;
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
