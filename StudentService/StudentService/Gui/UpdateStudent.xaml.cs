using StudentService.DAO;
using StudentService.Model.Enums;
using StudentService.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Window, INotifyPropertyChanged
    {
        private readonly StudentDao studentDao;
        private Student currentStudent;

        public string Surname
        {
            get => currentStudent.Surname;
            set
            {
                currentStudent.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string StudName
        {
            get => currentStudent.Name;
            set
            {
                currentStudent.Name = value;
                OnPropertyChanged(nameof(StudName));
            }
        }

        public DateTime? DateOfBirth
        {
            get => currentStudent.DateOfBirth.ToDateTime(new TimeOnly());
            set
            {
                currentStudent.DateOfBirth = DateOnly.FromDateTime(value ?? DateTime.Now);
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public string Address
        {
            get => currentStudent.Address;
            set
            {
                currentStudent.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string Phone
        {
            get => currentStudent.Phone;
            set
            {
                currentStudent.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => currentStudent.Email;
            set
            {
                currentStudent.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Index
        {
            get => currentStudent.Index;
            set
            {
                currentStudent.Index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public int YearOfStudy
        {
            get => currentStudent.YearOfStudy;
            set
            {
                currentStudent.YearOfStudy = value;
                OnPropertyChanged(nameof(YearOfStudy));
            }
        }

        public StudentStatus Status
        {
            get => currentStudent.Status;
            set
            {
                currentStudent.Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ObservableCollection<StudentStatus> StudentStatuses { get; set; }

        public double AvgGrade
        {
            get => currentStudent.AvgGrade;
            set
            {
                currentStudent.AvgGrade = value;
                OnPropertyChanged(nameof(AvgGrade));
            }
        }

        public UpdateStudent(Student student, StudentDao studentDao)
        {
            InitializeComponent();
            this.studentDao = studentDao;
            this.currentStudent = student;
            DataContext = this;

            StudentStatuses = new ObservableCollection<StudentStatus>
            {
                StudentStatus.BUDGET,
                StudentStatus.SELF_FINANCE
            };
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(Surname))
            {
                MessageBox.Show("Surname cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(StudName))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (DateOfBirth == null)
            {
                MessageBox.Show("Date of Birth cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Address))
            {
                MessageBox.Show("Address cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Phone))
            {
                MessageBox.Show("Phone cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Index))
            {
                MessageBox.Show("Index cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (YearOfStudy <= 0)
            {
                MessageBox.Show("Year of Study must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void UpdateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            try
            {
                studentDao.UpdateStudent(currentStudent);
                MessageBox.Show("Student updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

       

        private void OdustaniButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            Surname = string.Empty;
            StudName = string.Empty;
            DateOfBirth = null; // Reset date to null
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Index = string.Empty;
            YearOfStudy = 0;
            Status = default; // Reset to default enum value
            AvgGrade = 0; // Clear avg grade
        }

       
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
