using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.ObjectModel;


namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewAdress.xaml
    /// </summary>
    public partial class ViewAdress : Window, IObserver
    {
        public ViewAdress()
        {
            InitializeComponent();
            this.adressDao = adressDao ?? throw new ArgumentNullException(nameof(adressDao));
            adressDao = new AdressDao();
            Adresses = new ObservableCollection<Adress>();
            adressDao.AdressStudent.Subscribe(this);
            Update();
            DataContext = this;
        }
        public ObservableCollection<Adress> Adresses { get; set; }
        public Adress SelectedAdress { get; set; }
        private AdressDao adressDao;
        public void Update()
        //{
        //  Adresses.Clear();
        //  var adressList = adressDao.GetAll();
        //  foreach (var adress in adressList)
        // {
        //     Adresses.Add(adress);
        // }
        // }
        {
            Adresses.Clear();
            try
            {
                var adressList = adressDao.GetAll();
                foreach (var adress in adressList)
                {
                    Adresses.Add(adress);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating address list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        //{
        //    CreateAdress createAdress = new CreateAdress(adressDao);
        //    createAdress.Show();
        //}
        {
            if (SelectedAdress == null)
            {
                MessageBox.Show("Please select an address to delete.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this address?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    adressDao.RemoveAdress(SelectedAdress.Id);
                    Update(); // Refresh the list
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting address: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedAdress == null)
            {
                MessageBox.Show("Please select a adress to update.");
                return;
            }
            UpdateAdress updateAdress = new UpdateAdress(SelectedAdress, adressDao);
            updateAdress.Show();

        }
        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedAdress == null)
            {
                MessageBox.Show("Please select a adress to delete.");
                return;
            }

            adressDao.RemoveAdress(SelectedAdress.Id);
            Update();
        }
    }
}
