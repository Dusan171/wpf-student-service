using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

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
            adressDao = new AdressDao();
            Adresses = new ObservableCollection<Adress>();
            Update();
            adressDao.AdressStudent.Subscribe(this);
            DataContext = this;
        }
        public ObservableCollection<Adress> Adresses { get; set; }
        public Adress SelectedAdress { get; set; }
        private AdressDao adressDao;

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
            CreateAdress createAdress = new CreateAdress(adressDao);
            createAdress.Show();
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
