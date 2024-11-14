using StudentService.Model;
using System;
using System.ComponentModel;
using System.Windows;
using StudentService.DAO;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window, INotifyPropertyChanged
    {
        private DepartmentDao departmentDao;

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
            get=> name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
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
        private void ClearFields()
        {
            Name = string.Empty;
            Id = 0;
            Code = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CreateDepartment(DepartmentDao departmentDao)
        {
            InitializeComponent();
            this.departmentDao = departmentDao;
            //DataContext = this; //crveno
        }
        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Department instance directly from properties
                Department newDepartment = new Department
                {
                    Id =Id,
                    Code=Code,
                    Name=Name,
                };

                departmentDao.Create(newDepartment);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding department: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
    }
}
