//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
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
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewIndex.xaml
    /// </summary>
    public partial class ViewIndex : Window, IObserver
    {
        public ObservableCollection<StudentIndex> Indexes { get; set; }
        public StudentIndex SelectedIndex { get; set; }
        private IndexDao indexDao;

        public ViewIndex()
        {
            InitializeComponent();
            indexDao = new IndexDao();
            Indexes = new ObservableCollection<StudentIndex>();
            Update();
            indexDao.IndexStudent.Subscribe(this);
            DataContext = this;
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
            CreateIndex createIndex = new CreateIndex(indexDao);
            createIndex.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex == null)
            {
                MessageBox.Show("Please select a index to update.");
                return;
            }
            UpdateIndex updateIndex = new UpdateIndex(SelectedIndex, indexDao);
            updateIndex.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex == null)
            {
                MessageBox.Show("Please select a index to delete.");
                return;
            }

            indexDao.RemoveIndex(SelectedIndex.Id);
            Update();
        }
    }
}
