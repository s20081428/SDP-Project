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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for vehicle_order.xaml
    /// </summary>
    public partial class vehicle_order : Window
    {
        private string vehicle;
        private string type;
        private int capcity;
        private string gear;
        private string color;
        private string price;
        private string staffID;
        private string vehicleID;
        private string status;
        private DateTime pickupdate;
        private DateTime dropoffdate;
        private int Bookday;
        private decimal total_equip_price;
        private decimal total_trans_price;
        private int attract_bkID;
        private string total_equip_ID;
        private int total_equip_ID_index;
        private int vehicle_bookingID;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public vehicle_order(string vehicle, string type, int capcity, string gear, string color, string price, string staffID, string vehicleID, string status, int attract_bkID)
        {
            this.vehicle = vehicle;
            this.type = type;
            this.capcity = capcity;
            this.gear = gear;
            this.price = price;
            this.staffID = staffID;
            this.vehicleID = vehicleID;
            this.color = color;
            this.status = status;
            this.attract_bkID = attract_bkID;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            txt_color.Text = color;
            txt_vehicle.Text = vehicle;
            txt_type.Text = type;
            txt_Capacity.Text = Convert.ToString(capcity);
            txt_gear.Text = gear;
            txt_status.Text = status;
            im_vehicle.Source = new BitmapImage(new Uri("pack://application:,,,/Images/vehiclePhotos/" + vehicleID + ".png"));
            txt_transCharges.Text = "0";
            txt_EquipCharges.Text = "0";
            btn_Ok.IsEnabled = false;
            txt_status.Foreground = new SolidColorBrush(Colors.Red);
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

        }

        private void dg_equipmentdetails_Loaded(object sender, RoutedEventArgs e)
        {
            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var equipmentdetails = (from list in classicContext.equipments
                                        where list.Suitable.Equals(type)
                                        select new { list.EquipID, list.Equipment1, list.Suitable, list.Price });

                foreach (var output in equipmentdetails.ToList())
                {
                    dg_equipmentdetails.Items.Add(new { EquipID = output.EquipID, equip_photo = new Uri("pack://application:,,,/Images/EquipPhotos/" + output.EquipID + ".png"),  EquipName = output.Equipment1, EquipSuit = output.Suitable, EquipPrice = output.Price });
                }
            }
        }

        private void dp_pickupdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            pickupdate = Convert.ToDateTime(dp_pickupdate.SelectedDate);

            TimeSpan ts = dropoffdate - pickupdate;
            if (dp_dropoffdate.SelectedDate != null && dp_pickupdate.SelectedDate != null)
            {
                if (ts.Days >= 0)
                {
                    if (ts.Days == 0)
                    {
                        Bookday = 1;
                        txt_BookDay.Text = Bookday.ToString();
                        total_trans_price = Bookday * Convert.ToDecimal(price);
                        txt_transCharges.Text = "$" + (total_trans_price);
                    }
                    else
                    {
                        Bookday = ts.Days;
                        txt_BookDay.Text = Bookday.ToString();
                        total_trans_price = Bookday * Convert.ToDecimal(price);
                        txt_transCharges.Text = "$" + (total_trans_price);
                    }
                }
                else
                {
                    txt_BookDay.Text = null;
                    dp_pickupdate.SelectedDate = null;
                    MessageBox.Show("Pickupdate cannot larger than DropOffDate  , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }



        private void dp_dropoffdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dropoffdate = Convert.ToDateTime(dp_dropoffdate.SelectedDate);

            TimeSpan ts = dropoffdate - pickupdate;

            if (dp_dropoffdate.SelectedDate != null && dp_pickupdate.SelectedDate != null)
            {
                if (ts.Days >= 0)
                {
                    if (ts.Days == 0)
                    {
                        Bookday = 1;
                        txt_BookDay.Text = Bookday.ToString();
                        total_trans_price = Bookday * Convert.ToDecimal(price);
                        txt_transCharges.Text = "$" + (total_trans_price);
                    }
                    else
                    {
                        Bookday = ts.Days;
                        txt_BookDay.Text = Bookday.ToString();
                        total_trans_price = Bookday * Convert.ToDecimal(price);
                        txt_transCharges.Text = "$" + (total_trans_price);
                    }
                }
                else
                {
                    txt_BookDay.Text = null;
                    dp_dropoffdate.SelectedDate = null;
                    MessageBox.Show("DropOffDate cannot less than Pickupdate , Please Try Again  ", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            object row = dg_equipmentdetails.SelectedItem;
            decimal equip_price = Convert.ToDecimal((dg_equipmentdetails.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dg_equipmentdetails.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text.Length - 1));
            string equip_ID = (dg_equipmentdetails.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;

            total_equip_ID += equip_ID + " ";
            total_equip_price += equip_price;
            txt_EquipCharges.Text = "$" + (total_equip_price);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            object row = dg_equipmentdetails.SelectedItem;
            decimal equip_price = Convert.ToDecimal((dg_equipmentdetails.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dg_equipmentdetails.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text.Length - 1));
            string equip_ID = (dg_equipmentdetails.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;

            total_equip_ID = total_equip_ID.Replace(equip_ID+ " ", "");


            total_equip_price -= equip_price;
            txt_EquipCharges.Text = "$" + (total_equip_price);
        }

        private void txt_transCharges_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_GrandTotal.Text = "$" + (total_equip_price + total_trans_price);
        }

        private void txt_EquipCharges_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_GrandTotal.Text = "$" + (total_equip_price + total_trans_price);
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            if (txt_BookDay.Text == null || txt_BookDay.Text=="")
            {
                MessageBox.Show("PickupDate and DropoffDate are required!!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                txt_status.Text = "Confirmed";
                txt_status.Foreground = new SolidColorBrush(Colors.Blue);
                txt_status.FontWeight = FontWeights.Bold;
                dp_dropoffdate.IsEnabled = false;
                dp_pickupdate.IsEnabled = false;
                dg_equipmentdetails.IsEnabled = false;
                btn_Ok.IsEnabled = true;

                using (var classicContext = new project_dbEntities())
                {
                    var vehiclebookings = classicContext.Set<vehiclebooking>();
                    vehiclebookings.Add(new vehiclebooking
                    {
                        Vehicle_ID = vehicleID,
                        AttractionBookingID = attract_bkID,
                        BookDay = Bookday,
                        VehicleBookPrice = total_trans_price,
                        Orderdate = DateTime.Now.Date
                });
                    Console.WriteLine();
                    classicContext.SaveChanges();

                    var a = (from list in classicContext.attractbookings
                             where list.BookID.Equals(attract_bkID)
                             select list).FirstOrDefault();

                    a.status = txt_status.Text;

                    int num = classicContext.SaveChanges();
                }
            }
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var vehiclebookingID = (from list in classicContext.vehiclebookings
                                        where list.AttractionBookingID.Equals(attract_bkID) && list.BookDay.Equals(Bookday) && list.Vehicle_ID.Equals(vehicleID)
                                        select new { list.VehicleBookingID }).FirstOrDefault();

                vehicle_bookingID = vehiclebookingID.VehicleBookingID;

            }

            using (var classic = new project_dbEntities())
            {
                var equipmentlists = classic.Set<equipmentlist>();
                equipmentlists.Add(new equipmentlist
                {
                    VehicleBookingID = Convert.ToInt32(vehicle_bookingID),
                    EquipID = total_equip_ID,
                    EquipBookPrice = total_equip_price,
                    Orderdate = DateTime.Now.Date
        });
                Console.WriteLine();
                classic.SaveChanges();
            }

            MessageBox.Show(" A transport order has been successfully processed! You can view a order history by going to Menu and click [Order]->[History]", "Order successful message");
            this.Hide();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

            using (var classicContext = new project_dbEntities())
            {
                var a = (from list in classicContext.attractbookings
                         where list.BookID.Equals(attract_bkID)
                         select list).FirstOrDefault();

                a.status = "Self Organized";

                int num = classicContext.SaveChanges();
            } 
            
            this.Hide();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 704.553 / 811.681;
            if (e.HeightChanged)
                this.Width = this.Height * 811.681 / 704.553;
        }
    }
}
