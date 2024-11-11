//using StudentService.Model;
using System;
using System.Collections.Generic;
//using System.ComponentModel;
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
using System.Xml.Linq;

using StudentService.Model.Enums;
using StudentService.Model;
using System.ComponentModel;
using StudentService.DAO;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.RightsManagement;

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

        /*
         *  public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Professor HeadProfessor { get; set; }
        public List<Professor> Professors { get; set; }

         */
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
           // DataContext = this; //crveno
        }
        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Student instance directly from properties
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
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
    }
}
