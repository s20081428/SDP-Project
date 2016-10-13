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
    /// Interaction logic for report_Cust.xaml
    /// </summary>
    public partial class report_Cust : Window
    {
        private string user;
        public report_Cust()
        {
            InitializeComponent();
        }

        private void dg_flight_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void txt_CustID_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg_flight.Items.Clear();
            dg_attract.Items.Clear();
            dg_cruise.Items.Clear();
            dg_hotel.Items.Clear();
            dg_transport.Items.Clear();


            user = txt_CustID.Text;
            using (var classicContext = new project_dbEntities())
            {
                var flightbooking = (from flight in classicContext.flightbookings
                                     join cust in classicContext.customers on flight.CustID equals cust.CustID
                                     where flight.CustID.Equals(user) || cust.MobileNo.Equals(user)
                                     select new { cust.CustID, flight.BookingID, flight.FlightNo, flight.OrderDate, flight.DepDateTime, flight.Class, flight.AdultNum, flight.ChildNum, flight.InfantNum, flight.AdultPrice, flight.ChildPrice, flight.InfantPrice, flight.TotalAmt });


                foreach (var output in flightbooking.ToList())
                {
                    dg_flight.Items.Add(new { OrderDate = output.OrderDate.Date.ToShortDateString(), BookingID = output.BookingID, FlightNo = output.FlightNo, DepDateTime = output.DepDateTime, Class = output.Class, TotalAmt = "$" + output.TotalAmt });
                }
            }

            using (var classicContext = new project_dbEntities())
            {
                var hotelbooking = (from hotel in classicContext.hotelbookings
                                    join cust in classicContext.customers on hotel.CustID equals cust.CustID
                                    where hotel.CustID.Equals(user) || cust.MobileNo.Equals(user)
                                    select new { hotel.BookingID, hotel.OrderDate, hotel.HotelID, hotel.Checkin, hotel.Checkout, hotel.RoomSize, hotel.RoomType, hotel.Price });

               
                foreach (var output in hotelbooking.ToList())
                {
                    dg_hotel.Items.Add(new { OrderDate = output.OrderDate.Date.ToShortDateString(), BookingID = output.BookingID, HotelID = output.HotelID, RoomType = output.RoomType, RoomSize = output.RoomSize, Checkin = output.Checkin, Checkout = output.Checkout, TotalAmt = "$" + output.Price });
                }
            }

            using (var classicContext = new project_dbEntities())
            {
                var cruisebooking = (from cruise in classicContext.cruisebookings
                                     join cust in classicContext.customers on cruise.custID equals cust.CustID
                                     where cruise.custID.Equals(user) || cust.MobileNo.Equals(user)
                                     select new { cruise.BookingID, cruise.orderDate, cruise.cruiseNo, cruise.cruiseName, cruise.TourDay, cruise.ChildNum, cruise.AdultNum, cruise.ChildPrice, cruise.AdultPrice, cruise.TotalAmt,cruise.StartDate });
              
                foreach (var output in cruisebooking.ToList())
                {
                    dg_cruise.Items.Add(new { OrderDate = output.orderDate.Date.ToShortDateString(), BookingID = output.BookingID, cruiseNo = output.cruiseNo, cruiseName = output.cruiseName, TourDay = output.TourDay, StartDate = output.StartDate.Date.ToShortDateString(), TotalAmt = "$" + output.TotalAmt });
                }
            }

            using (var classicContext = new project_dbEntities())
            {
                var attractbooking = (from attract in classicContext.attractbookings
                                      join cust in classicContext.customers on attract.custID equals cust.CustID
                                      where attract.custID.Equals(user) || cust.MobileNo.Equals(user)
                                      select new { attract.BookID, attract.orderDate, attract.Attaction, attract.City, attract.childNum, attract.adultNum, attract.childPrice, attract.adultPrice, attract.status, attract.TotalAmt });
               
                foreach (var output in attractbooking.ToList())
                {
                    dg_attract.Items.Add(new { BookingID = output.BookID, OrderDate = output.orderDate.Date.ToShortDateString(), City = output.City, Attraction = output.Attaction,Status = output.status, TotalAmt = "$" + output.TotalAmt });
                }
            }

            using (var classicContext = new project_dbEntities())
            {
                var vehiclebooking = (from vehicle in classicContext.vehiclebookings
                                      join attract in classicContext.attractbookings on vehicle.AttractionBookingID equals attract.BookID
                                      join cust in classicContext.customers on attract.custID equals cust.CustID
                                      where attract.custID.Equals(user) || cust.MobileNo.Equals(user)
                                      select new { vehicle.VehicleBookingID, vehicle.Vehicle_ID, vehicle.BookDay,vehicle.AttractionBookingID, vehicle.VehicleBookPrice,vehicle.Orderdate });
              
                foreach (var output in vehiclebooking.ToList())
                {
                    dg_transport.Items.Add(new { OrderDate=output.Orderdate.Date.ToShortDateString(), Attraction_BookingID=output.AttractionBookingID, BookingID = output.VehicleBookingID, VehicleID = output.Vehicle_ID, BookDay = output.BookDay, TotalAmt = "$" + output.VehicleBookPrice });
                }
            }

            using (var classicContext = new project_dbEntities())
            {
                var equipmentlist = (from equip in classicContext.equipmentlists
                                     join vehicle in classicContext.vehiclebookings on equip.VehicleBookingID equals vehicle.VehicleBookingID
                                     join attract in classicContext.attractbookings on vehicle.AttractionBookingID equals attract.BookID
                                     join cust in classicContext.customers on attract.custID equals cust.CustID
                                     where attract.custID.Equals(user) || cust.MobileNo.Equals(user)
                                     select new { equip.EquipBookingID,equip.Orderdate, equip.EquipID, equip.EquipBookPrice,equip.VehicleBookingID });
                
                foreach (var output in equipmentlist.ToList())
                {
                    dg_equip.Items.Add(new { OrderDate=output.Orderdate.Date.ToShortDateString(), BookingID = output.EquipBookingID, EquipID = output.EquipID, Vehicle_BookingID=output.VehicleBookingID, TotalAmt = "$" + output.EquipBookPrice });
                }
            }
        }

        private void btn_export_Click(object sender, RoutedEventArgs e)
        {
            btn_export.Visibility = Visibility.Collapsed;
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(this, "");
            }
            else
            {
                btn_export.Visibility = Visibility.Visible;
            }
            btn_export.Visibility = Visibility.Visible;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
                this.Height = this.Width * 979.5 / 1270;
            if (e.HeightChanged)
                this.Width = this.Height * 1270 / 979.5;
        }
    }
}
