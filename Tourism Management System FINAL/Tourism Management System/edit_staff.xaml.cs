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
    /// Interaction logic for edit_staff.xaml
    /// </summary>
    public partial class edit_staff : Window
    {
        private string staffid;
        private string staffname;
        private string gender;
        private string center;
        private string email;
        private string pass;
        private string position;
        private int salary;
        private string ctype;
        private int late;

        public edit_staff(string staffid, string staffname, string gender, string center, string email, string pass, string position, int salary, string ctype, int late)
        {
            this.staffid = staffid;
            this.staffname = staffname;
            this.gender = gender;
            this.center = center;
            this.email = email;
            this.pass = pass;
            this.position = position;
            this.salary = salary;
            this.ctype = ctype;
            this.late = late;
            InitializeComponent();
        }


        private void cb_Gender_Loaded(object sender, RoutedEventArgs e)
        {
            cb_Gender.Items.Add("M");
            cb_Gender.Items.Add("F");
        }

        private void cb_Center_Loaded(object sender, RoutedEventArgs e)
        {
            cb_Center.Items.Add("A");
            cb_Center.Items.Add("B");
            cb_Center.Items.Add("C");
        }

        private void cb_Position_Loaded(object sender, RoutedEventArgs e)
        {
            cb_Position.Items.Add("Customer Service Officer");
            cb_Position.Items.Add("Center Manager");
            cb_Position.Items.Add("Admintrative");
        }

        private void cb_type_Loaded(object sender, RoutedEventArgs e)
        {
            cb_type.Items.Add("Officer");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
                txt_staffid.Text = staffid;
                txt_staffid.IsEnabled = false;
                txt_staffname.Text = staffname;
                txt_late.Text = Convert.ToString(late);
                txt_salary.Text = Convert.ToString(salary);
                txt_staffEmail.Text = email.Substring(0, email.IndexOf("@"));

                txt_staffpassword.Text = pass;

                if (gender == "M")
                {
                    cb_Gender.SelectedIndex = 0;
                }
                else
                {
                    cb_Gender.SelectedIndex = 1;
                }

                if (center == "A")
                {
                    cb_Center.SelectedIndex = 0;
                }
                else if (center == "B")
                {
                    cb_Center.SelectedIndex = 1;
                }
                else if (center == "C")
                {
                    cb_Center.SelectedIndex = 2;
                }

                if (position == "Customer Service Officer")
                {
                    cb_Position.SelectedIndex = 0;
                }
                else if (position == "Center Manager")
                {
                    cb_Position.SelectedIndex = 1;
                }
                else if (position == "Administrator")
                {
                    cb_Position.SelectedIndex = 2;
                }

                if (ctype == "Officer")
                {
                    cb_type.SelectedIndex = 0;
                }
            
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txt_salary.Text, out n);
            bool isNumeric_Name = int.TryParse(txt_staffname.Text, out n);
            if (txt_late.Text == "" || txt_salary.Text == "" || txt_staffEmail.Text == "" || txt_staffid.Text == "" || txt_staffname.Text == "" || txt_staffpassword.Text == "")
            {
                MessageBox.Show("You must enter/select all field!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (!isNumeric)
            {
                MessageBox.Show("Salary must be a number.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (isNumeric_Name)
            {
                MessageBox.Show("Name must be letter.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                using (var classicContext = new project_dbEntities())
                {
                    var staff = (from list in classicContext.staffs
                                 where list.StaffID.Equals(staffid)
                                 select list).FirstOrDefault();

                    staff.StaffID = txt_staffid.Text;
                    staff.StaffName = txt_staffname.Text;
                    staff.Gender = cb_Gender.SelectedItem.ToString();
                    staff.Center = cb_Center.SelectedItem.ToString();
                    staff.Email = txt_staffEmail.Text + "@tt.com";
                    staff.Pass = txt_staffpassword.Text;
                    staff.Position = cb_Position.SelectedItem.ToString();
                    staff.Salary = Convert.ToInt32(txt_salary.Text);
                    staff.Ctype = cb_type.SelectedItem.ToString();
                    staff.Late = Convert.ToInt32(txt_late.Text);

                    int num = classicContext.SaveChanges();
                    MessageBox.Show("The staff information was changed successfully!", "Changed Successfully");
                    this.Hide();
                }
            }
        }
    }
}
