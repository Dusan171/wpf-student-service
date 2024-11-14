using StudentService.DAO;
using StudentService.Model;
using StudentService.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

using System.Net;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateAdress.xaml
    /// </summary>
    public partial class CreateDAOAdress : Window, INotifyPropertyChanged
    {
        private AddressDao adressDao;

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
            get=> town;
            set
            {
                town = value;
                OnPropertyChanged(nameof(Town));
            }
        }
        private string country;
        public string Country
        {
            get=> country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        public CreateDAOAdress(AddressDao adressDao)
        {
            //InitializeComponent(); //CRVENO
            this.adressDao = adressDao;
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
        private void AddAdressButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Adres instance directly from properties
                Adress newAdress = new Adress
                {
                    Id = id,
                    Street=Street,
                    Number=Number,
                    Town=Town,
                    Country=Country
                };

                adressDao.Create(newAdress);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
    }
}
