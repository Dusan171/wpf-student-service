using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentService.Gui
{
    public partial class ViewAddresses : Window, IObserver
    {
        public ObservableCollection<Adress> Addresses { get; set; }
        public Adress SelectedAddress { get; set; }
        private AddressDao addressDao;

        public ViewAddresses()
        {
            InitializeComponent();
            addressDao = new AddressDao();
            Addresses = new ObservableCollection<Adress>();
            Update();
            addressDao.AddressSubject.Subscribe(this);
            DataContext = this;
        }

        public void Update()
        {
            Addresses.Clear();
            var addressList = addressDao.GetAll();
            foreach (var address in addressList)
            {
                Addresses.Add(address);
            }
        }

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateAdress createAddress = new CreateAdress(addressDao);
            createAddress.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedAddress == null)
            {
                MessageBox.Show("Please select an address to update.");
                return;
            }
            UpdateAddress updateAddress = new UpdateAddress(SelectedAddress, addressDao);
            updateAddress.Show();
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedAddress == null)
            {
                MessageBox.Show("Please select an address to delete.");
                return;
            }

            addressDao.RemoveAddress(SelectedAddress.Id);
            Update();
        }
    }
}
