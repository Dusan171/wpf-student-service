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
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
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

        private string idNumber;
        public string IdNumber
        {
            get => idNumber;
            set
            {
                idNumber = value;
                OnPropertyChanged(nameof(IdNumber));
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
            }
        }

        public CreateProfessor(ProfessorDao professorDao)
        {
            InitializeComponent();
            this.professorDao = professorDao;
            DataContext = this;
        }

        private void AddProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Professor newProfessor = new Professor
                {
                    Surname = Surname,
                    Name = Name,
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding professor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            Surname = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            IdNumber = string.Empty;
            Vocation = string.Empty;
            YearsOfService = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
