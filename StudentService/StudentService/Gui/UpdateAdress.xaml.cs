using StudentService.Model;
using StudentService.DAO;
using System;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateAdress.xaml
    /// </summary>
    public partial class UpdateAdress : Window, INotifyPropertyChanged
    {
        private readonly AdressDao adressDao;
        private Adress currentAdress;

        //  public int Id { get; set; }
        public int Id
        {
            get => currentAdress.Id;
            set
            {
                currentAdress.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        // public string Street { get; set; }
        public string Street
        {
            get => currentAdress.Street;
            set
            {
                currentAdress.Street = value;
                OnPropertyChanged(nameof(Street));
            }
        }
        //  public int Number { get; set; }
        public int Number
        {
            get => currentAdress.Number;
            set
            {
                currentAdress.Number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        //  public string Town { get; set; }
        public string Town
        {
            get => currentAdress.Town;
            set
            {
                currentAdress.Town = value;
                OnPropertyChanged(nameof(Town));
            }
        }
        //  public string Country { get; set; }

        public string Country
        {
            get => currentAdress.Country;
            set
            {
                currentAdress.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public UpdateAdress(Adress adress, AdressDao adressDao)
        {
            InitializeComponent();
            this.adressDao = adressDao;
            this.currentAdress = adress;
            DataContext = this;
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(Street) || string.IsNullOrWhiteSpace(Town) || string.IsNullOrWhiteSpace(Country) || Number <= 0)
            {
                MessageBox.Show("All fields must be filled correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        private void UpdateAdressButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;
            try
            {
                adressDao.UpdateAdress(currentAdress);
                MessageBox.Show("Adress updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating adress: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
