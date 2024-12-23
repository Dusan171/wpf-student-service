using StudentService.DAO;
using StudentService.Model;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    public partial class CreateGrade : Window, INotifyPropertyChanged
    {
        private GradeDao gradeDao;
        private StudentDao studentDao;

        public CreateGrade(GradeDao gradeDao, StudentDao studentDao)
        {
            InitializeComponent();
            this.gradeDao = gradeDao;
            this.studentDao = studentDao;
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(StudentIdTextBox.Text) ||
                string.IsNullOrEmpty(SubjectIdTextBox.Text) ||
                string.IsNullOrEmpty(GradeValueTextBox.Text) ||
                GradeDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please fill in all fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
