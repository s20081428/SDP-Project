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
using System.Data.Entity.Validation;

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for add_customer.xaml
    /// </summary>
    public partial class add_customer : Window
    {
        private int custID;
        private string _custID;
        public add_customer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_CsutID.IsEnabled = false;
            cb_Gender.Items.Add("M");
            cb_Gender.Items.Add("F");
            using (var classicContext = new project_dbEntities())
            {
                var getlastcustid = (from list in classicContext.customers
                                   orderby list.CustID descending
                                   select new { list.CustID}).FirstOrDefault();

                custID = Convert.ToInt32(getlastcustid.CustID.Substring(1,getlastcustid.CustID.Length-1)) + 1 ;
                _custID = Convert.ToString(custID);
                
                if (_custID.Length == 2)
                {
                    txt_CsutID.Text = "C0" + _custID;
                }else
                {
                    txt_CsutID.Text = "C" + _custID;
                }
            
            }

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txt_MobileNo.Text, out n);
            bool isNumeric_Surname = int.TryParse(txt_Surename.Text, out n);
            bool isNumeric_Givenname = int.TryParse(txt_GivenName.Text, out n);
            bool isNumeric_Nationality = int.TryParse(txt_Nationality.Text, out n);
            if (txt_CsutID.Text == "" || txt_GivenName.Text == "" || txt_MobileNo.Text == "" || txt_Nationality.Text == "" || txt_Passport.Text == "" || txt_Surename.Text == "")
            {
                MessageBox.Show("You must enter/select all field!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(!isNumeric)
            {
                MessageBox.Show("Mobile no must be a number.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (isNumeric_Surname)
            {
                MessageBox.Show("Surname must be letter.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (isNumeric_Givenname)
            {
                MessageBox.Show("Given name must be letter.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (isNumeric_Nationality)
            {
                MessageBox.Show("Nationality must be letter.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                using (var classicContext = new project_dbEntities())
                {
                    var custs = classicContext.Set<customer>();
                    custs.Add(new customer
                    {
                        CustID = txt_CsutID.Text,
                        Surname = txt_Surename.Text,
                        GivenName = txt_GivenName.Text,
                        DateOfBirth =Convert.ToDateTime(dp_birth.SelectedDate.Value),
                        Gender = cb_Gender.SelectedItem.ToString(),
                        Passport = txt_Passport.Text,
                        MobileNo = txt_MobileNo.Text,
                        Nationality = txt_Nationality.Text

                    });
                    Console.WriteLine();
                    try
                    {
                        classicContext.SaveChanges();
                    }
                    catch(DbEntityValidationException ex)
                    {
                        throw;
                    }

                }
                MessageBox.Show("A customer added successfully");
                this.Hide();
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
