using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    public partial class ViewGrade : Window, IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<Grade> Grades { get; set; }
        public Grade SelectedGrade { get; set; }
        private GradeDao gradeDao;
        private StudentDao studentDao;

        public ViewGrade(StudentDao studentDao)
        {
            InitializeComponent();
            this.gradeDao = new GradeDao();
            this.studentDao = studentDao;
            Grades = new ObservableCollection<Grade>();
            Update();
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
            var createGradeWindow = new CreateGrade(gradeDao, studentDao);
            createGradeWindow.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                MessageBox.Show("Please select a grade to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var updateGradeWindow = new UpdateGrade(SelectedGrade, gradeDao, studentDao);
            updateGradeWindow.Show();
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                MessageBox.Show("Please select a grade to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            gradeDao.RemoveGrade(SelectedGrade.Id);
            Update();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



