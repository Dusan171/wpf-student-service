using StudentService.DAO;
using StudentService.Model;
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
    /// Interaction logic for ViewGRADE.xaml
    /// </summary>
    public partial class ViewGRADE : Window, IObserver
    {
        public ObservableCollection<Grade> Grades { get; set; }
        public Grade SelectedGrade { get; set; }
        private GradeDao gradeDao;
        public ViewGRADE()
        {
            InitializeComponent();
            gradeDao = new GradeDao();
            Grades = new ObservableCollection<Grade>();
            Update();
            gradeDao.StudentGrade.Subscribe(this);
            DataContext = this;
        }
        public void Update()
        {
            Grades.Clear();
            var gradeList = gradeDao.GetAll();
            foreach (var grade in gradeList)
            {
                Grades.Add(grade);
            }
        }

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateDAOGrade createDAOGrade = new CreateDAOGrade(gradeDao);
            createDAOGrade.Show();

        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                return;
            }
            UpdateGrade updateGrade = new UpdateGrade(SelectedGrade, gradeDao);
            updateGrade.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                return;
            }

            gradeDao.RemoveGrade(SelectedGrade.Id);
            Update();

        }
    }
}
