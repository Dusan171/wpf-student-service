using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ViewAdress.xaml
    /// </summary>
    public partial class ViewAdress : Window, IObserver
    {
        public ObservableCollection<Adress> Adresses { get; set; }
        public Adress SelectedAdress { get; set; }
        private AddressDao adressDao;
        public ViewAdress()
        {
            InitializeComponent();
            adressDao = new AddressDao();
            Adresses = new ObservableCollection<Adress>();
            Update();
            adressDao.AdressStudent.Subscribe(this);
            //DataContext = this;//crveno
        }
        public void Update()
        {
            Adresses.Clear();
            var adressList = adressDao.GetAll();
            foreach (var adress in adressList)
            {
                Adresses.Add(adress);
            }
        }
        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateDAOAdress createAdress = new CreateDAOAdress(adressDao);
            createAdress.Show();
        }
        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedAdress == null)
            {
                return;
            }
            UpdateDAOAddress updateAdress = new UpdateDAOAddress(SelectedAdress, adressDao);
            updateAdress.Show();
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedAdress == null)
            {
                return;
            }

            adressDao.RemoveVehicle(SelectedAdress.Id);
            Update();

        }
    }
}
