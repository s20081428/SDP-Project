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
    /// Interaction logic for edit_cust.xaml
    /// </summary>
    public partial class edit_cust : Window
    {
        private string custID;
        private string surname;
        private string givenname;
        private DateTime dateofbirth;
        private string gender;
        private string passport;
        private string mobileno;
        private string nationality;

        public edit_cust(string custID, string surname, string givenname, DateTime dateofbirth, string gender, string passport, string mobileno, string nationality)
        {
            this.custID = custID;
            this.surname = surname;
            this.givenname = givenname;
            this.dateofbirth = dateofbirth;
            this.gender = gender;
            this.passport = passport;
            this.mobileno = mobileno;
            this.nationality = nationality;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dp_birth.Text = "";
            txt_CsutID.IsEnabled = false;
            cb_Gender.Items.Add("M");
            cb_Gender.Items.Add("F");

            txt_CsutID.Text = custID;
            txt_GivenName.Text = givenname;
            txt_MobileNo.Text = mobileno;
            txt_Nationality.Text = nationality;
            txt_Passport.Text = passport;
            txt_Surename.Text = surname;
            dp_birth.Text = Convert.ToString(dateofbirth);

            if (gender == "M")
            {
                cb_Gender.SelectedIndex = 0;
            }
            else
            {
                cb_Gender.SelectedIndex = 1;
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txt_MobileNo.Text, out n);
            bool isNumeric_Surname = int.TryParse(txt_Surename.Text, out n);
            bool isNumeric_Givenname = int.TryParse(txt_GivenName.Text, out n);
            bool isNumeric_Nationality = int.TryParse(txt_Nationality.Text, out n);

            if (txt_CsutID.Text == "" || txt_GivenName.Text == "" || txt_MobileNo.Text == "" || txt_Nationality.Text == "" || txt_Passport.Text == "" || txt_Surename.Text == "" || dp_birth.Text == "")
            {
                MessageBox.Show("You must enter/select all field!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!isNumeric)
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
                    var cust = (from list in classicContext.customers
                                where list.CustID.Equals(txt_CsutID.Text)
                                select list).FirstOrDefault();

                    cust.CustID = txt_CsutID.Text;
                    cust.Surname = txt_Surename.Text;
                    cust.GivenName = txt_GivenName.Text;
                    cust.DateOfBirth = Convert.ToDateTime(dp_birth.Text);
                    cust.Gender = cb_Gender.SelectedItem.ToString();
                    cust.Passport = txt_Passport.Text;
                    cust.MobileNo = txt_MobileNo.Text;
                    cust.Nationality = txt_Nationality.Text;


                    int num = classicContext.SaveChanges();
                    MessageBox.Show("The customer information was changed successfully!", "Changed Successfully");
                    this.Hide();
                }
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
