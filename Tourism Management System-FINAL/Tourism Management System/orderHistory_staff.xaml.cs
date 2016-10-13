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
    /// Interaction logic for orderHistory_staff.xaml
    /// </summary>
    public partial class orderHistory_staff : Window
    {
        private string staffID;
        public orderHistory_staff(string staffID)
        {
            this.staffID = staffID;
            InitializeComponent();
        }

        private void btn_showAll_Click(object sender, RoutedEventArgs e)
        {
            lab_total.Content = "Personal Sales Amount(All):";
            dg_orderHistoryResult.Items.Clear();
            decimal total=0;
            using (var classicContext = new project_dbEntities())
            {
                var hotelbooking = (from list in classicContext.hotelbookings
                                   where list.StaffID.Equals(staffID)
                                   select new { list.OrderDate,list.TotalAmt});

                foreach (var hotel in hotelbooking.ToList())
                {
                    total += hotel.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Hotel Booking", TotalAmt = "$" + hotel.TotalAmt, OrderDate = hotel.OrderDate });
                }

                var flightbooking = (from list in classicContext.flightbookings
                                    where list.StaffID.Equals(staffID)
                                    select new { list.OrderDate, list.TotalAmt });

                foreach (var flight in flightbooking.ToList())
                {
                    total += flight.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Flight Booking", TotalAmt = "$" + flight.TotalAmt, OrderDate = flight.OrderDate });
                }

                var attractbooking = (from list in classicContext.attractbookings
                                     where list.staffID.Equals(staffID)
                                     select new { list.orderDate, list.TotalAmt });

                foreach (var attract in attractbooking.ToList())
                {
                    total += attract.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Attraction Booking", TotalAmt = "$" + attract.TotalAmt, OrderDate = attract.orderDate });
                }

                var curisebooking = (from list in classicContext.cruisebookings
                                      where list.staffID.Equals(staffID)
                                      select new { list.orderDate, list.TotalAmt });

                foreach (var curise in curisebooking.ToList())
                {
                    total += curise.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Cruise Booking", TotalAmt = "$" + curise.TotalAmt, OrderDate = curise.orderDate });
                }

            }
            txt_total.Text = "$" + total;
        }

        private void btn_showToday_Click(object sender, RoutedEventArgs e)
        {
            lab_total.Content = "Personal Sales Amount(Today):";
            dg_orderHistoryResult.Items.Clear();
            DateTime today =Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            DateTime today1 = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToShortDateString());

            decimal total = 0;
            using (var classicContext = new project_dbEntities())
            {
                var hotelbooking = (from list in classicContext.hotelbookings
                                    where list.StaffID.Equals(staffID) && list.OrderDate >= today && list.OrderDate< today1
                                    select new { list.OrderDate, list.TotalAmt });

                foreach (var hotel in hotelbooking.ToList())
                {
                    total += hotel.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Hotel Booking", TotalAmt = "$" + hotel.TotalAmt, OrderDate = hotel.OrderDate });
                }

                var flightbooking = (from list in classicContext.flightbookings
                                     where list.StaffID.Equals(staffID) && list.OrderDate >= today && list.OrderDate < today1
                                     select new { list.OrderDate, list.TotalAmt });

                foreach (var flight in flightbooking.ToList())
                {
                    total += flight.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Flight Booking", TotalAmt = "$" + flight.TotalAmt, OrderDate = flight.OrderDate });
                }

                var attractbooking = (from list in classicContext.attractbookings
                                      where list.staffID.Equals(staffID) && list.orderDate >= today && list.orderDate < today1
                                      select new { list.orderDate, list.TotalAmt });

                foreach (var attract in attractbooking.ToList())
                {
                    total += attract.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Attraction Booking", TotalAmt = "$" + attract.TotalAmt, OrderDate = attract.orderDate });
                }

                var curisebooking = (from list in classicContext.cruisebookings
                                     where list.staffID.Equals(staffID) && list.orderDate >= today && list.orderDate < today1
                                     select new { list.orderDate, list.TotalAmt });

                foreach (var curise in curisebooking.ToList())
                {
                    total += curise.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Cruise Booking", TotalAmt = "$" + curise.TotalAmt, OrderDate = curise.orderDate });
                }

            }
            txt_total.Text = "$" + total;
        }

        private void btn_showWeek_Click(object sender, RoutedEventArgs e)
        {
            lab_total.Content = "Personal Sales Amount(Past Week):";
            dg_orderHistoryResult.Items.Clear();
            DateTime today = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToShortDateString());
            DateTime today1 = Convert.ToDateTime(DateTime.Now.Date.AddDays(-7).ToShortDateString());

            decimal total = 0;
            using (var classicContext = new project_dbEntities())
            {
                var hotelbooking = (from list in classicContext.hotelbookings
                                    where list.StaffID.Equals(staffID) && list.OrderDate >= today1 && list.OrderDate < today
                                    select new { list.OrderDate, list.TotalAmt });

                foreach (var hotel in hotelbooking.ToList())
                {
                    total += hotel.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Hotel Booking", TotalAmt = "$" + hotel.TotalAmt, OrderDate = hotel.OrderDate });
                }

                var flightbooking = (from list in classicContext.flightbookings
                                     where list.StaffID.Equals(staffID) && list.OrderDate >= today1 && list.OrderDate < today
                                     select new { list.OrderDate, list.TotalAmt });

                foreach (var flight in flightbooking.ToList())
                {
                    total += flight.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Flight Booking", TotalAmt = "$" + flight.TotalAmt, OrderDate = flight.OrderDate });
                }

                var attractbooking = (from list in classicContext.attractbookings
                                      where list.staffID.Equals(staffID) && list.orderDate >= today1 && list.orderDate < today
                                      select new { list.orderDate, list.TotalAmt });

                foreach (var attract in attractbooking.ToList())
                {
                    total += attract.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Attraction Booking", TotalAmt = "$" + attract.TotalAmt, OrderDate = attract.orderDate });
                }

                var curisebooking = (from list in classicContext.cruisebookings
                                     where list.staffID.Equals(staffID) && list.orderDate >= today1 && list.orderDate < today
                                     select new { list.orderDate, list.TotalAmt });

                foreach (var curise in curisebooking.ToList())
                {
                    total += curise.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Cruise Booking", TotalAmt = "$" + curise.TotalAmt, OrderDate = curise.orderDate });
                }

            }
            txt_total.Text = "$" + total;
        }

        private void btn_showMonth_Click(object sender, RoutedEventArgs e)
        {
            lab_total.Content = "Personal Sales Amount(Past Month):";
            dg_orderHistoryResult.Items.Clear();
            DateTime today = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToShortDateString());
            DateTime today1 = Convert.ToDateTime(DateTime.Now.Date.AddMonths(-1).ToShortDateString());

            decimal total = 0;
            using (var classicContext = new project_dbEntities())
            {
                var hotelbooking = (from list in classicContext.hotelbookings
                                    where list.StaffID.Equals(staffID) && list.OrderDate >= today1 && list.OrderDate < today
                                    select new { list.OrderDate, list.TotalAmt });

                foreach (var hotel in hotelbooking.ToList())
                {
                    total += hotel.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Hotel Booking", TotalAmt = "$" + hotel.TotalAmt, OrderDate = hotel.OrderDate });
                }

                var flightbooking = (from list in classicContext.flightbookings
                                     where list.StaffID.Equals(staffID) && list.OrderDate >= today1 && list.OrderDate < today
                                     select new { list.OrderDate, list.TotalAmt });

                foreach (var flight in flightbooking.ToList())
                {
                    total += flight.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Flight Booking", TotalAmt = "$" + flight.TotalAmt, OrderDate = flight.OrderDate });
                }

                var attractbooking = (from list in classicContext.attractbookings
                                      where list.staffID.Equals(staffID) && list.orderDate >= today1 && list.orderDate < today
                                      select new { list.orderDate, list.TotalAmt });

                foreach (var attract in attractbooking.ToList())
                {
                    total += attract.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Attraction Booking", TotalAmt = "$" + attract.TotalAmt, OrderDate = attract.orderDate });
                }

                var curisebooking = (from list in classicContext.cruisebookings
                                     where list.staffID.Equals(staffID) && list.orderDate >= today1 && list.orderDate < today
                                     select new { list.orderDate, list.TotalAmt });

                foreach (var curise in curisebooking.ToList())
                {
                    total += curise.TotalAmt;
                    dg_orderHistoryResult.Items.Add(new { OrderType = "Cruise Booking", TotalAmt = "$" + curise.TotalAmt, OrderDate = curise.orderDate });
                }

            }
            txt_total.Text = "$" + total;
        }
    }
}
