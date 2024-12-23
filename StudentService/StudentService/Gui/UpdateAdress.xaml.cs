using System;
using System.ComponentModel;
using System.Windows;
using StudentService.Model;
using StudentService.DAO;

namespace StudentService.Gui
{
    public partial class UpdateAddress : Window, INotifyPropertyChanged
    {
        private readonly AddressDao _addressDao;
        private Adress _currentAddress;

        public string Street
        {
            get => _currentAddress.Street;
            set
            {
                _currentAddress.Street = value;
                OnPropertyChanged(nameof(Street));
            }
        }

        public int Number
        {
            get => _currentAddress.Number;
            set
            {
                _currentAddress.Number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public string Town
        {
            get => _currentAddress.Town;
            set
            {
                _currentAddress.Town = value;
                OnPropertyChanged(nameof(Town));
            }
        }

        public string Country
        {
            get => _currentAddress.Country;
            set
            {
                _currentAddress.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public UpdateAddress(Adress address, AddressDao addressDao)
        {
            InitializeComponent();
            _addressDao = addressDao;
            _currentAddress = address;
            DataContext = this;
        }

        private void UpdateAddressButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _addressDao.UpdateAddress(_currentAddress);
                MessageBox.Show("Address updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating address: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
