using StudentService.DAO;
using StudentService.Model.Enums;
using StudentService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void UpdateStudentButton_Click(object sender, RoutedEventArgs e)
        {
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
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
