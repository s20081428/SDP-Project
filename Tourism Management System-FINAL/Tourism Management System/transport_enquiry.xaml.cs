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
    /// Interaction logic for transport_enquiry.xaml
    /// </summary>
    public partial class transport_enquiry : Page
    {
        private string staffID;
        private object row;
        private object row2;
        public transport_enquiry(string staffid)
        {
            
            this.staffID = staffid;
            InitializeComponent();
        }

        private void dg_tfm_attractresult_Loaded(object sender, RoutedEventArgs e)
        {
            updateAttract();
        }

        private void dg_tfm_attractresult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_transportType.Visibility = Visibility.Visible;
            dg_vehicleResult.Visibility = Visibility.Visible;
            lab_vehicletype.Visibility = Visibility.Visible;
            dg_vehicleResult.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var vehicle = (from v in classicContext.vehicles
                               select new { v });

                foreach (var output in vehicle.ToList())
                {
                    dg_vehicleResult.Items.Add(new { VehicleName = output.v.Vehicle_Name, VehicleType = UppercaseFirst(output.v.Vehicle_Type), Capcity = output.v.Capacity, Gear = output.v.Gear, Color = output.v.Color, Price = output.v.Price });
                }
            }
        }

        private string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            cb_transportType.Visibility = Visibility.Collapsed;
            lab_vehicletype.Visibility = Visibility.Collapsed;
            cb_transportType.Items.Add("Car");
            cb_transportType.Items.Add("Coach");
        }

        private void cb_transportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg_vehicleResult.Items.Clear();
            string type =Convert.ToString(cb_transportType.SelectedItem);
            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var vehicle = (from v in classicContext.vehicles
                               where v.Vehicle_Type.Equals(type)
                               select new { v });

                foreach (var output in vehicle.ToList())
                {
                    dg_vehicleResult.Items.Add(new { VehicleName = output.v.Vehicle_Name, VehicleType = UppercaseFirst(output.v.Vehicle_Type), Capcity = output.v.Capacity, Gear = output.v.Gear, Color = output.v.Color, Price = output.v.Price });
                }
            }
        }

        private void btn_vehicle_order(object sender, RoutedEventArgs e)
        {

            row = dg_vehicleResult.SelectedItem;
            row2 = dg_tfm_attractresult.SelectedItem;


            int attract_bkID = Convert.ToInt32((dg_tfm_attractresult.SelectedCells[0].Column.GetCellContent(row2) as TextBlock).Text);
            string vehicle = (dg_vehicleResult.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            string type = (dg_vehicleResult.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            int capcity = int.Parse((dg_vehicleResult.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text);
            string gear = (dg_vehicleResult.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            string color = (dg_vehicleResult.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            string price = ((dg_vehicleResult.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dg_vehicleResult.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text.Length - 1));
            string staffID = this.staffID;
            string vehicleID;
            string status;

            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var vehicleicon = (from v in classicContext.vehicles
                                   where v.Vehicle_Name.Equals(vehicle)
                                   select new { v.Vehicle_ID}).FirstOrDefault();

                vehicleID = vehicleicon.Vehicle_ID;

                var a = (from list in classicContext.attractbookings
                         where list.BookID.Equals(attract_bkID)
                         select list).FirstOrDefault();

                a.status = type;
                status = type;
                int num = classicContext.SaveChanges();
            }

            updateAttract();
            vehicle_order order = new vehicle_order(vehicle, type, capcity, gear,color,price,staffID,vehicleID,status, attract_bkID);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            order.Top = (primScreenHeight - order.Height) / 2;
            order.Left = (primScreenWidth - order.Width) / 2;
            order.ShowDialog();
        }

        private void updateAttract()
        {
            dg_tfm_attractresult.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var attract = (from ab in classicContext.attractbookings
                               select new { ab.BookID, ab.Attaction, ab.City, ab.adultPrice, ab.custID, ab.childNum, ab.adultNum, ab.childPrice, ab.TotalAmt, ab.status });

                foreach (var output in attract.ToList())
                {
                    dg_tfm_attractresult.Items.Add(new { BookID = output.BookID, CustID=output.custID, AttractName = output.Attaction, AdultNum = output.adultNum, ChildNum = output.childNum, Status = output.status, TotalAmt = output.TotalAmt });
                }
            }
            dg_vehicleResult.Visibility = Visibility.Collapsed;
            cb_transportType.Visibility = Visibility.Collapsed;
            lab_vehicletype.Visibility = Visibility.Collapsed;
        }

        

    }
}

