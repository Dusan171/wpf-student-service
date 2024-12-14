
using StudentService.Model;
using System;
using System.ComponentModel;
using System.Windows;
using StudentService.DAO;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateGrade.xaml
    /// </summary>
    public partial class CreateGrade : Window, INotifyPropertyChanged
    {
        private GradeDao gradeDao;
        //public int Id { get; set; }
        private int id;
        public int Id
        {
            get => id;
            set
            {
                 id= value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //public Student PassedStudent { get; set; }
        private Student passedStudent;
        public Student PassedStudent
        {
            get => passedStudent;
            set
            {
                passedStudent= value;
                OnPropertyChanged(nameof(PassedStudent));
            }
        }
        //public Subject Subject { get; set; }
        private Subject subject;
        public Subject Subject
        {
            get => subject;
            set
            {
                subject= value;
                OnPropertyChanged(nameof(Subject));
            }
        }
        //public int Value { get; set; }
        private int value;
        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        // public DateOnly Date { get; set; }
        
        private DateOnly? date;
        public DateOnly? Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public CreateGrade(GradeDao gradeDao)
        {
            InitializeComponent();
            this.gradeDao = gradeDao;
            DataContext = this;
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( PassedStudent == null || Subject == null)
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!Date.HasValue)
                {
                    MessageBox.Show("Please select a date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Grade newGrade = new Grade
                {
                    Id = Id,
                    Value = Value,
                    Date = Date.Value, // Using DateOnly directly
                    Subject = Subject,
                    PassedStudent = PassedStudent
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding grade: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        { 
            Id = 0;
            Value = 0;
            PassedStudent = null;
            Subject = null;
            Date = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
