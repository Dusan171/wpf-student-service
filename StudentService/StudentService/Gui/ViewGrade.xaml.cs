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
    /// Interaction logic for ViewGrade.xaml
    /// </summary>
    public partial class ViewGrade : Window
    {
        //public int Id { get; set; }
        // public Student PassedStudent { get; set; }
        // public Subject Subject { get; set; }
        // public int Value { get; set; }
        // public DateOnly Date { get; set; }

        public ObservableCollection<Grade> Grades { get; set; }
        public Grade SelectedGrade { get; set; }
        private GradeDao gradeDao;

        public ViewGrade()
        {
            InitializeComponent();
            gradeDao = new GradeDao();
            Grades = new ObservableCollection<Grade>();
            Update();
            //gradeDao.ProfessorSubject.Subscribe(this);
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
            CreateGrade createGrade = new CreateGrade(gradeDao);
            createGrade.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                MessageBox.Show("Please select a grade to update.");
                return;
            }
            UpdateGrade updateGrade = new UpdateGrade(SelectedGrade, gradeDao);
            updateGrade.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                MessageBox.Show("Please select a grade to delete.");
                return;
            }

            gradeDao.RemoveGrade(SelectedGrade.Id);
            Update();
        }
    }
}
