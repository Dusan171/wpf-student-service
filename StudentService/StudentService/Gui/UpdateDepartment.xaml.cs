using StudentService.DAO;
using StudentService.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using StudentService.Model.Enums;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.RightsManagement;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateDepartment.xaml
    /// </summary>
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
           // DataContext = this; //crveno
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
