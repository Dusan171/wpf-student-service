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

using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewDepartment.xaml
    /// </summary>
    public partial class ViewDepartment : Window, IObserver
    {
        public ViewDepartment()
        {
            InitializeComponent();
            departmentDao = new DepartmentDao();
            Departments = new ObservableCollection<Department>();
            Update();
            //departmentDao.ProfessorSubject.Subscribe(this);
            DataContext = this;
        }

        public ObservableCollection<Department> Departments { get; set; }
        public Department SelectedDepartment { get; set; }
        private DepartmentDao departmentDao;

        public void Update()
        {
            Departments.Clear();
            var departmentList = departmentDao.GetAll();
            foreach (var department in departmentList)
            {
                Departments.Add(department);
            }
        }

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateDepartment createDepartment = new CreateDepartment(departmentDao);
            createDepartment.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Please select a department to update.");
                return;
            }
            UpdateDepartment updateDepartment = new UpdateDepartment(SelectedDepartment, departmentDao);
            updateDepartment.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Please select a department to delete.");
                return;
            }

            departmentDao.RemoveDepartment(SelectedDepartment.Id);
            Update();
        }


    }
}
