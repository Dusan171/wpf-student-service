using StudentService.Model;
using StudentService.DAO;
using System;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    public partial class UpdateProfessor : Window, INotifyPropertyChanged
    {
        private readonly ProfessorDao professorDao;
        private Professor currentProfessor;

        public string Surname
        {
            get => currentProfessor.Surname;
            set
            {
                currentProfessor.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string ProfName
        {
            get => currentProfessor.Name;
            set
            {
                currentProfessor.Name = value;
                OnPropertyChanged(nameof(ProfName));
            }
        }

        public DateTime? DateOfBirth
        {
            get => currentProfessor.DateOfBirth.ToDateTime(new TimeOnly());
            set
            {
                currentProfessor.DateOfBirth = DateOnly.FromDateTime(value ?? DateTime.Now);
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public string Address
        {
            get => currentProfessor.Address;
            set
            {
                currentProfessor.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string Phone
        {
            get => currentProfessor.Phone;
            set
            {
                currentProfessor.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => currentProfessor.Email;
            set
            {
                currentProfessor.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string IdNumber
        {
            get => currentProfessor.IdNumber;
            set
            {
                currentProfessor.IdNumber = value;
                OnPropertyChanged(nameof(IdNumber));
            }
        }

        public string Vocation
        {
            get => currentProfessor.Vocation;
            set
            {
                currentProfessor.Vocation = value;
                OnPropertyChanged(nameof(Vocation));
            }
        }

        public int YearsOfService
        {
            get => currentProfessor.YearsOfService;
            set
            {
                currentProfessor.YearsOfService = value;
                OnPropertyChanged(nameof(YearsOfService));
            }
        }

        public UpdateProfessor(Professor professor, ProfessorDao professorDao)
        {
            InitializeComponent();
            this.professorDao = professorDao;
            this.currentProfessor = professor;
            DataContext = this;
        }

        private void UpdateProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                professorDao.UpdateProfessor(currentProfessor);
                MessageBox.Show("Professor updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating professor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}