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
            departmentDao.DepartmentSubject.Subscribe(this);
            Update();
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
            var createDepartment = new CreateDepartment(departmentDao);
            createDepartment.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Please select a department to update.");
                return;
            }
            var updateDepartment = new UpdateDepartment(SelectedDepartment, departmentDao);
            updateDepartment.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Please select a department to delete.");
                return;
            }

            var confirmationResult = MessageBox.Show("Are you sure you want to delete this department?",
                                                       "Delete Confirmation",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

            if (confirmationResult == MessageBoxResult.Yes)
            {
                departmentDao.RemoveDepartment(SelectedDepartment.Id);
                Update();  // Refresh department list
            }
            
        }
    }
}
