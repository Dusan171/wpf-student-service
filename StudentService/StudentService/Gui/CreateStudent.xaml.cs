using StudentService.Model.Enums;
using StudentService.Model;
using System;
using System.ComponentModel; // Include this namespace
using System.Windows;
using StudentService.DAO;
using System.Collections.ObjectModel;

namespace StudentService.Gui
{
    public partial class CreateStudent : Window, INotifyPropertyChanged // Implement INotifyPropertyChanged
    {
        private StudentDao studentDao;

        // Properties with INotifyPropertyChanged implementation
        private string surname;
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string studName;
        public string StudName
        {
            get => studName;
            set
            {
                studName = value;
                OnPropertyChanged(nameof(StudName));
            }
        }

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        private string address;
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string index;
        public string Index
        {
            get => index;
            set
            {
                index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        private int yearOfStudy;
        public int YearOfStudy
        {
            get => yearOfStudy;
            set
            {
                yearOfStudy = value;
                OnPropertyChanged(nameof(YearOfStudy));
            }
        }

        private StudentStatus status;
        public StudentStatus Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ObservableCollection<StudentStatus> StudentStatuses { get; set; }

     

        public CreateStudent(StudentDao studentDao)
        {
            InitializeComponent();
            this.studentDao = studentDao;
            DataContext = this;

            StudentStatuses = new ObservableCollection<StudentStatus>();
            StudentStatuses.Add(StudentStatus.BUDGET);
            StudentStatuses.Add(StudentStatus.SELF_FINANCE);
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

        private void PotvrdiButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                //MessageBox.Show("All fields must be filled!");
                return;
            }
            try
            {
                Student newStudent = new Student
                {
                    Surname = Surname,
                    Name = StudName,
                    DateOfBirth = DateOnly.FromDateTime(DateOfBirth.Value),
                    Address = Address,
                    Phone = Phone,
                    Email = Email,
                    Index = Index,
                    YearOfStudy = YearOfStudy,
                    Status = Status,
                    AvgGrade = 0
                };

                studentDao.Create(newStudent);
                ClearFields();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OdustaniButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void ClearFields()
        {
            Surname = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Index = string.Empty;
            YearOfStudy = 0;
            Status = default; // Reset to the default value
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
