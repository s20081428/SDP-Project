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
using System.Windows.Threading;




namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Object name;
        private Object position;
        private string staffID;
        private string staff_Type;
        private double aspectRatio = 0.0;


        public MainWindow(string staffID,Object name, Object position)
        {
            this.name = name;
            this.position = position;
            this.staffID = staffID;
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            aspectRatio = this.ActualWidth / this.ActualHeight;
            string staffPhoto;

            lalwelcome.Text= "Welcome back " + name +" ! ("+ position +")";
            using (var classicContext = new project_dbEntities())
            {
                //Get staffType type from DB
                var staffType = (from list in classicContext.staffs
                                 where list.StaffID.Equals(staffID)
                                 select new { list.Ctype, list.Photo }).FirstOrDefault();
                staff_Type = staffType.Ctype;
                staffPhoto = staffType.Photo;
            }

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
            dispatcherTimer1.Tick += new EventHandler(dispatcherTimer_DB);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer1.Start();
            if (staffPhoto != null){
                staff_icon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/staff_icon/" + staffPhoto));
            }else
            {
                staff_icon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/staff_icon/noicon.png"));
            }
            logo.Source = new BitmapImage(new Uri("pack://application:,,,/Images/TT_logo/TT_General.jpg"));

            tb_welcome.Text = "Welcome, " + name + "!\n" + "("+ position+")";

            airticket_enquiry nextPage = new airticket_enquiry(staffID);
            frameShow.Content = nextPage;

            if (staffID == "guest")
            {
                tab_transport.Visibility = Visibility.Collapsed;
                tab_report.Visibility = Visibility.Collapsed;
            }

        }

        public static bool CheckConnection()
        {
            try
            {
                using (var classicContext = new project_dbEntities())
                {
                    classicContext.Database.Connection.Open();
                    classicContext.Database.Connection.Close();
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer.Text = "Current Time:  " + DateTime.Now.ToString("tt HH:mm:ss");
        }

        private void dispatcherTimer_DB(object sender, EventArgs e)
        {
            if (CheckConnection())
            {
                dbstatus.Text = "DataBase Connection: Connected";
                dbstatus.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                dbstatus.Text = "DataBase Connection: Unconnected";
                dbstatus.Foreground = new SolidColorBrush(Colors.Red);
                dbError popup = new dbError();
                double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
                double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
                popup.Top = (primScreenHeight - popup.Height) / 2;
                popup.Left = (primScreenWidth - popup.Width) / 2;
                popup.Show();
                
            }
        }

        private void showOrderHistory(object sender, RoutedEventArgs e)
        {
            if (staff_Type == "Officer")
            {
                orderHistory_staff o1 = new orderHistory_staff(staffID);
                double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
                double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
                o1.Top = (primScreenHeight - o1.Height) / 2;
                o1.Left = (primScreenWidth - o1.Width) / 2;
                o1.Show();
            }else if (staff_Type == "Admin")
            {

            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void OpenMyProfile(object sender, RoutedEventArgs e)
        {
            if (staffID != "guest")
            {
                myprofle profile = new myprofle(staffID);
                double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
                double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
                profile.Top = (primScreenHeight - profile.Height) / 2;
                profile.Left = (primScreenWidth - profile.Width) / 2;
                profile.ShowDialog();
            }
        }

        private void airticket(object sender, RoutedEventArgs e)
        {
            airticket_enquiry nextPage = new airticket_enquiry(staffID);
            frameShow.Content = nextPage;
        }

        private void hotel(object sender, RoutedEventArgs e)
        {
            hotel_enquiry nextPage = new hotel_enquiry(staffID);
            frameShow.Content = nextPage;
        }

        private void attraction(object sender, RoutedEventArgs e)
        {
            attraction_enquiry nextPage = new attraction_enquiry(staffID);
            frameShow.Content = nextPage;
        }

        private void cruise(object sender, RoutedEventArgs e)
        {
            cruise_enquiry nextPage = new cruise_enquiry(staffID);
            frameShow.Content = nextPage;
        }

        private void transport(object sender, RoutedEventArgs e)
        {
            transport_enquiry nextPage = new transport_enquiry(staffID);
            frameShow.Content = nextPage;
        }

        private void report(object sender, RoutedEventArgs e)
        {
            report nextPage = new report(staffID);
            frameShow.Content = nextPage;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            
            var result = MessageBox.Show("Are you sure to logout?", "Logout", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                CloseAllWindows();

                Login login = new Login();
                login.Show();
            }
            else if (result == MessageBoxResult.No)
            {

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            logout(sender, e);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 618 / 1106;
            if (e.HeightChanged)
                this.Width = this.Height * 1106 / 618;
        }
    }
}
