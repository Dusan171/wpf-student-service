using System;
using System.ComponentModel;
using System.Windows;
using StudentService.DAO;
using StudentService.Model;

namespace StudentService.Gui
{
    public partial class CreateAdress : Window, INotifyPropertyChanged
    {
        private readonly AddressDao _addressDao;

        // Properties with INotifyPropertyChanged implementation
        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnPropertyChanged(nameof(Street));
            }
        }

        private int number;
        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        private string town;
        public string Town
        {
            get => town;
            set
            {
                town = value;
                OnPropertyChanged(nameof(Town));
            }
        }

        private string country;
        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public CreateAdress(AddressDao addressDao)
        {
            InitializeComponent();
            _addressDao = addressDao;
            DataContext = this;
        }

        private void AddAddressButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Address instance directly from properties
                Adress newAddress = new Adress
                {
                    Street = Street,
                    Number = Number,
                    Town = Town,
                    Country = Country
                };

                _addressDao.Create(newAddress);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding address: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
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
    }
}
