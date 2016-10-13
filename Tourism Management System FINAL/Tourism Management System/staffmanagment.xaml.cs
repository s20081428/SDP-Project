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
    /// Interaction logic for staffmanagment.xaml
    /// </summary>
    public partial class staffmanagment : Page
    {
        public staffmanagment()
        {
            InitializeComponent();
        }

        private void btn_addStaff_Click(object sender, RoutedEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txt_salary.Text, out n);
            bool isNumeric_Name = int.TryParse(txt_staffname.Text, out n);
            if (txt_salary.Text == "" || txt_staffid.Text == "" || txt_staffname.Text == "" || txt_staffpassword.Text == "" || cb_Center.SelectedItem == null || cb_Gender.SelectedItem == null || cb_Position.SelectedItem == null || cb_type.SelectedItem == null || txt_staffEmail.Text=="" )
            {
                MessageBox.Show("Please make sure you have entered all field.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }else if (!isNumeric)
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
                    var staffs = classicContext.Set<staff>();
                    staffs.Add(new staff
                    {
                        StaffID = txt_staffid.Text,
                        StaffName = txt_staffname.Text,
                        Gender = cb_Gender.SelectedItem.ToString(),
                        Center = cb_Center.SelectedItem.ToString(),
                        Email = txt_staffEmail.Text + "@tt.com",
                        Pass = txt_staffpassword.Text,
                        Position = cb_Position.SelectedItem.ToString(),
                        Salary = Convert.ToInt32(txt_salary.Text),
                        Ctype = cb_type.SelectedItem.ToString(),
                        Late = 0
                    });
                    Console.WriteLine();
                    classicContext.SaveChanges();

                }
                MessageBox.Show("staff added successfully");
            }
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

        private void dg_editstaff_Loaded(object sender, RoutedEventArgs e)
        {
            using (var classicContext = new project_dbEntities())
            {
                var staff = (from staffs in classicContext.staffs
                                     select new { staffs });


                foreach (var output in staff.ToList())
                {
                    dg_editstaff.Items.Add(output.staffs);
                }
            }
        }

        private void btn_delete_staff(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure delete this staff?", "Alert message", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                object row = dg_editstaff.SelectedItem;
                string staffid = (dg_editstaff.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
                using (var classicContext = new project_dbEntities())
                {
                    var staff = (from s1 in classicContext.staffs
                                 where s1.StaffID.Equals(staffid)
                                 select s1).FirstOrDefault();

                    //Delete it from memory
                    classicContext.staffs.Remove(staff);
                    //Save to database
                    classicContext.SaveChanges();
                }
            }else
            {

            }
        }

        private void btn_edit_staff(object sender, RoutedEventArgs e)
        {
            object row = dg_editstaff.SelectedItem;
            string staffid = (dg_editstaff.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            string staffname = (dg_editstaff.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            string gender = (dg_editstaff.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            string center = (dg_editstaff.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            string email = (dg_editstaff.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            string pass = (dg_editstaff.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            string position = (dg_editstaff.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text;
            int salary = Convert.ToInt32((dg_editstaff.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text);
            string ctype = (dg_editstaff.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text;
            int late = Convert.ToInt32((dg_editstaff.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text);

            edit_staff e1 = new edit_staff(staffid, staffname, gender, center, email, pass, position, salary, ctype, late);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            e1.Top = (primScreenHeight - e1.Height) / 2;
            e1.Left = (primScreenWidth - e1.Width) / 2;
            e1.Show();

        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            dg_editstaff.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                var staff = (from staffs in classicContext.staffs
                             select new { staffs });


                foreach (var output in staff.ToList())
                {
                    dg_editstaff.Items.Add(output.staffs);
                }
            }
        }

        private void dg_editcust_Loaded(object sender, RoutedEventArgs e)
        {
            using (var classicContext = new project_dbEntities())
            {
                var cust = (from custs in classicContext.customers
                             select new { custs.CustID, custs.Surname, custs.GivenName, custs.DateOfBirth, custs.Gender, custs.Passport, custs.MobileNo, custs.Nationality });


                foreach (var output in cust.ToList())
                {
                    dg_editcust.Items.Add(new { CustID = output.CustID, Surname = output.Surname, GivenName = output.GivenName, DateOfBirth = output.DateOfBirth.ToShortDateString(), Gender=output.Gender, Passport= output.Passport, MobileNo=output.MobileNo, Nationality= output.Nationality });
                }
            }
        }

        private void btn_Refresh_Cust_Click(object sender, RoutedEventArgs e)
        {
            dg_editcust.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                var cust = (from custs in classicContext.customers
                            select new { custs.CustID, custs.Surname, custs.GivenName, custs.DateOfBirth, custs.Gender, custs.Passport, custs.MobileNo, custs.Nationality });


                foreach (var output in cust.ToList())
                {
                    dg_editcust.Items.Add(new { CustID = output.CustID, Surname = output.Surname, GivenName = output.GivenName, DateOfBirth = output.DateOfBirth.ToShortDateString(), Gender = output.Gender, Passport = output.Passport, MobileNo = output.MobileNo, Nationality = output.Nationality });
                }
            }
        }

        private void btn_edit_cust(object sender, RoutedEventArgs e)
        {
            object row = dg_editcust.SelectedItem;
            string custID = (dg_editcust.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            string surname = (dg_editcust.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            string givenname = (dg_editcust.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            DateTime dateofbirth = Convert.ToDateTime((dg_editcust.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text);
            string gender = (dg_editcust.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            string passport = (dg_editcust.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            string mobileno = (dg_editcust.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text;
            string nationality = (dg_editcust.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;


            edit_cust e1 = new edit_cust(custID, surname, givenname, dateofbirth, gender, passport, mobileno, nationality);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            e1.Top = (primScreenHeight - e1.Height) / 2;
            e1.Left = (primScreenWidth - e1.Width) / 2;
            e1.Show();
        }

        private void btn_delete_cust(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure delete this customer?", "Alert message", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                object row = dg_editcust.SelectedItem;
                string custid = (dg_editcust.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
                using (var classicContext = new project_dbEntities())
                {
                    var cust = (from s1 in classicContext.customers
                                 where s1.CustID.Equals(custid)
                                 select s1).FirstOrDefault();

                    //Delete it from memory
                    classicContext.customers.Remove(cust);
                    //Save to database
                    classicContext.SaveChanges();
                }
            }
            else
            {

            }
        }

        private void btn_Addcust_Click(object sender, RoutedEventArgs e)
        {
            add_customer new1 = new add_customer();
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            new1.Top = (primScreenHeight - new1.Height) / 2;
            new1.Left = (primScreenWidth - new1.Width) / 2;
            new1.Show();
        }
    }
}
