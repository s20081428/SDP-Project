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
    /// Interaction logic for hotel_enquiry.xaml
    /// </summary>
    public partial class hotel_enquiry : Page
    {
        private string staffID;
        public hotel_enquiry(string staffid)
        {
            this.staffID = staffid;
            InitializeComponent();
        }

        private void cb_hotel_country_Loaded(object sender, RoutedEventArgs e)
        {
            cb_hotel_country.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {

                //Get country name in hotel
                var getCountry = (from list in classicContext.hotels
                                  select new { list.Country }).Distinct();

                //Show allcountry in to cb_hotel_country
                foreach (var output in getCountry.ToList())
                {
                    cb_hotel_country.Items.Add(output.Country);
                }

            }
        }

        private void cb_hotel_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Clear 'cb_hotel_city' while cb_hotel country selection is changed 
            cb_hotel_city.Items.Clear();
            cb_hotel_district.Items.Clear();
        }

        private void cb_hotel_city_DropDownOpened(object sender, EventArgs e)
        {
            cb_hotel_city.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                var tf_country = "";

                try
                {
                    tf_country = cb_hotel_country.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {

                }

                if (tf_country == null || tf_country == "")
                {
                    MessageBox.Show("Please select country first!");
                }

                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var getCity = (from list in classicContext.hotels
                               where list.Country.Equals(tf_country)
                               select new { list.City }).Distinct();

                //Show all city name with airport name in to cb_To 

                foreach (var output in getCity.ToList())
                {
                    cb_hotel_city.Items.Add(output.City);
                }
            }
        }

        private void cb_hotel_district_DropDownOpened(object sender, EventArgs e)
        {
            cb_hotel_district.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                var tf_city = "";
                var tf_country = "";

                try
                {
                    tf_country = cb_hotel_country.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {

                }

                try
                {
                    tf_city = cb_hotel_city.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {

                }

                if (tf_country == null || tf_country == "")
                {
                    MessageBox.Show("Please select country first!");
                }
                else if (tf_city == null || tf_city == "")
                {
                    MessageBox.Show("Please select city first!");
                }


                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var getDistrict = (from list in classicContext.hotels
                                   where list.City.Equals(tf_city)
                                   select new { list.District }).Distinct();

                //Show all city name with airport name in to cb_To 

                foreach (var output in getDistrict.ToList())
                {
                    cb_hotel_district.Items.Add(output.District);
                }

            }
        }

        private void btn_hotel_search_Click(object sender, RoutedEventArgs e)
        {

            dg_hotel.Items.Clear();
            using (var classicContext = new project_dbEntities())
            {
                string tf_city = "";
                string tf_country = "";
                string tf_district = "";
                string tf_keyword = txt_keyword.Text.Trim();

                try
                {
                    tf_country = cb_hotel_country.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {

                }

                try
                {
                    tf_city = cb_hotel_city.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {

                }

                try
                {
                    tf_district = cb_hotel_district.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {

                }
                if (tf_country == "" || tf_country == null)
                {
                    MessageBox.Show("Country is required to select. Please Try Again.");
                }
                else if (tf_city == "" || tf_city == null)
                {
                    MessageBox.Show("City is required to select. Please Try Again.");

                }
                else if ((tf_district == "" || tf_district == null) && (tf_keyword==null || tf_keyword==""))
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var getDistrict = (from list in classicContext.hotels
                                       where list.Country.Equals(tf_country) && list.City.Equals(tf_city) && (list.ChiName.Contains(tf_keyword) || list.EngName.Contains(tf_keyword))
                                       select new { list.ChiName, list.EngName, list.Star, list.Country, list.City, list.District, list.Address }).Distinct();

                    var getCount = (from list in classicContext.hotels
                                    where list.Country.Equals(tf_country) && list.City.Equals(tf_city)
                                    select new { list }).Count();
                    lab_hotal_count.Content = getCount + " hotel(s) have been found.";
                    var star = "";
                    foreach (var output in getDistrict.ToList())
                    {
                        if (output.Star == 3)
                        {
                            star = "★★★☆☆";
                        }
                        else if (output.Star == Convert.ToDecimal(3.5))
                        {
                            star = "★★★\u00BD☆";
                        }
                        else if (output.Star == 4)
                        {
                            star = "★★★★☆";
                        }
                        else if (output.Star == Convert.ToDecimal(4.5))
                        {
                            star = "★★★★\u00BD";
                        }
                        else if (output.Star == 5)
                        {
                            star = "★★★★★";
                        }
                        dg_hotel.Items.Add(new { Name = output.ChiName + " " + output.EngName, Country = output.Country, City = output.City, District = output.District, Address = output.Address, Star = star });
                    }

                }
                else if (tf_district == "" || tf_district == null)
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var getDistrict = (from list in classicContext.hotels
                                       where list.Country.Equals(tf_country) && list.City.Equals(tf_city) && (list.ChiName.Contains(tf_keyword)||list.EngName.Contains(tf_keyword))
                                       select new { list.ChiName, list.EngName, list.Star, list.Country, list.City, list.District, list.Address }).Distinct();

                    var getCount = (from list in classicContext.hotels
                                    where list.Country.Equals(tf_country) && list.City.Equals(tf_city) 
                                    select new { list }).Count();
                    lab_hotal_count.Content = getCount + " hotel(s) have been found.";
                    var star = "";
                    foreach (var output in getDistrict.ToList())
                    {
                        if (output.Star == 3)
                        {
                            star = "★★★☆☆";
                        }
                        else if (output.Star == Convert.ToDecimal(3.5))
                        {
                            star = "★★★\u00BD☆";
                        }
                        else if (output.Star == 4)
                        {
                            star = "★★★★☆";
                        }
                        else if (output.Star == Convert.ToDecimal(4.5))
                        {
                            star = "★★★★\u00BD";
                        }
                        else if (output.Star == 5)
                        {
                            star = "★★★★★";
                        }
                        dg_hotel.Items.Add(new { Name = output.ChiName + " " + output.EngName, Country = output.Country, City = output.City, District = output.District, Address = output.Address, Star = star });
                    }

                }
                else if (txt_keyword.Text == null || txt_keyword.Text == "")
                {
                    //Get city name, airport name, shortform of city while there are flight in flight schedules
                    var getDistrict = (from list in classicContext.hotels
                                       where list.Country.Equals(tf_country) && list.City.Equals(tf_city) && list.District.Equals(tf_district)
                                       select new { list.ChiName, list.EngName, list.Star, list.Country, list.City, list.District, list.Address }).Distinct();

                    var getCount = (from list in classicContext.hotels
                                    where list.Country.Equals(tf_country) && list.City.Equals(tf_city) && list.District.Equals(tf_district)
                                    select new { list }).Count();
                    lab_hotal_count.Content = getCount + " hotel(s) have been found.";
                    { 
                    var star = "";
                        foreach (var output in getDistrict.ToList())
                        {
                            if (output.Star == 3)
                            {
                                star = "★★★☆☆";
                            }
                            else if (output.Star == Convert.ToDecimal(3.5))
                            {
                                star = "★★★\u00BD☆";
                            }
                            else if (output.Star == 4)
                            {
                                star = "★★★★☆";
                            }
                            else if (output.Star == Convert.ToDecimal(4.5))
                            {
                                star = "★★★★\u00BD";
                            }
                            else if (output.Star == 5)
                            {
                                star = "★★★★★";
                            }
                            dg_hotel.Items.Add(new { Name = output.ChiName + " " + output.EngName, Country = output.Country, City = output.City, District = output.District, Address = output.Address, Star = star });
                        }
                     }
                }
                else
                {
                    var getDistrict = (from list in classicContext.hotels
                                       where list.Country.Equals(tf_country) && list.City.Equals(tf_city) && list.District.Equals(tf_district) && (list.ChiName.Contains(txt_keyword.Text) || list.EngName.Contains(txt_keyword.Text))
                                       select new { list.ChiName, list.EngName, list.Star, list.Country, list.City, list.District, list.Address }).Distinct();

                    var getCount = (from list in classicContext.hotels
                                    where list.Country.Equals(tf_country) && list.City.Equals(tf_city) && list.District.Equals(tf_district) && (list.ChiName.Contains(txt_keyword.Text) || list.EngName.Contains(txt_keyword.Text))
                                    select new { list.ChiName, list.EngName, list.Star, list.Country, list.City, list.District, list.Address }).Count();

                    lab_hotal_count.Content = getCount + " hotel(s) have been found.";
                    var star = "";
                    foreach (var output in getDistrict.ToList())
                    {
                        if (output.Star == 3)
                        {
                            star = "★★★☆☆";
                        }
                        else if (output.Star == Convert.ToDecimal(3.5))
                        {
                            star = "★★★\u00BD☆";
                        }
                        else if (output.Star == 4)
                        {
                            star = "★★★★☆";
                        }
                        else if (output.Star == Convert.ToDecimal(4.5))
                        {
                            star = "★★★★\u00BD";
                        }
                        else if (output.Star == 5)
                        {
                            star = "★★★★★";
                        }
                        dg_hotel.Items.Add(new { Name = output.ChiName + " " + output.EngName, Country = output.Country, City = output.City, District = output.District, Address = output.Address, Star = star });
                    }
                }


            }
        }



        private void dg_hotel_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            dg_hotel_room.Visibility = Visibility.Visible;
            dg_hotel_room.Items.Clear();
            object item = dg_hotel.SelectedItem;
            string Hotel_Address = "";
            try
            {
                Hotel_Address = (dg_hotel.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            }
            catch (NullReferenceException)
            {

            }

            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var Hotel_details = (from r in classicContext.rooms
                                     join h in classicContext.hotels on r.HotelID equals h.HotelID
                                     where h.Address.Equals(Hotel_Address)
                                     select new { h.ChiName, h.EngName, r.RoomType, r.RoomNum, r.RoomSize, r.AdultNum, r.ChildNum, r.RoomDesc, r.Price });

                foreach (var output in Hotel_details.ToList())
                {
                    dg_hotel_room.Items.Add(new { Name = output.ChiName + " " + output.EngName, RoomType = output.RoomType, RoomNum = output.RoomNum, RoomSize = output.RoomSize, AdultNum = output.AdultNum, ChildNum = output.ChildNum, RoomDesc = output.RoomDesc, Price =  output.Price, ShowButton = showbutton });
                }
            }
        }
        private void btn_hotel_order(object sender, RoutedEventArgs e)
        {
            Object row = dg_hotel_room.SelectedItem;
            string Hotel_Name = (dg_hotel_room.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text;
            string Room_Type = (dg_hotel_room.SelectedCells[1].Column.GetCellContent(row) as TextBlock).Text;
            string Room_price = (dg_hotel_room.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text.Substring(1, (dg_hotel_room.SelectedCells[6].Column.GetCellContent(row) as TextBlock).Text.Length-1);
            string staffID = this.staffID;
            string Room_size = (dg_hotel_room.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            string Hotel_ID = "";

            using (var classicContext = new project_dbEntities())
            {
                //Get city name, airport name, shortform of city while there are flight in flight schedules
                var HotelID = (from h in classicContext.hotels
                               where Hotel_Name.Contains(h.ChiName)
                               select new { h.HotelID }).Distinct();

                foreach (var output in HotelID.ToList())
                {
                    Hotel_ID = output.HotelID.ToString();
                }
            }

            hotel_order order = new hotel_order(Hotel_ID, Hotel_Name, Room_Type, Room_price, Room_size, staffID);
            double primScreenHeight = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            double primScreenWidth = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            order.Top = (primScreenHeight - order.Height) / 2;
            order.Left = (primScreenWidth - order.Width) / 2;
            order.Show();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dg_hotel_room.Visibility = Visibility.Collapsed;
        }
    }


}
