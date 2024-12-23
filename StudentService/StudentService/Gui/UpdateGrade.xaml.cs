using StudentService.DAO;
using StudentService.Model;
using System;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateGrade.xaml
    /// </summary>
    public partial class UpdateGrade : Window, INotifyPropertyChanged
    {
        private readonly GradeDao gradeDao;
        private readonly StudentDao studentDao;
        private Grade currentGrade;

        public int StudentId
        {
            get => currentGrade.PassedStudent.Id;
            set
            {
                currentGrade.PassedStudent.Id = value;
                OnPropertyChanged(nameof(StudentId));
            }
        }

        public int SubjectId
        {
            get => currentGrade.Subject.Id;
            set
            {
                currentGrade.Subject.Id = value;
                OnPropertyChanged(nameof(SubjectId));
            }
        }

        public int Value
        {
            get => currentGrade.Value;
            set
            {
                currentGrade.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public DateTime? Date
        {
            get => currentGrade.Date.ToDateTime(new TimeOnly());
            set
            {
                currentGrade.Date = DateOnly.FromDateTime(value ?? DateTime.Now);
                OnPropertyChanged(nameof(Date));
            }
        }

        public UpdateGrade(Grade grade, GradeDao gradeDao, StudentDao studentDao)
        {
            InitializeComponent();
            this.gradeDao = gradeDao;
            this.studentDao = studentDao;
            this.currentGrade = grade;
            DataContext = this;
        }

        private void UpdateGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ensure the student exists
                if (studentDao.GetById(StudentId) == null)
                {
                    MessageBox.Show("Student not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                gradeDao.UpdateGrade(currentGrade);
                MessageBox.Show("Grade updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating grade: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
