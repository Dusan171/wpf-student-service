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

using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;



namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewStudentIndex.xaml
    /// </summary>
    public partial class ViewStudentIndex : Window, IObserver
    {
        public ObservableCollection<StudentIndex> Indexes { get; set; }
        public Index SelectedIndex { get; set; }
        private IndexDao indexDao;

        public ViewStudentIndex()
        {
            InitializeComponent();
            indexDao = new IndexDao();
            Indexes = new ObservableCollection<StudentIndex>();
            Update();
            indexDao.Indexstudent.Subscribe(this);
            //DataContext = this; //crveno
        }

        public void Update()
        {
            Indexes.Clear();
            var indexList = indexDao.GetAll();
            foreach (var index in indexList)
            {
                Indexes.Add(index);
            }
        }

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateStudentIndex createIndex = new CreateStudentIndex(indexDao);
            //createStudentIndex.Show();//?
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex == null)
            {
                MessageBox.Show("Please select a index to update.");
                return;
            }
            UpdateStudentIndex updateIndex = new UpdateStudentIndex(SelectedIndex, indexDao);
            updateIndex.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex == null)
            {
                MessageBox.Show("Please select a professor to delete.");
                return;
            }

            indexDao.RemoveIndex(SelectedIndex.Id);
            Update();
        }
    }
}
