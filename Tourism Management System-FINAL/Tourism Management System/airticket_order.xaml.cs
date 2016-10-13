using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
    /// Interaction logic for airticket_order.xaml
    /// </summary>
    public partial class airticket_order : Window
    {
        private DateTime FightDate;
        private string FightNo;
        private string classtype;
        private string From;
        private string To;
        private double adult_price;
        private double child_price;
        private double infant_price;
        private string brithYear;
        private string staffID;
        private string custID;

        public airticket_order(DateTime FightDate, string FightNo, string classtype, string From, string To, string adult_price, string child_price, string infant_price, string staffID)
        {
            this.FightDate = FightDate;
            this.classtype = classtype;
            this.FightNo = FightNo;
            this.From = From;
            this.To = To;
            this.adult_price = Double.Parse(adult_price);
            this.child_price = Double.Parse(child_price);
            this.infant_price = Double.Parse(infant_price);
            this.staffID = staffID;
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            //Close airticket window if user clicked Cancel button
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_Person_Adult.IsEnabled = false;
            txt_Person_Child.IsEnabled = false;
            txt_Person_Infant.IsEnabled = false;
            txt_fightdate.Text = FightDate.Date.ToString("dd/MM/yyyy");
            txt_departtime.Text = FightDate.ToString("hh:mm tt");
            txt_Class.Text = classtype;
            txt_fightNo.Text = FightNo;
            txt_From.Text = From;
            txt_to.Text = To;
            txt_price_Adult.Text = "$" + adult_price;
            txt_price_Child.Text = "$" + child_price;
            txt_price_Infant.Text = "$" + infant_price;
            txt_Person_Adult.Text = null;
            txt_Person_Child.Text = null;
            txt_Person_Infant.Text = null;

            //Get Tax from DB and add to textbox
            using (var classicContext = new project_dbEntities())
            {
                var getTax = (from list in classicContext.flightclasses
                              where list.FlightNo == FightNo
                              select new { list.Tax }).FirstOrDefault();
                decimal tax = getTax.Tax;
                txt_Tax_Adult.Text = txt_Tax_Child.Text = txt_Tax_Infant.Text = Convert.ToString(tax);
            }
        }

        private void txt_Person_Adult_TextChanged(object sender, RoutedEventArgs e)
        {
            
                //Call personchanged method while the 'adult' person textbox changed
                personchanged();
            
        }

        private void txt_Person_Child_TextChanged(object sender, RoutedEventArgs e)
        {
           
                //Call personchanged method while the 'child' person textbox changed
                personchanged();
            
        }

        private void txt_Person_Infant_TextChanged(object sender, RoutedEventArgs e)
        {
            
                //Call personchanged method while the 'infant' person textbox changed
                personchanged();
            
        }

        private void personchanged()
        {
            int person_child;
            int person_infant;
            int person_adult;

            
            try
            {
                //get number of adult from 'txt_Person_Adult'.
                person_adult = int.Parse(txt_Person_Adult.Text);
            }
            catch (FormatException)
            {
                //if no number in 'txt_Person_Adult' set number of adult is 0 and show to 'txt_Person_Adult'.
                person_adult = 0;
                txt_Person_Adult.Text = person_adult.ToString();

            }
            //Calculate totel adult price with tax fee.
            double price_adult = adult_price + (adult_price * (double.Parse(txt_Tax_Adult.Text) / 100));

            try
            {
                //get number of child from 'txt_Person_Adult'.
                person_child = int.Parse(txt_Person_Child.Text);
            }
            catch (FormatException)
            {
                //if no number in 'txt_Person_Child' set number of child is 0 and show to 'txt_Person_Child'.
                person_child = 0;
                txt_Person_Child.Text = person_child.ToString();
            }
            //Calculate totel child price with tax fee.
            double price_child = child_price + (child_price * (double.Parse(txt_Tax_Child.Text) / 100));

            try
            {
                //get number of Infant from 'txt_Person_Infant'.
                person_infant = int.Parse(txt_Person_Infant.Text);
            }
            catch (FormatException)
            {
                //if no number in 'txt_Person_Infant' set number of infant is 0 and show to 'txt_Person_Infant'.
                person_infant = 0;
                txt_Person_Infant.Text = person_infant.ToString();
            }

            //Calculate totel infant price with tax fee.
            double price_infant = infant_price + (infant_price * (double.Parse(txt_Tax_Infant.Text) / 100));

            //Caculate totel cost 
            double total = person_adult * price_adult + person_child * price_child + person_infant * price_infant;


            //10% discount for customer who brithYear is 1976,1977,1985,1911 and $150 discount for new customer
            if ((brithYear == "1976" || brithYear == "1977" || brithYear == "1985" || brithYear == "1991") && isNewCustomer() == true)
            {
                lab_discount.Content = "1. $150 discount for new customer \n2. 10 % extra discount for the year of birth is " + brithYear + ".";
                if (total > 0)
                {
                    txt_subtotal.Text = "$" + (total * 0.9 - 150).ToString("#0.0");
                    txt_original_price.Text = "$" + total.ToString("#0.0");
                    txt_discount.Text = "-$" + (total - (total * 0.9 - 150)).ToString("#0.0");
                }
                else
                {
                    txt_subtotal.Text = "$" + 0;
                    txt_original_price.Text = "$" + 0;
                    txt_discount.Text = "$" + 0;
                }
            }

            //10% discount for customer who brithYear is 1976,1977,1985,1911 only
            else if ((brithYear == "1976" || brithYear == "1977" || brithYear == "1985" || brithYear == "1991") && isNewCustomer() == false)
            {
                lab_discount.Content = "1. 10 % extra discount for the year of birth is " + brithYear + ".";
                if (total > 0)
                {
                    txt_subtotal.Text = "$" + (total * 0.9).ToString("#0.0");
                    txt_original_price.Text = "$" + total.ToString("#0.0");
                    txt_discount.Text = "-$" + (total - total * 0.9).ToString("#0.0"); 
                }
                else
                {
                    txt_subtotal.Text = "$" + 0;
                    txt_original_price.Text = "$" + 0;
                    txt_discount.Text = "$" + 0;
                }
            }

            //$150 discount for new customer only
            else if (isNewCustomer() == true)
            {
                lab_discount.Content = "1. $150 discount for new customer.";
                if (total > 0)
                {
                    txt_subtotal.Text = "$" + (total - 150).ToString("#0.0");
                    txt_original_price.Text = "$" + total.ToString("#0.0");
                    txt_discount.Text = "-$" + (total - (total - 150)).ToString("#0.0");
                }
                else
                {
                     txt_subtotal.Text = "$" + 0;
                    txt_original_price.Text = "$" + 0;
                    txt_discount.Text = "$" + 0;
                }
            }

            //show total cost in 'txt_subtotal.Text' if cust hasn't any discount.
            else
            {
                txt_original_price.Text = "$" + total.ToString("#0.0");
                txt_subtotal.Text = "$" + total.ToString("#0.0");
                txt_discount.Text = "-$" + 0;
            }
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //Alert user if he/her has not select customer
            if (lb_custresult.Items.Count == 0)
            {
                MessageBox.Show("Please select a customer to order!!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Alert user if he/her has not enter a vaild number of no of people 
            else if (txt_Person_Adult.Text == "0" && txt_Person_Child.Text == "0" && txt_Person_Infant.Text == "0")
            {
                MessageBox.Show("You must enter a valid number of adults/child/infant!!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Alert user if he/her has not enter no of people 
            else if (string.IsNullOrWhiteSpace(txt_Person_Adult.Text))
            {
                MessageBox.Show("Please enter a number of adults/child/infant!!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //Insert a flight order record in flightorder table
            else
            {
                int person_adult = int.Parse(txt_Person_Adult.Text);
                int person_child = int.Parse(txt_Person_Child.Text);
                int person_infant = int.Parse(txt_Person_Infant.Text);
                string non_process_custID = lb_custresult.Items[0].ToString();
                string customer = non_process_custID.Substring(8, 4);
                decimal totalamt = decimal.Parse(txt_subtotal.Text.Substring(1, txt_subtotal.Text.Length - 1));
                string from = txt_From.Text;
                string to = txt_to.Text;


                using (var classicContext = new project_dbEntities())
                {
                    var flightbookings = classicContext.Set<flightbooking>();
                    flightbookings.Add(new flightbooking
                    {
                        FlightNo = FightNo,
                        DepDateTime = FightDate,
                        Class = classtype,
                        OrderDate = DateTime.Now,
                        StaffID = staffID,
                        CustID = customer,
                        AdultNum = person_adult,
                        ChildNum = person_child,
                        InfantNum = person_infant,
                        AdultPrice = Convert.ToInt32(adult_price),
                        ChildPrice = Convert.ToInt32(child_price),
                        InfantPrice = Convert.ToInt32(infant_price),
                        TotalAmt = totalamt,
                        From = from,
                        To = to
                    });
                    Console.WriteLine();
                    try
                    {
                        classicContext.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        throw;
                    }
                    catch (DbUpdateException dbEx)
                    {
                        throw;
                    }
                }
                MessageBox.Show(" A flight order has been successfully processed! You can generate a flight booking confirmation by going to Menu and click [Report]->[Flight Booking Confirmation]","Order successful message");
                this.Hide();
            }
        }


        
        private bool isNewCustomer()
        {
            //Check a customer is or isn't new customer
            using (var classicContext = new project_dbEntities())
            {
                var getcustfHotelbbooking = (from list in classicContext.hotelbookings where list.CustID.Equals(custID) select new { list.CustID,list.OrderDate });
                var getcustfflightbbooking = (from list in classicContext.flightbookings where list.CustID.Equals(custID) select new { list.CustID, list.OrderDate });

                if(getcustfflightbbooking.Count()==0 && getcustfHotelbbooking.Count() == 1)
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
            }
            personchanged();
            if (lb_custresult.Items.Count != 0)
            {
                txt_Person_Adult.IsEnabled = true;
                txt_Person_Child.IsEnabled = true;
                txt_Person_Infant.IsEnabled = true;
            }else
            {
                txt_Person_Adult.IsEnabled = false;
                txt_Person_Child.IsEnabled = false;
                txt_Person_Infant.IsEnabled = false;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 600.21 / 883.751;
            if (e.HeightChanged)
                this.Width = this.Height * 883.751 / 600.21;
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
