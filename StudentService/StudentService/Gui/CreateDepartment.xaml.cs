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
        private List<Professor> professors;
        public List<Professor> Professors
        {
            get => professors;
           set
           {
               professors = value;
               OnPropertyChanged(nameof(Professors));
           }
        }

        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Department newDepartment = new Department
                {
                  Id = Id, 
                  Code = Code,
                  Name = Name,
                  HeadProfessor = HeadProfessor,
                  Professors = Professors
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
            Id = 0;
            Code = string.Empty;
            Name = string.Empty;
            // public List<Professor> Professors { get; set; }

            // public Professor HeadProfessor { get; set; }
           // HeadProfessor = null; 
            //Professors = empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
