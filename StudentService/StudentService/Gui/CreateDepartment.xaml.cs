using StudentService.Model;
using StudentService.DAO;
using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace StudentService.Gui
{
    public partial class CreateDepartment : Window, INotifyPropertyChanged
    {
        private DepartmentDao departmentDao;

        // Properties with INotifyPropertyChanged implementation
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

        private int headProfessorId;
        public int HeadProfessorId
        {
            get => headProfessorId;
            set
            {
                headProfessorId = value;
                OnPropertyChanged(nameof(HeadProfessorId));
            }
        }

        private string professorsIds;
        public string ProfessorsIds
        {
            get => professorsIds;
            set
            {
                professorsIds = value;
                OnPropertyChanged(nameof(ProfessorsIds));
            }
        }

        public CreateDepartment(DepartmentDao departmentDao)
        {
            InitializeComponent();
            this.departmentDao = departmentDao;
            DataContext = this;
        }

        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse professors IDs from input text (separated by commas)
                List<Professor> professors = ProfessorsIds.Split(',')
                    .Select(id => new Professor { Id = int.Parse(id.Trim()) }).ToList();

                // Create a new Department instance directly from properties
                Department newDepartment = new Department
                {
                    Code = Code,
                    Name = Name,
                    HeadProfessor = new Professor { Id = HeadProfessorId },
                    Professors = professors
                };

                departmentDao.Create(newDepartment);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding department: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            Code = string.Empty;
            Name = string.Empty;
            HeadProfessorId = 0;
            ProfessorsIds = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
