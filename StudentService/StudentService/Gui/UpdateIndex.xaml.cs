using StudentService.Model;
using StudentService.DAO;
using System;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateIndex.xaml
    /// </summary>
    public partial class UpdateIndex : Window, INotifyPropertyChanged
    {
        private readonly IndexDao indexDao;
        private StudentIndex currentIndex;

        //public int Id { get; set; }
        public int Id
        {
            get => currentIndex.Id;
            set
            {
                currentIndex.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //public string CourseCode { get; set; }
        public string CourseCode
        {
            get => currentIndex.CourseCode;
            set
            {
                currentIndex.CourseCode = value;
                OnPropertyChanged(nameof(CourseCode));
            }
        }
        //public int RegisterNumber { get; set; }
        public int RegisterNumber
        {
            get => currentIndex.RegisterNumber;
            set
            {
                currentIndex.RegisterNumber = value;
                OnPropertyChanged(nameof(RegisterNumber));
            }
        }
        // public int RegisterYear { get; set; }

        public int RegisterYear
        {
            get => currentIndex.RegisterYear;
            set
            {
                currentIndex.RegisterYear = value;
                OnPropertyChanged(nameof(RegisterYear));
            }
        }

        public UpdateIndex(StudentIndex index, IndexDao indexDao)
        {
            InitializeComponent();
            this.indexDao = indexDao;
            this.currentIndex = index;
            DataContext = this;
        }

        private void UpdateIndexButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CourseCode) || RegisterNumber <= 0 || RegisterYear <= 0)
                {
                    MessageBox.Show("Please fill in all fields with valid data.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                indexDao.UpdateIndex(currentIndex);
                MessageBox.Show("Index updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating index: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
