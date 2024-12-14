using StudentService.Model;
using System;
using System.ComponentModel;
using System.Windows;
using StudentService.DAO;
using System.Collections.Generic;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window, INotifyPropertyChanged
    {
        private readonly DepartmentDao departmentDao;
      
        public CreateDepartment(DepartmentDao departmentDao)
        {
            InitializeComponent();
            this.departmentDao = departmentDao;
            DataContext = this;
            
        }
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
        //public string Code { get; set; }
        private string code;
        public string Code
        {
            get => code;
            set
            {
                code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        // public string Name { get; set; }
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

        // public Professor HeadProfessor { get; set; }
        private Professor headProfessor;
        public Professor HeadProfessor
        {
           get => headProfessor;
            set
            {
               headProfessor = value;
               OnPropertyChanged(nameof(HeadProfessor));
            }
        }
        // public List<Professor> Professors { get; set; }
        private List<Professor> professors=new List<Professor>();
        public List<Professor> Professors
        {
           get => professors;
           set
           {
               professors = value;
               OnPropertyChanged(nameof(Professors));
           }
        }
        // Metod za dodavanje departmana sa unapređenom obradom grešaka
        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validacija unosa
                if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(Name) || HeadProfessor == null)
                {
                    MessageBox.Show("Please provide all the required fields: Code, Name, and Head Professor.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Department newDepartment = new Department
                {
                    Id = Id, // Generisanje ID-a u DAO klasi je bolje, ali ako želite da ga korisnik unosi, ostavite ovako
                    Code = Code,
                    Name = Name,
                    HeadProfessor = HeadProfessor,
                    Professors = Professors
                };

                var createdDepartment = departmentDao.Create(newDepartment);

                // Obaveštavanje korisnika o uspešnom kreiranju
                MessageBox.Show($"Department {createdDepartment.Name} created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Čišćenje polja
                ClearFields();
            }
            catch (Exception ex)
            {
                // Obrada greške sa detaljima
                MessageBox.Show($"Error adding department: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Čišćenje svih polja u formi
        private void ClearFields()
        {
            Id = 0;
            Code = string.Empty;
            Name = string.Empty;
            HeadProfessor = null; 
            Professors = new List<Professor>();
        }
        // Event za obaveštavanje o promenama na property-jima
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
