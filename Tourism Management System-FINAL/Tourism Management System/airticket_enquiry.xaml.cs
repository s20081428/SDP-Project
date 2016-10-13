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
using System.Data;

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for airticket_enquiry.xaml
    /// </summary>
    public partial class airticket_enquiry : Page
    {
        private string staffID;
        public airticket_enquiry(string staffid)
        {
            this.staffID = staffid;
            InitializeComponent();
        }

        

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            //DateTime dpdate = dp_depart.Text;
            String classtype = "";
            String airline = "";
            String from1 = "";
            String to = "";
            Nullable<DateTime> depart;
            Nullable<DateTime> depart_next;

            //Get cb_from selected option value
            try
            {
                from1 = cb_from.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("From place is required!!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //Get cb_To select option value
            try
            {
                to = cb_To.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("To place is required!!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //Get cb_airline select option value
            try
            {
                airline = cb_airline.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                airline = null;

            }

            //Get cb_class select option value
            try
            {
                classtype = cb_class.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                classtype = null;
            }

            //Get depart date from selected date
            try
            {
                depart = dp_depart.SelectedDate.Value;
                depart_next = dp_depart.SelectedDate.Value.AddDays(1);
            }
            catch (InvalidOperationException)
            {
                depart = null;
                depart_next = null;
            }

            Object showbutton;

            if (staffID == "guest")
            {
                showbutton = Visibility.Collapsed;
            }else
            {
                showbutton = Visibility.Visible;
            }
            dataGrid1.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                //Show search result without select airline, class, departdate
                if ((airline == null || airline == "") && (classtype == null || classtype == "") && (depart == null))
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to)
                                     select new { c.Tax,f.FlyMinute,f.Aircraft,f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });

                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax=scheldule1.Tax,Aircraft = scheldule1.Aircraft,FlyMinute= scheldule1.FlyMinute,FlightNo=scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol="=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult =  scheldule1.Price_Adult, Price_Infant  =  scheldule1.Price_Infant, ShowButton = showbutton});
                    }
                }
                //Show search result without select airline, class
                else if ((airline == null || airline == "") && (classtype == null || classtype == ""))
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to) && f.DepDateTime >= depart && f.DepDateTime < depart_next
                                     select new { c.Tax, f.FlyMinute, f.Aircraft, f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });

                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax = scheldule1.Tax, Aircraft = scheldule1.Aircraft, FlyMinute = scheldule1.FlyMinute, FlightNo = scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol = "=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult = scheldule1.Price_Adult, Price_Infant = scheldule1.Price_Infant, ShowButton = showbutton });
                    }
                }
                //Show search result without select airline, depart date only
                else if ((airline == null || airline == "") && (depart == null))
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where c.Class.Equals(classtype) && f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to)
                                     select new { c.Tax, f.FlyMinute, f.Aircraft, f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });

                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax = scheldule1.Tax, Aircraft = scheldule1.Aircraft, FlyMinute = scheldule1.FlyMinute, FlightNo = scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol = "=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult = scheldule1.Price_Adult, Price_Infant = scheldule1.Price_Infant, ShowButton = showbutton });
                    }
                }
                //Show search result without select class, depart date only 
                else if ((classtype == null || classtype == "") && depart == null)
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to) && p.AirlineCode.Equals(airline)
                                     select new { c.Tax, f.FlyMinute, f.Aircraft, f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });
                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax = scheldule1.Tax, Aircraft = scheldule1.Aircraft, FlyMinute = scheldule1.FlyMinute, FlightNo = scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol = "=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult = scheldule1.Price_Adult, Price_Infant = scheldule1.Price_Infant, ShowButton = showbutton });
                    }
                }
                //Show search result without select airline only
                else if (airline == null || airline == "")
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where c.Class.Equals(classtype) && f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to) && f.DepDateTime >= depart && f.DepDateTime < depart_next
                                     select new { c.Tax, f.FlyMinute, f.Aircraft, f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });

                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax = scheldule1.Tax, Aircraft = scheldule1.Aircraft, FlyMinute = scheldule1.FlyMinute, FlightNo = scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol = "=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult = scheldule1.Price_Adult, Price_Infant = scheldule1.Price_Infant, ShowButton = showbutton });
                    }
                }
                //Show search result without select depart date only
                else if (depart == null)
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where c.Class.Equals(classtype) && f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to) && p.AirlineCode.Equals(airline)
                                     select new { c.Tax, f.FlyMinute, f.Aircraft, f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });
                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax = scheldule1.Tax, Aircraft = scheldule1.Aircraft, FlyMinute = scheldule1.FlyMinute, FlightNo = scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol = "=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult = scheldule1.Price_Adult, Price_Infant = scheldule1.Price_Infant, ShowButton = showbutton });
                    }
                }
                //Show search result without select classtype only
                else if (classtype == null || classtype == "")
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to) && f.DepDateTime >= depart && f.DepDateTime < depart_next && p.AirlineCode.Equals(airline)
                                     select new { c.Tax, f.FlyMinute, f.Aircraft, f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });

                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax = scheldule1.Tax, Aircraft = scheldule1.Aircraft, FlyMinute = scheldule1.FlyMinute, FlightNo = scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol = "=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult = scheldule1.Price_Adult, Price_Infant = scheldule1.Price_Infant, ShowButton = showbutton });
                    }
                }
                else
                //Show search result with selected all 
                {
                    var scheldule = (from f in classicContext.flightschedules
                                     join c in classicContext.flightclasses on f.FlightNo equals c.FlightNo
                                     join p in classicContext.airlines on c.AirlineCode equals p.AirlineCode
                                     where c.Class.Equals(classtype) && f.DepAirport.Equals(from1) && f.ArrAirport.Equals(to) && f.DepDateTime >= depart && f.DepDateTime < depart_next && p.AirlineCode.Equals(airline)
                                     select new { c.Tax, f.FlyMinute, f.Aircraft, f.FlightNo, p.icon, p.airlineName, f.DepDateTime, f.ArrDateTime, f.DepAirport, f.ArrAirport, c.Class, c.Price_Children, c.Price_Adult, c.Price_Infant });
                    foreach (var scheldule1 in scheldule.ToList())
                    {
                        dataGrid1.Items.Add(new { Tax = scheldule1.Tax, Aircraft = scheldule1.Aircraft, FlyMinute = scheldule1.FlyMinute, FlightNo = scheldule1.FlightNo, airline_photo = new Uri("pack://application:,,,/Images/airline_logo/" + scheldule1.icon), airlineName = scheldule1.airlineName, DepDateTime = scheldule1.DepDateTime, ArrDateTime = scheldule1.ArrDateTime, DepAirport = scheldule1.DepAirport, symbol = "=>", ArrAirport = scheldule1.ArrAirport, Class = scheldule1.Class, Price_Children = scheldule1.Price_Children, Price_Adult = scheldule1.Price_Adult, Price_Infant = scheldule1.Price_Infant, ShowButton = showbutton });
                    }
                }


            }
            

    }


        private void cb_class_Loaded(object sender, RoutedEventArgs e)
        {
            cb_class.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                //Get Class type from DB
                var getclass = (from list in classicContext.flightclasses select new { list.Class }).Distinct();

                //Add a black items which is use for show all
                cb_class.Items.Add("");

                //Show result of class type in cb_class
                foreach (var getName1 in getclass.ToList())
                {
                    cb_class.Items.Add(getName1.Class);
                }

            }
        }

        public class ComboBoxItem
        {
            public string Value;
            public string Text;

            public ComboBoxItem(string val, string text)
            {
                Value = val;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void cb_from_Loaded(object sender, RoutedEventArgs e)
        {
            cb_from.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                //Set key and value of cb_from items
                cb_from.DisplayMemberPath = "Key";
                cb_from.SelectedValuePath = "Value";

                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var getName_from = (from list in classicContext.aircontries
                                    join p in classicContext.flightschedules on list.shortform equals p.DepAirport
                                    where list.shortform.Equals(p.DepAirport)
                                    select new { list.chinese_city, list.airport_name, list.shortform }).Distinct();

                //Show all city name with airport name in to cb_From 
                foreach (var getName1 in getName_from.ToList())
                {
                    cb_from.Items.Add(new KeyValuePair<string, string>(getName1.chinese_city + "(" + getName1.airport_name + ")", getName1.shortform));
                }

            }
        }

        private void cb_To_Loaded(object sender, RoutedEventArgs e)
        {
            cb_To.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                //Set key and value of cb_To items
                cb_To.DisplayMemberPath = "Key";
                cb_To.SelectedValuePath = "Value";

                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var getName_To = (from list in classicContext.aircontries
                                  join p in classicContext.flightschedules on list.shortform equals p.ArrAirport
                                  where list.shortform.Equals(p.ArrAirport)
                                  select new { list.chinese_city, list.airport_name, list.shortform }).Distinct();

                //Show all city name with airport name in to cb_To 
                foreach (var getName1 in getName_To.ToList())
                {
                    cb_To.Items.Add(new KeyValuePair<string, string>(getName1.chinese_city + "(" + getName1.airport_name + ")", getName1.shortform));
                }

            }
        }

        private void cb_airline_DropDownOpened(object sender, EventArgs e)
        {
            cb_airline.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                var from_test = "";
                var to = "";
                try
                {
                    from_test = cb_from.SelectedValue.ToString();
                    to = cb_To.SelectedValue.ToString();
                }
                catch (NullReferenceException)
                {

                }

                //Set key and value of cb_airline items
                cb_airline.DisplayMemberPath = "Key";
                cb_airline.SelectedValuePath = "Value";

                //Get airline refer form the selection of cb_to
                var getName_airline = (from f in classicContext.airlines
                                       join c in classicContext.flightclasses on f.AirlineCode equals c.AirlineCode
                                       join d in classicContext.flightschedules on c.FlightNo equals d.FlightNo
                                       where d.DepAirport.Equals(from_test) && d.ArrAirport.Equals(to)
                                       select new { f.airlineName, f.AirlineCode }).Distinct();

                //Add a black items which is use for show all
                cb_airline.Items.Add("");

                //Show the results of all related airline 
                foreach (var getName1 in getName_airline.ToList())
                {
                    cb_airline.Items.Add(new KeyValuePair<string, string>(getName1.airlineName, getName1.AirlineCode));
                }

            }
        }

        private void cb_To_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //clear cb_aireline items if cb_to selection changed
            cb_airline.Items.Clear();
        }

        private void btn_order(object sender, RoutedEventArgs e)
        {
            //GET SELECTED ROW VALUES
            object row = dataGrid1.SelectedItem;
            DateTime FightDate = Convert.ToDateTime((dataGrid1.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text);
            string FightNo = (dataGrid1.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            string From = (dataGrid1.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            string To = (dataGrid1.SelectedCells[7].Column.GetCellContent(row) as TextBlock).Text;
            string adult_price = ((dataGrid1.SelectedCells[10].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dataGrid1.SelectedCells[10].Column.GetCellContent(row) as TextBlock).Text.Length-1));
            string child_price = ((dataGrid1.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dataGrid1.SelectedCells[9].Column.GetCellContent(row) as TextBlock).Text.Length-1));
            string infant_price = ((dataGrid1.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dataGrid1.SelectedCells[11].Column.GetCellContent(row) as TextBlock).Text.Length-1));
            string classtype = (dataGrid1.SelectedCells[8].Column.GetCellContent(row) as TextBlock).Text;
            string staffID = this.staffID;


            
            //Display a new airticker_order window with the details of selected row
            airticket_order m1 = new airticket_order(FightDate, FightNo, classtype, From, To, adult_price, child_price, infant_price, staffID);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            m1.Top = (primScreenHeight - m1.Height) / 2;
            m1.Left = (primScreenWidth - m1.Width) / 2;
            m1.Show();
        }

        class airticket
        {
            public string ArrAirport { get; set; }
            public DateTime ArrDateTime { get; set; }
            public string Class { get; set; }
            public string DepAirport { get; set; }
            public DateTime DepDateTime { get; set; }
            public string FlightNo { get; set; }
            public int Price_Adult { get; set; }
            public int Price_Children { get; set; }
            public int Price_Infant { get; set; }
            public string airlineName { get; set; }
            public string airline_photo { get; set; }




        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
