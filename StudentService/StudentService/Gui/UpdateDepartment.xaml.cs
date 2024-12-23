
using StudentService.Model;
using StudentService.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

using StudentService.Model.Enums;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.RightsManagement;

namespace StudentService.Gui
{
    public partial class UpdateDepartment : Window, INotifyPropertyChanged
    {
        private readonly DepartmentDao departmentDao;
        private Department currentDepartment;
        public int Id
        {
            get => currentDepartment.Id;
            set
            {
                currentDepartment.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
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
  
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public UpdateDepartment(Department department,DepartmentDao departmentDao)
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
                departmentDao.UpdateDepartment(currentDepartment);
                MessageBox.Show("Department updating successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //Close(); //crveno
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating department: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
