using StudentService.DAO;
using StudentService.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateAddress.xaml
    /// </summary>
    public partial class UpdateDAOAddress : Window, INotifyPropertyChanged
    {
        private readonly AddressDao adressDao;
        private Adress currentAddres;
        public int Id
        {
            get => currentAddres.Id;
            set
            {
                currentAddres.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Street
        {
            get => currentAddres.Street;
            set
            {
                currentAddres.Street = value;
                OnPropertyChanged(nameof(Street));
            }
        }
        public int Number
        {
            get => currentAddres.Number;
            set
            {
                currentAddres.Number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public string Town
        {
            get => currentAddres.Town;
            set
            {
                currentAddres.Town = value;
                OnPropertyChanged(nameof(Town));
            }
        }
        public string Country
        {
            get => currentAddres.Country;
            set
            {
                currentAddres.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        public UpdateDAOAddress(Adress address,AddressDao adressDao)
        {
            //InitializeComponent(); //crveno nakon sto sam promijenio ime u UpdateDAOAddress
            this.adressDao = adressDao;
            this.currentAddres = address;
            DataContext = this; 
        }
        private void ClearFields()
        {
            Id = 0;
            Street = string.Empty;
            Number = 0;
            Town = string.Empty;
            Country = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateAdressButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                adressDao.UpdateVehicle(currentAddres);
                MessageBox.Show("Adress updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding adress: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
