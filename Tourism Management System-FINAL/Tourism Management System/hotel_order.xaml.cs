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
    /// Interaction logic for hotel_order.xaml
    /// </summary>
    public partial class hotel_order : Window
    {
        private string hotel_name;
        private string room_type;
        private string room_price;
        private string brithYear;
        private string staffID;
        private int hotelID;
        private int room_size;
        private DateTime checkin;
        private DateTime checkout;
        private int Bookday;
        private decimal total_hotel_price;
        private string c_room_price;
        private string custID = "";

        public hotel_order(string hotel_id, string hotel_name, string room_type, string room_price, string room_size, string staff_id)
        {
            this.hotel_name = hotel_name;
            this.room_type = room_type;
            this.room_price = "$" + room_price;
            this.staffID = staff_id;
            this.hotelID = int.Parse(hotel_id);
            this.room_size = int.Parse(room_size);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dp_Checkin.IsEnabled = false;
            dp_Checkout.IsEnabled = false;
            txt_HotelName.Text = hotel_name;
            txt_RoomType.Text = room_type;
            txt_PricePerNight.Text = room_price;
            dp_Checkin.SelectedDate = null;
            dp_Checkout.SelectedDate = null;
            c_room_price = room_price.Substring(1, room_price.Length - 1);
            txt_NumOfNight.Text = "0";
        }



        private void btn_Hotel_Submit_Click(object sender, RoutedEventArgs e)
        {
            //Alert user if he/her has not select customer
            if (lb_custresult.Items.Count == 0)
            {
                MessageBox.Show("A customer is not be selected!, Please Try Again", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (dp_Checkin.SelectedDate == null || dp_Checkout.SelectedDate == null)
            {
                MessageBox.Show("Number of Checkin/Checkout date is **required** to select , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Insert a hotel order record in flightorder table
            else
            {
                int NumOfNight = int.Parse(txt_NumOfNight.Text);
                string non_process_custID = lb_custresult.Items[0].ToString();
                string customer = non_process_custID.Substring(8, 4);
                decimal price = decimal.Parse(room_price.Substring(1, room_price.Length - 1));
                decimal totalamt = decimal.Parse(txt_hotel_subtotal.Text.Substring(1, txt_hotel_subtotal.Text.Length - 1));
                DateTime orderdate = DateTime.Now.Date;
                DateTime check_in = dp_Checkin.SelectedDate.Value;
                DateTime check_out = dp_Checkout.SelectedDate.Value;


                using (var classicContext = new project_dbEntities())
                {
                    var hotelbookings = classicContext.Set<hotelbooking>();
                    hotelbookings.Add(new hotelbooking
                    {
                        OrderDate = orderdate,
                        StaffID = staffID,
                        CustID = customer,
                        HotelID = hotelID,
                        RoomType = room_type,
                        Price = price,
                        RoomSize = room_size,
                        TotalAmt = totalamt,
                        Checkin = check_in,
                        Checkout = check_out

                    });
                    Console.WriteLine();
                    classicContext.SaveChanges();

                    var a = (from list in classicContext.rooms
                             where list.RoomType.Equals(room_type) && list.HotelID.Equals(hotelID)
                             select list).FirstOrDefault();

                    a.RoomNum = a.RoomNum - 1;

                    int num = classicContext.SaveChanges();
                }
                MessageBox.Show(" A hotel order has been successfully processed! You can view a order history by going to Menu and click [Order]->[History]", "Order successful message");
                this.Hide();
            }
        }

        private void btn_Hotel_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txt_NumOfNight_TextChanged(object sender, TextChangedEventArgs e)
        {
          

            try
            {
                personchanged();
            }
            catch (FormatException) { 
            }
        }

        private void dp_Checkin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            checkin = Convert.ToDateTime(dp_Checkin.SelectedDate);

            TimeSpan ts = checkout - checkin;
            if (dp_Checkout.SelectedDate != null && dp_Checkin.SelectedDate != null)
            {
                if (ts.Days >= 0)
                {
                    if (ts.Days == 0)
                    {
                        Bookday = 1;
                        txt_NumOfNight.Text = Bookday.ToString();
                    }
                    else
                    {
                        Bookday = ts.Days;
                        txt_NumOfNight.Text = Bookday.ToString();
                    }
                }
                else
                {
                    txt_NumOfNight.Text = null;
                    dp_Checkin.SelectedDate = null;
                    txt_hotel_subtotal.Text = "";
                    MessageBox.Show("Check-in Date cannot larger than Check-out Date  , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void personchanged()
        {
            double subtotal;
            double hotel_price = double.Parse(txt_PricePerNight.Text.Substring(1, txt_PricePerNight.Text.Length - 1));
            if ((brithYear == "1976" || brithYear == "1977" || brithYear == "1985" || brithYear == "1991") && txt_NumOfNight.Text != null && txt_NumOfNight.Text != "" && isNewCustomer(custID) == true)
                {
                    int NumOfNight = int.Parse(txt_NumOfNight.Text);
                    txt_hotel_subtotal.Text = "$" + (((hotel_price * NumOfNight) * 0.9) - 150);
                    lab_discount.Content = "1. $150 discount for new customer \n2. 10 % extra discount for the year of birth is " + brithYear + ".";
                }
                else if ((brithYear == "1976" || brithYear == "1977" || brithYear == "1985" || brithYear == "1991") && txt_NumOfNight.Text != null && txt_NumOfNight.Text != "" && isNewCustomer(custID) == false)
                {
                    int NumOfNight = int.Parse(txt_NumOfNight.Text);
                    subtotal = (((hotel_price * NumOfNight) * 0.9));
                    if (subtotal > 0)
                    {
                        txt_hotel_subtotal.Text = "$" + subtotal;
                    }
                    lab_discount.Content = "1. 10 % extra discount for the year of birth is " + brithYear + ".";
                }
                else if (txt_NumOfNight.Text != null && txt_NumOfNight.Text != "" && isNewCustomer(custID) == true)
                {
                    int NumOfNight = int.Parse(txt_NumOfNight.Text);
                   subtotal = (hotel_price * NumOfNight - 150);
                    if (subtotal > 0)
                    {
                        txt_hotel_subtotal.Text = "$" + subtotal;
                    }
                lab_discount.Content = "1. $150 discount for new customer.";
                }
                 else
                {
                    int NumOfNight = int.Parse(txt_NumOfNight.Text);
                    subtotal=  (hotel_price * NumOfNight);
                    if (subtotal > 0)
                    {
                        txt_hotel_subtotal.Text = "$" + subtotal;
                    }
                lab_discount.Content = "No discount for this customer.";
                }
        }
        private void dp_Checkout_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            checkout = Convert.ToDateTime(dp_Checkout.SelectedDate);

            TimeSpan ts = checkout - checkin;
            if (dp_Checkout.SelectedDate != null && dp_Checkin.SelectedDate != null)
            {
                if (ts.Days >= 0)
                {
                    if (ts.Days == 0)
                    {
                        Bookday = 1;
                        txt_NumOfNight.Text = Bookday.ToString();
                    }
                    else
                    {
                        Bookday = ts.Days;
                        txt_NumOfNight.Text = Bookday.ToString();
                    }
                }
                else
                {
                    txt_NumOfNight.Text = null;
                    dp_Checkout.SelectedDate = null;
                    txt_hotel_subtotal.Text = "";
                    MessageBox.Show("Check-out Date cannot less than Check-in date  , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private bool isNewCustomer(string custID)
        {
            //Check a customer is or isn't new customer
            using (var classicContext = new project_dbEntities())
            {
                var getcustfHotelbbooking = (from list in classicContext.hotelbookings where list.CustID.Equals(custID) select new { list.CustID, list.OrderDate });
                var getcustfflightbbooking = (from list in classicContext.flightbookings where list.CustID.Equals(custID) select new { list.CustID, list.OrderDate });

                if (getcustfflightbbooking.Count() == 1 && getcustfHotelbbooking.Count() == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        private void txt_cust_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if user selected a customer show custID, CustName, Brithday, MobileNo in 'lb_custresult'
            lab_discount.Content = "";
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
                    brithYear = custinfo.DateOfBirth.Year.ToString();
                    custID = custinfo.CustID;
                }

                personchanged();
            }
            
           
            if (lb_custresult.Items.Count != 0)
            {
                dp_Checkin.IsEnabled = true;
                dp_Checkout.IsEnabled = true;
            }
            else
            {
                dp_Checkin.IsEnabled = false;
                dp_Checkout.IsEnabled = false;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 627.093 / 826.055;
            if (e.HeightChanged)
                this.Width = this.Height * 826.055 / 627.093;
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
