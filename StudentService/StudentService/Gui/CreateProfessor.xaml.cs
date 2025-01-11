using System;
using System.ComponentModel;
using System.Windows;
using StudentService.Model;
using StudentService.DAO;

namespace StudentService.Gui
{
    public partial class CreateProfessor : Window, INotifyPropertyChanged
    {
        private ProfessorDao professorDao;

        private string surname;
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(AreFieldsValid));
            }
        }

        private string name;
        public string FirstName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(AreFieldsValid));
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
                OnPropertyChanged(nameof(AreFieldsValid));
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
                OnPropertyChanged(nameof(AreFieldsValid));
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
                OnPropertyChanged(nameof(AreFieldsValid));
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
                OnPropertyChanged(nameof(AreFieldsValid));
            }
        }

        private string idNumber;
        public string IdNumber
        {
            get => idNumber;
            set
            {
                idNumber = value;
                OnPropertyChanged(nameof(IdNumber));
                OnPropertyChanged(nameof(AreFieldsValid));
            }
        }

        private string vocation;
        public string Vocation
        {
            get => vocation;
            set
            {
                vocation = value;
                OnPropertyChanged(nameof(Vocation));
                OnPropertyChanged(nameof(AreFieldsValid));
            }
        }

        private int yearsOfService;
        public int YearsOfService
        {
            get => yearsOfService;
            set
            {
                yearsOfService = value;
                OnPropertyChanged(nameof(YearsOfService));
                OnPropertyChanged(nameof(AreFieldsValid));
            }
        }

        public bool AreFieldsValid =>
            !string.IsNullOrWhiteSpace(Surname) &&
            !string.IsNullOrWhiteSpace(FirstName) &&
            DateOfBirth.HasValue &&
            !string.IsNullOrWhiteSpace(Address) &&
            !string.IsNullOrWhiteSpace(Phone) &&
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(IdNumber) &&
            !string.IsNullOrWhiteSpace(Vocation) &&
            YearsOfService >= 0;

        public CreateProfessor(ProfessorDao professorDao)
        {
            InitializeComponent();
            this.professorDao = professorDao;
            DataContext = this;
        }

        private void PotvrdiButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Professor newProfessor = new Professor
                {
                    Surname = Surname,
                    Name = FirstName,
                    DateOfBirth = DateOnly.FromDateTime(DateOfBirth.Value),
                    Address = Address,
                    Phone = Phone,
                    Email = Email,
                    IdNumber = IdNumber,
                    Vocation = Vocation,
                    YearsOfService = YearsOfService
                };

                professorDao.Create(newProfessor);
                ClearFields();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding professor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OdustaniButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearFields()
        {
            Surname = string.Empty;
            FirstName = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            IdNumber = string.Empty;
            Vocation = string.Empty;
            YearsOfService = 0;
            DateOfBirth = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}