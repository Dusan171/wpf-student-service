using StudentService.Model;
using StudentService.DAO;
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
        private Grade currentGrade;

        // public int Id { get; set; }
        public int Id
        {
            get => currentGrade.Id;
            set
            {
                currentGrade.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        // public Student PassedStudent { get; set; }
        public Student PassedStudent
        {
            get => currentGrade.PassedStudent;
            set
            {
                currentGrade.PassedStudent = value;
                OnPropertyChanged(nameof(PassedStudent));
            }
        }

        // public Subject Subject { get; set; }
        public Subject Subject
        {
            get => currentGrade.Subject;
            set
            { 
                currentGrade.Subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }

        // public int Value { get; set; }
        public int Value
        {
            get => currentGrade.Value;
            set
            {
                currentGrade.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        // public DateOnly Date { get; set; }
        public DateTime? Date
        {
            get => currentGrade.Date.ToDateTime(new TimeOnly());
            set
            {
                currentGrade.Date = DateOnly.FromDateTime(value ?? DateTime.Now);
                OnPropertyChanged(nameof(Date));
            }
        }

        public UpdateGrade(Grade grade, GradeDao gradeDao)
        {
            InitializeComponent();
            this.gradeDao = gradeDao;
            this.currentGrade = grade;
            DataContext = this;
        }

        private void UpdateGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
