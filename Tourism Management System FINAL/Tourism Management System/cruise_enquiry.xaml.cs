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
using System.Collections.ObjectModel;

namespace Tourism_Management_System
{
    /// <summary>
    /// Interaction logic for cruise_enquiry.xaml
    /// </summary>
    public partial class cruise_enquiry : Page
    {
        private string staffID;
        public cruise_enquiry(string staffid)
        {
            this.staffID = staffid;
            InitializeComponent();
        }

        private void btn_cruise_search_Click(object sender, RoutedEventArgs e)
        {
           Object showbutton;

            if (staffID == "guest")
            {
                showbutton = Visibility.Collapsed;
            }
            else
            {
                showbutton = Visibility.Visible;
            }
            ObservableCollection<Cruise> empData = new ObservableCollection<Cruise>();
            if ((cb_cruise_organizer.SelectedValue == null) && (cb_cruise_month.SelectedValue == null))
            {
                using (var classicContext = new project_dbEntities())
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var attraction = (from c in classicContext.cruises
                                      join o in classicContext.cruiseorganizers on c.OrganID equals o.OrganID
                                      orderby c.TourDay ascending
                                      select new { c.CruiseNo, c.CruiseName, c.RefPrice, c.TourDay, c.OrganID, o.OrganizerC, o.OrganizerE, c.StartDate });

                    foreach (var output in attraction.ToList())
                    {
                        empData.Add(new Cruise { CruiseNo = output.CruiseNo, CruiseName = output.CruiseName, RefPrice = output.RefPrice, TourDay = output.TourDay, OrganizerC = output.OrganizerC + " (" + output.OrganizerE + ")", StartDate = output.StartDate.Date, ShowButton = showbutton });
                    }
                }
            }
            else if (cb_cruise_organizer.SelectedValue != null && cb_cruise_month.SelectedValue != null)
            {
                int cruiseorganizer = int.Parse(cb_cruise_organizer.SelectedValue.ToString());
                int cruiseMonth = int.Parse(cb_cruise_month.SelectedValue.ToString());

                using (var classicContext = new project_dbEntities())
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var attraction = (from c in classicContext.cruises
                                      join o in classicContext.cruiseorganizers on c.OrganID equals o.OrganID
                                      where c.OrganID.Equals(cruiseorganizer) && c.StartDate.Month >= cruiseMonth
                                      orderby c.TourDay ascending
                                      select new { c.CruiseNo, c.CruiseName, c.RefPrice, c.TourDay, o.OrganizerC, o.OrganizerE, c.StartDate });

                    foreach (var output in attraction.ToList())
                    {
                        empData.Add(new Cruise { CruiseNo = output.CruiseNo, CruiseName = output.CruiseName, RefPrice = output.RefPrice, TourDay = output.TourDay, OrganizerC = output.OrganizerC + " (" + output.OrganizerE + ")", StartDate = output.StartDate, ShowButton = showbutton });
                    }
                }
            }
            else if (cb_cruise_organizer.SelectedValue != null && cb_cruise_month.SelectedValue == null)
            {
                int cruiseorganizer = int.Parse(cb_cruise_organizer.SelectedValue.ToString());


                using (var classicContext = new project_dbEntities())
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var attraction = (from c in classicContext.cruises
                                      join o in classicContext.cruiseorganizers on c.OrganID equals o.OrganID
                                      where c.OrganID.Equals(cruiseorganizer)
                                      orderby c.TourDay ascending
                                      select new { c.CruiseNo, c.CruiseName, c.RefPrice, c.TourDay, o.OrganizerC, o.OrganizerE, c.StartDate });

                    foreach (var output in attraction.ToList())
                    {
                        empData.Add(new Cruise { CruiseNo = output.CruiseNo, CruiseName = output.CruiseName, RefPrice = output.RefPrice, TourDay = output.TourDay, OrganizerC = output.OrganizerC + " (" + output.OrganizerE + ")", StartDate = output.StartDate, ShowButton = showbutton });
                    }
                }
            }
            else if (cb_cruise_organizer.SelectedValue == null && cb_cruise_month.SelectedValue != null)
            {
                int cruiseMonth = int.Parse(cb_cruise_month.SelectedValue.ToString());

                using (var classicContext = new project_dbEntities())
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var attraction = (from c in classicContext.cruises
                                      join o in classicContext.cruiseorganizers on c.OrganID equals o.OrganID
                                      where c.StartDate.Month >= cruiseMonth
                                      orderby c.TourDay ascending
                                      select new { c.CruiseNo, c.CruiseName, c.RefPrice, c.TourDay, o.OrganizerC, o.OrganizerE, c.StartDate });

                    foreach (var output in attraction.ToList())
                    {
                        empData.Add(new Cruise { CruiseNo = output.CruiseNo, CruiseName = output.CruiseName, RefPrice = output.RefPrice, TourDay = output.TourDay, OrganizerC = output.OrganizerC + " (" + output.OrganizerE + ")", StartDate = output.StartDate, ShowButton = showbutton });
                    }
                }
            }
            ListCollectionView collection = new ListCollectionView(empData);

            collection.GroupDescriptions.Add(new PropertyGroupDescription("TourDay"));

            dg_cruise.ItemsSource = collection;
        }

        public class Cruise
        {
            public string CruiseNo { get; set; }
            public string CruiseName { get; set; }
            public int RefPrice { get; set; }
            public int TourDay { get; set; }
            public string OrganizerC { get; set; }
            public DateTime StartDate { get; set; }
            public Object ShowButton { get; set; }
        }

        private void cb_cruise_month_Loaded(object sender, RoutedEventArgs e)
        {

            cb_cruise_month.Items.Clear();
            cb_cruise_month.DisplayMemberPath = "Key";
            cb_cruise_month.SelectedValuePath = "Value";


            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("---Select All---", null));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("January", "1"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("February", "2"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("March", "3"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("April", "4"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("May", "5"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("June", "6"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("July", "7"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("August", "8"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("September", "9"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("October", "10"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("November", "11"));
            cb_cruise_month.Items.Add(new KeyValuePair<string, string>("December", "12"));

        }

        private void cb_cruise_organizer_Loaded(object sender, RoutedEventArgs e)
        {
            cb_cruise_organizer.Items.Clear();
            cb_cruise_organizer.DisplayMemberPath = "Key";
            cb_cruise_organizer.SelectedValuePath = "Value";

            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var cruiseorganizers = (from c in classicContext.cruiseorganizers
                                        select new { c.OrganID, c.OrganizerC, c.OrganizerE });

                cb_cruise_organizer.Items.Add(new KeyValuePair<string, string>("--------------Select All--------------", null));
                foreach (var output in cruiseorganizers.ToList())
                {
                    cb_cruise_organizer.Items.Add(new KeyValuePair<string, string>(output.OrganizerC + " (" + output.OrganizerE + ")", Convert.ToString(output.OrganID)));
                }
            }
        }

        private void btn_cruise_order(object sender, RoutedEventArgs e)
        {
            Object row = dg_cruise.SelectedItem;

            string cruiseName = (dg_cruise.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            DateTime startday;
            string staffID = this.staffID;
            double cruise_price = double.Parse((dg_cruise.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dg_cruise.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text.Length-1));
            string cruiseNo;
            int tourDay;

            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var cruise = (from c in classicContext.cruises
                              where c.CruiseName.Equals(cruiseName) 
                              select new { c.StartDate,c.CruiseNo, c.TourDay }).FirstOrDefault();

                startday = cruise.StartDate;
                cruiseNo = cruise.CruiseNo;
                tourDay = cruise.TourDay;
            }
            cruise_order order = new cruise_order(cruiseNo, cruiseName, tourDay, cruise_price, startday, staffID);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            order.Top = (primScreenHeight - order.Height) / 2;
            order.Left = (primScreenWidth - order.Width) / 2;
            order.Show();
        }

        private void btnshowDetils_Click(object sender, RoutedEventArgs e)
        {
            Object row = dg_cruise.SelectedItem;

            string cruiseName = (dg_cruise.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            string pdf;

            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var cruise = (from c in classicContext.cruises
                              where c.CruiseName.Equals(cruiseName)
                              select new {c.pdf }).FirstOrDefault();

                pdf = cruise.pdf;
            }
            cruise_PDF details = new cruise_PDF(pdf);
            details.Show();
        }
    }
}
