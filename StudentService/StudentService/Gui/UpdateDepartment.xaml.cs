//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

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
                departmentDao.UpdateDepartment(currentDepartment);
                MessageBox.Show("Department updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
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
