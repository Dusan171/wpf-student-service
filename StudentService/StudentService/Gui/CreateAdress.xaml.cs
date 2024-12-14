using System;
using System.Windows;
using StudentService.DAO;
using System.ComponentModel;
using StudentService.Model;
using System.IO;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateAdress.xaml
    /// </summary>
    public partial class CreateAdress : Window, INotifyPropertyChanged
    {

        private AdressDao adressDao;
        public CreateAdress(AdressDao adressDao)
        {
            InitializeComponent();
            this.adressDao = adressDao;
            DataContext = this;
        }

        //public int Id { get; set; }
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //public string Street { get; set; }
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
        // public int Number { get; set; }
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
        // public string Town { get; set; }
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
        // public string Country { get; set; }

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
        private void AddAdressButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Adress newAdress = new Adress
                {
                    Id = Id,
                    Street = Street,
                    Number = Number,
                    Town = Town,
                    Country = Country
                };

                adressDao.Create(newAdress);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding adress: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }
}
