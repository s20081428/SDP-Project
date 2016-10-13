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
    /// Interaction logic for attraction_enquiry.xaml
    /// </summary>
    public partial class attraction_enquiry : Page
    {
        private string staffID;
        public attraction_enquiry(string staffid)
        {
            this.staffID = staffid;
            InitializeComponent();
        }

        private void txt_attraction_location_TextChanged(object sender, TextChangedEventArgs e)
        {
            lb_attraction_location.Visibility = Visibility.Visible;
            lb_attraction_location.Items.Clear();

            if (txt_attraction_location.Text == "" || txt_attraction_location.Text == null)
            {
                using (var classicContext = new project_dbEntities())
                {
                    //GET ALL CITY NAME AND ADD TO LISTBOX
                    var city = (from c in classicContext.attractions
                                select new { c.City }).Distinct();

                    foreach (var output in city.ToList())
                    {
                        lb_attraction_location.Items.Add(output.City);
                    }
                }
            }
            else
            {
                using (var classicContext = new project_dbEntities())
                {
                    //GET RELATED CITY NAME AND ADD TO LISTBOX
                    var city = (from c in classicContext.attractions
                                where c.City.Contains(txt_attraction_location.Text)
                                select new { c.City }).Distinct();

                    foreach (var output in city.ToList())
                    {
                        lb_attraction_location.Items.Add(output.City);
                    }
                }
            }
        }

        private void lb_attraction_location_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SET LISTBOX HIDDEN WHEN USER SELECTED A ITEM FROM LIST BOX
            try
            {
                txt_attraction_location.Text = lb_attraction_location.SelectedItem.ToString();
                lb_attraction_location.Items.Clear();
                lb_attraction_location.Visibility = Visibility.Collapsed;
            }
            catch (NullReferenceException){ }
        }

        private void btn_attraction_order(object sender, RoutedEventArgs e)
        {
            //GET SELECTED ROW VALUES
            Object row = dg_attraction_result.SelectedItem;
            string attraction_name = (dg_attraction_result.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            string duration = (dg_attraction_result.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            string cancellation = (dg_attraction_result.SelectedCells[3].Column.GetCellContent(row) as TextBlock).Text;
            string city = (dg_attraction_result.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            string child_price = (dg_attraction_result.SelectedCells[4].Column.GetCellContent(row) as TextBlock).Text;
            string adult_price = (dg_attraction_result.SelectedCells[5].Column.GetCellContent(row) as TextBlock).Text;
            string staffID = this.staffID;
            string photo_attract;

            using (var classicContext = new project_dbEntities())
            {
                //GET ATTRACTPHOTO PATH
                var photo = (from c in classicContext.attractions
                                  where c.City.Equals(city) && c.AttractName.Equals(attraction_name)
                                  select new { c.AttractPhoto }).FirstOrDefault();

                photo_attract = photo.AttractPhoto;
            }

            attraction_order attraction = new attraction_order(attraction_name, duration, cancellation, city, adult_price, child_price, staffID, photo_attract);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            attraction.Top = (primScreenHeight - attraction.Height) / 2;
            attraction.Left = (primScreenWidth - attraction.Width) / 2;
            //DISABLE ORDER BUTTON IF LOGIN AS GUEST
            if (staffID != "guest")
            {
                attraction.Show();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lb_attraction_location.Visibility = Visibility.Collapsed;
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
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
            im_attraction.Source = null;
            dg_attraction_result.Items.Clear();
            string city = "";
            try
            {
                city = txt_attraction_location.Text;
            }
            catch (NullReferenceException) {
                MessageBox.Show("Location is required!!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var attraction = (from c in classicContext.attractions
                                  join p in classicContext.attractprices on c.AttractName equals p.AttractName
                                  where c.City.Equals(city)
                                  select new { c.AttractName, c.Cancellation, c.City, c.Duration, p.ChildPrice, p.AdultPrice }).Distinct();

                foreach (var output in attraction.ToList())
                {
                    dg_attraction_result.Items.Add(new { AttractName = output.AttractName, Duration = output.Duration, City = output.City, Cancellation = output.Cancellation, AdultPrice = output.AdultPrice, ChildPrice = output.ChildPrice, ShowButton = showbutton });
                }
            }
        }

        private void dg_attraction_result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object row = dg_attraction_result.SelectedItem;
            string attraction_name ="";
            string city="";
            try
            {
                attraction_name = (dg_attraction_result.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
                city = (dg_attraction_result.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            }
            catch (NullReferenceException)
            {

            }

            if (attraction_name != "" && city != "")
            {
                using (var classicContext = new project_dbEntities())
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var attraction = (from c in classicContext.attractions
                                      where c.City.Equals(city) && c.AttractName.Equals(attraction_name)
                                      select new { c.AttractPhoto }).FirstOrDefault();


                    im_attraction.Source = new BitmapImage(new Uri("pack://application:,,,/Images/attractionPhotos/" + attraction.AttractPhoto));

                }
            }
        }
    }
}
