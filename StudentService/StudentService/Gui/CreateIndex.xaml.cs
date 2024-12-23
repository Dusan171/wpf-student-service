using StudentService.Model;
using System;
using System.ComponentModel;
using System.Windows;
using StudentService.DAO;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateIndex.xaml
    /// </summary>
    public partial class CreateIndex : Window, INotifyPropertyChanged
    {
        private IndexDao indexDao;

        //public int Id { get; set; }
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //public string CourseCode { get; set; }
        private string courseCode;
        public string CourseCode
        {
            get => courseCode;
            set
            {
                courseCode = value;
                OnPropertyChanged(nameof(CourseCode));
            }
        }
        // public int RegisterNumber { get; set; }
        private int registerNumber;
        public int RegisterNumber
        {
            get => registerNumber;
            set
            {
                registerNumber = value;
                OnPropertyChanged(nameof(RegisterNumber));
            }
        }
        //public int RegisterYear { get; set; }
        private int registerYear;
        public int RegisterYear
        {
            get => registerYear;
            set
            {
                registerYear = value;
                OnPropertyChanged(nameof(RegisterYear));
            }
        }

        public CreateIndex(IndexDao indexDao)
        {
            InitializeComponent();
            this.indexDao = indexDao;
            DataContext = this;
        }

        private void AddIndexButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StudentIndex newStudentIndex = new StudentIndex
                {
                    Id = Id,
                    CourseCode = CourseCode,
                    RegisterNumber = RegisterNumber,
                    RegisterYear = RegisterYear,
                };

                indexDao.Create(newStudentIndex);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding index: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            Id = 0;
            CourseCode = string.Empty;
            RegisterNumber = 0;
            RegisterYear = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}