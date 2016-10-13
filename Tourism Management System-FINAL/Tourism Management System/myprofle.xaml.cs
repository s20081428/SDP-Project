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
    /// Interaction logic for myprofle.xaml
    /// </summary>
    public partial class myprofle : Window
    {

        private string staffID;
        public myprofle(string staffID)
        {
            this.staffID = staffID;
            InitializeComponent();
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            using (var classicContext = new project_dbEntities())
            {
                var getstaffinfo = (from list in classicContext.staffs
                                    where list.StaffID.Equals(staffID)
                                    select new { list.Pass }).FirstOrDefault();


                if (txt_currentPassword.Password == "" || txt_currentPassword.Password == null)
                {
                    MessageBox.Show("Please enter current password!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (txt_newPassword.Password == "" || txt_newPassword.Password == null)
                {
                    MessageBox.Show("Please enter new password!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (txt_confirmPassword.Password == "" || txt_confirmPassword.Password == null)
                {
                    MessageBox.Show("Please enter confirm password!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (txt_newPassword.Password.Length < 6)
                {
                    MessageBox.Show("You entered a new password is too short! Password length must >= 6", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (txt_confirmPassword.Password != txt_newPassword.Password)
                {
                    MessageBox.Show("Your confirm password is wrong!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (txt_currentPassword.Password != getstaffinfo.Pass)
                {
                    MessageBox.Show("You entered a wrong current Password!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var a = (from list in classicContext.staffs
                             where list.StaffID.Equals(staffID)
                             select list).FirstOrDefault();

                    a.Pass = txt_newPassword.Password;

                    int num = classicContext.SaveChanges();
                    MessageBox.Show("You password was changed successfully! Please login again.", "Password Changed");

                    CloseAllWindows();
                    Login login = new Login();
                    login.Show();

                }
            }
        }

        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
 
            using (var classicContext = new project_dbEntities())
            {
                var getstaffinfo = (from list in classicContext.staffs
                                    where list.StaffID.Equals(staffID)
                                    select new { list.StaffID, list.Position, list.Pass, list.Late, list.Email, list.Gender, list.Center, list.StaffName, list.Photo }).FirstOrDefault();
                txt_staffID.Text = getstaffinfo.StaffID;
                txt_StaffName.Text = getstaffinfo.StaffName;
                txt_Gender.Text = getstaffinfo.Gender;
                txt_center.Text = getstaffinfo.Center;
                txt_email.Text = getstaffinfo.Email;
                txt_Late.Text = getstaffinfo.Late.ToString();
                staff_icon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/staff_icon/" + getstaffinfo.Photo));
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 384/781;
            if (e.HeightChanged)
                this.Width = this.Height * 781/384;
        }
    }
}
