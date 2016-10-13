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
    /// Interaction logic for flight_searchForCust.xaml
    /// </summary>
    public partial class flight_searchForCust : Page
    {
        private string user;
        public flight_searchForCust()
        {
            InitializeComponent();
        }

        private void txt_cust_TextChanged(object sender, TextChangedEventArgs e)
        {
            user = txt_cust.Text;
            dg_flightbk.Items.Clear();

            using (var classicContext = new project_dbEntities())
            {
                var flightbooking = (from flight in classicContext.flightbookings
                                     join cust in classicContext.customers on flight.CustID equals cust.CustID
                                     where flight.CustID.Equals(user) || cust.MobileNo.Equals(user)
                                     select new { cust.CustID, flight.BookingID, flight.FlightNo, flight.OrderDate, flight.DepDateTime, flight.Class, flight.AdultNum, flight.ChildNum,flight.From,flight.To, flight.InfantNum, flight.AdultPrice, flight.ChildPrice, flight.InfantPrice, flight.TotalAmt });


                foreach (var output in flightbooking.ToList())
                {
                    dg_flightbk.Items.Add(new { BookingID = output.BookingID, OrderDate = output.OrderDate.Date.ToShortDateString(), FlightNo = output.FlightNo,From = output.From, To= output.To, DepDateTime = output.DepDateTime, Class = output.Class, Price = "$" + output.TotalAmt, symbol="=>" });
                }
            }
        }

        private void btn_showConfirm(object sender, RoutedEventArgs e)
        {
            object row = dg_flightbk.SelectedItem;

            //DateTime FightDate = Convert.ToDateTime((dataGrid1.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text);
            int BookingID = Convert.ToInt32((dg_flightbk.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
            DateTime Orderdate = Convert.ToDateTime((dg_flightbk.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text);
            string FlightNo = (dg_flightbk.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            string from = (dg_flightbk.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            string to = (dg_flightbk.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            DateTime DepdateTime = Convert.ToDateTime((dg_flightbk.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text);
            string classtype = (dg_flightbk.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            string price = ((dg_flightbk.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dg_flightbk.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text.Length - 1));
            int AdultNum;
            int ChildNum;
            int InfantNum;
            string CustID;
            decimal AdultPrice;
            decimal ChildPrice;
            decimal InfantPrice;

            using (var classicContext = new project_dbEntities())
            {
                var flightbooking = (from flight in classicContext.flightbookings
                                     where flight.BookingID.Equals(BookingID)
                                     select new { flight.AdultNum, flight.ChildNum, flight.InfantNum,flight.CustID,flight.AdultPrice,flight.ChildPrice,flight.InfantPrice}).FirstOrDefault();

                AdultNum = flightbooking.AdultNum;
                ChildNum = flightbooking.ChildNum;
                InfantNum = flightbooking.InfantNum;
                
                CustID = flightbooking.CustID;
                AdultPrice = flightbooking.AdultPrice;
                ChildPrice= flightbooking.ChildPrice;
                InfantPrice = flightbooking.InfantPrice;

            }

            flight_confirm pop = new flight_confirm( BookingID,  Orderdate,  FlightNo,  DepdateTime, classtype, price,AdultNum, ChildNum,InfantNum,CustID,AdultPrice,ChildPrice,InfantPrice,from,to);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            pop.Top = (primScreenHeight - pop.Height) / 2;
            pop.Left = (primScreenWidth - pop.Width) / 2;
            pop.Show();
        }
         
    }
}
