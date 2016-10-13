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
using System.Security.Cryptography;

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
        }

        //-----------------------------------------CUSTOMIZE RESTORE, MAXIMIZE, MINIMIZE BUTTON--------------------------------------------//
        private void PART_TITLEBAR_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void PART_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PART_MAXIMIZE_RESTORE_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void PART_MINIMIZE_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        //-----------------------------------------------------END OF CUSTOM BUTTON----------------------------------------------------------//
        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void login()
        {
            //RETURN MESSAGE IF THE USERNAME IS EMPTY
            if (string.IsNullOrEmpty(txtbox_name.Text))
            {
                MessageBox.Show("Please enter you username.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox_name.Focus();
                return;
            }
            //RETURN MESSAGE IF THE PASSWORD IS EMPTY
            else if (string.IsNullOrEmpty(txtbox_password.Password))
            {
                MessageBox.Show("Please enter you password.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox_password.Focus();
                return;
            }
            else
            {
                //CONNECT TO DATABASE
                using (var classicContext = new project_dbEntities())
                {
                    var account = (from list in classicContext.staffs where list.StaffID == txtbox_name.Text select new { list.StaffID, list.Pass, list.StaffName, list.Position }).FirstOrDefault();
                    var account_email = (from list in classicContext.staffs where list.Email == txtbox_name.Text select new { list.StaffID, list.Pass, list.StaffName, list.Position }).FirstOrDefault();

                    if (account != null || account_email != null)
                    {
                        //LOGIN WITH STAFFID
                        if (account != null)
                        {
                            //ENCTYPE THE PASSWORD OF INPUT AND DATABASE PASSWORD
                            MD5 md5 = MD5.Create();
                            string inputpass = Convert.ToBase64String(md5.ComputeHash(Encoding.Default.GetBytes(txtbox_password.Password)));
                            string dbpass = Convert.ToBase64String(md5.ComputeHash(Encoding.Default.GetBytes(account.Pass)));

                            //CHECK INPUTPASS IS OR NOT EQUAL TO DBPASS
                            if (inputpass == dbpass)
                            {
                                //LOGIN AS STAFF
                                if (account.Position == "Customer Service Officer" || account.Position == "Center Manager" || account.Position == "Guest")
                                {
                                    MainWindow m1 = new MainWindow(account.StaffID, account.StaffName, account.Position);
                                    m1.Show();
                                    this.Hide();
                                }
                                //LOGIN AS ADMINISTRATOR
                                else if (account.Position == "Administrator")
                                {
                                    MainWindow_Staff m1 = new MainWindow_Staff(account.StaffID, account.StaffName, account.Position);
                                    m1.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                //RETURN MESSAGE IF THE PASSWORD IS WRONG
                                MessageBox.Show("Password Incorrect!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        //LOGIN WITH EMAIL
                        else if (account_email != null)
                        {
                            MD5 md5 = MD5.Create();
                            string inputpass = Convert.ToBase64String(md5.ComputeHash(Encoding.Default.GetBytes(txtbox_password.Password)));
                            string dbpass = Convert.ToBase64String(md5.ComputeHash(Encoding.Default.GetBytes(account_email.Pass)));

                            //CHECK INPUTPASS IS OR NOT EQUAL TO DBPASS
                            if (inputpass == dbpass)
                            {
                                //LOGIN AS STAFF
                                if (account_email.Position == "Customer Service Officer" || account_email.Position == "Center Manager")
                                {
                                    MainWindow m1 = new MainWindow(account_email.StaffID, account_email.StaffName, account_email.Position);
                                    m1.Show();
                                    this.Hide();
                                }
                                //LOGIN AS ADMINISTRATOR
                                else if (account_email.Position == "Administrator")
                                {
                                    MainWindow_Staff m1 = new MainWindow_Staff(account_email.StaffID, account_email.StaffName, account_email.Position);
                                    m1.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                //RETURN MESSAGE IF THE PASSWORD IS WRONG
                                MessageBox.Show("Password Incorrect!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    else
                    {
                        //RETURN MESSAGE IF THE USERNAME IS WRONG
                        MessageBox.Show("Username Incorrect!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void txtbox_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CONNECT TO DATABASE
            using (var classContext = new project_dbEntities())
            { //keyword search

                lab_login_name.Content = " ";
                string keyword = txtbox_name.Text;
                var name = (from list in classContext.staffs
                            where list.StaffID.Equals(keyword) || list.Email.Equals(keyword)
                            select new { list.StaffName, list.Gender }).FirstOrDefault();

                try
                {
                    //CHECK USER IS MALE OR FEMALE
                    string mr;
                    if (name.Gender == "M")
                    {
                        mr = "Mr. ";
                    }
                    else
                    {
                        mr = "Ms. ";
                    }
                    lab_login_name.Content = mr + name.StaffName;
                }
                catch (NullReferenceException)
                {

                }

            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //CLOSE THE LOGIN WINDOW
            this.Close();
        }

        private void txtbox_password_KeyDown(object sender, KeyEventArgs e)
        {
            //SET PRESS ENTER TO LOGIN
            if (e.Key == Key.Enter)
            {
                login();
            }
        }

        private void btn_guest_Click(object sender, RoutedEventArgs e)
        {
            //LOGIN AS GUEST
            string guest_username = "guest";
            string guest_password = "123456";
            using (var classicContext = new project_dbEntities())
            {
                var account = (from list in classicContext.staffs where list.StaffID == guest_username select new { list.StaffID, list.Pass, list.StaffName, list.Position }).FirstOrDefault();

                if (guest_password == account.Pass)
                {
                    MainWindow m1 = new MainWindow(account.StaffID, account.StaffName, account.Position);
                    m1.Show();
                    this.Hide();
                }
            }
        }
    }
}
