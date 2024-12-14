
using StudentService.Model;
using StudentService.DAO;
using System;
using System.ComponentModel;
using System.Windows;


namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateDepartment.xaml
    /// </summary>
    public partial class UpdateDepartment : Window, INotifyPropertyChanged
    {
        private readonly DepartmentDao departmentDao;
        private Department currentDepartment;

        //public int Id { get; set; }
        public int Id
        {
            get => currentDepartment.Id;
            set
            {
                currentDepartment.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //public string Code { get; set; }
        public string Code
        {
            get => currentDepartment.Code;
            set
            {
                currentDepartment.Code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        // public string Name { get; set; }
        public string Name
        {
            get => currentDepartment.Name;
            set
            {
                currentDepartment.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        //  public Professor HeadProfessor { get; set; }
        public Professor HeadProfessor
        {
            get => currentDepartment.HeadProfessor;
            set
            {
                currentDepartment.HeadProfessor = value;
                OnPropertyChanged(nameof(HeadProfessor));
            }
        }
        // public List<Professor> Professors { get; set; }
        public List<Professor> Professors
        {
            get=>currentDepartment.Professors;
            set
            { 
              currentDepartment.Professors = value;
              OnPropertyChanged(nameof(Professors));
            }
        }
        public UpdateDepartment(Department department, DepartmentDao departmentDao)
        {
            InitializeComponent();
            this.departmentDao = departmentDao;
            this.currentDepartment = department;
            DataContext = this;
        }

        private void UpdateDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    departmentDao.UpdateDepartment(currentDepartment);
                    MessageBox.Show("Department updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating department: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ValidateInput()
        {
            // Add validation for required fields
            return !string.IsNullOrWhiteSpace(Code) &&
                   !string.IsNullOrWhiteSpace(Name) &&
                   HeadProfessor != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
