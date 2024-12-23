using StudentService.DAO;
using StudentService.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace StudentService.Gui
{
    public partial class UpdateDepartment : Window, INotifyPropertyChanged
    {
        private readonly DepartmentDao departmentDao;
        private Department currentDepartment;

        public string Code
        {
            get => currentDepartment.Code;
            set
            {
                currentDepartment.Code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        public string Name
        {
            get => currentDepartment.Name;
            set
            {
                currentDepartment.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Professor HeadProfessor
        {
            get => currentDepartment.HeadProfessor;
            set
            {
                currentDepartment.HeadProfessor = value;
                OnPropertyChanged(nameof(HeadProfessor));
            }
        }

        public ObservableCollection<Professor> Professors
        {
            get => new ObservableCollection<Professor>(currentDepartment.Professors);
            set
            {
                currentDepartment.Professors = new List<Professor>(value);
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
                var updatedDepartment = departmentDao.UpdateDepartment(currentDepartment);
                if (updatedDepartment != null)
                {
                    MessageBox.Show("Department updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Error: Department not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating department: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
