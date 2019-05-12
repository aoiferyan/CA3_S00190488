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
using System.Data.Entity;

namespace CA3_S00190488
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        Entities1 db = new Entities1();

        public string[] Sizes { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Sizes = new string[] { "All", "Small", "Medium", "Large" };

            DataContext = this;
        }

        //This is what happens when you click the search button
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string x = Startpicker.SelectedDate.ToString();
            DateTime StartDay = DateTime.Parse(x);
            string y = Endpicker.SelectedDate.ToString();
            DateTime EndDay = DateTime.Parse(y);


            //if statement to determine what happens when the combobox is changed. This determines what size is selected 
            //and also makes sure that what is displayed is available on those dates and doesn't have a previous booking on 
            //those dates
            if (SizeComboBox.Text != "All")
            {
                
                var query = from c in db.Cars
                            join b in db.Bookings on c.Id equals b.CarId
                            where (c.Size.Equals(SizeComboBox.Text) && StartDay < b.StartDate && EndDay < b.StartDate) || (c.Size.Equals(SizeComboBox.Text) && StartDay > b.EndDate && EndDay > b.EndDate)
                            select c;

                lbxavailablecars.ItemsSource = query.ToList().Distinct();

           
            }
            else
            {
                var query = from c in db.Cars
                            join b in db.Bookings on c.Id equals b.CarId
                            where (StartDay < b.StartDate && EndDay < b.StartDate) || (StartDay > b.EndDate && EndDay > b.EndDate)
                            select c;

                lbxavailablecars.ItemsSource = query.ToList().Distinct();


            }


        }

        private void lbxavailablecars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //create car object
            Car selectedCar = lbxavailablecars.SelectedItem as Car;
            string x = Startpicker.SelectedDate.ToString();
            DateTime StartDay = DateTime.Parse(x);
            string y = Endpicker.SelectedDate.ToString();
            DateTime EndDay = DateTime.Parse(y);
            selectedCar.StartDate1 = StartDay;
            selectedCar.EndDate1 = EndDay;

            //if statement to check which car has been picked and display an image of that car
            if (selectedCar.Model == "Avensis")
            {

                imgCar.Source = new BitmapImage(new Uri("/Images/Avensis.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Corolla")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Corolla.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Corsa")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Corsa.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Fiesta")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Fiesta1.jpeg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Focus")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Focus.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "GrandlandX")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Grandlandx.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Passat")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Passat.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Polo")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Polo.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Tiguan")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Tiguan.jpg", UriKind.Relative));
            }
            else if (selectedCar.Model == "Yaris")
            {
                imgCar.Source = new BitmapImage(new Uri("/Images/Yaris.jpg", UriKind.Relative));
            }

            //displays the details of the car selected through the GetDetails method in the partial car class
            Txtblkselectedcar.Text = selectedCar.GetCarDetail();

        }

        //This is what happens when the book button is clicked
        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {

            //using (var context = new Entities1())
            //{
                //create car object
                Car selectedCar = lbxavailablecars.SelectedItem as Car;

                string x = Startpicker.SelectedDate.ToString();
                DateTime StartDay = DateTime.Parse(x);
                string y = Endpicker.SelectedDate.ToString();
                DateTime EndDay = DateTime.Parse(y);
                selectedCar.StartDate1 = StartDay;
                selectedCar.EndDate1 = EndDay;


                //add booking to the database
                Booking b = new Booking();
                {
                    b.CarId = selectedCar.Id;
                    b.StartDate = StartDay;
                    b.EndDate = EndDay;
                    
                };
                db.Bookings.Add(b);
                db.SaveChanges();

                //displays a message box of booking details using the getdetails method
                MessageBox.Show($"CONFIRMATION\n\n{selectedCar.GetCarDetail()}");
            //}
            Application.Current.Shutdown();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
