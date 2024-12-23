using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using StudentService.DAO;
using StudentService.Gui;
using StudentService.Model;

namespace StudentService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, StudentService.Observer.IObserver
    {
        public ObservableCollection<Student> Students { get; set; }

        private StudentDao studentDao;
        private GradeDao gradeDao;

        public Student SelectedStudent { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            studentDao = new StudentDao();
            Students = new ObservableCollection<Student>();
            Update();
            studentDao.StudentSubject.Subscribe(this);
            DataContext = this;
        }


        //ViewStudents viewStudents = new ViewStudents();
        //viewStudents.Show();

        //ViewProfessors viewProfessors = new ViewProfessors();
        //viewProfessors.Show();

        //ViewSubject viewSubject = new ViewSubject();
        //viewSubject.Show();

        // ViewIndex viewIndex = new ViewIndex();
        //viewIndex.Show();

        // ViewAdress viewAdress = new ViewAdress();
        // viewAdress.Show();

        // ViewDepartment viewDepartment = new ViewDepartment();
        // viewDepartment.Show();

        //ViewGrade viewGrade = new ViewGrade();
        // viewGrade.Show();
        private void ProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            ViewProfessors viewProfessors = new ViewProfessors();
            viewProfessors.Show();
        }

        

        private void SubjectButton_Click(object sender, RoutedEventArgs e)
        {
            ViewSubject viewSubject = new ViewSubject();
            viewSubject.Show();
        }


     

        public void Update()
        {
            Students.Clear();
            var studentList = studentDao.GetAll();
            foreach (var student in studentList)
            {
                Students.Add(student);
            }
        }

      

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedStudent == null)
            {
                return;
            }
            UpdateStudent updateStudent = new UpdateStudent(SelectedStudent, studentDao);
            updateStudent.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Please select a student to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirmation dialog
            var result = MessageBox.Show($"Are you sure you want to delete the student {SelectedStudent.Name} {SelectedStudent.Surname}?",
                                          "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                
                    studentDao.RemoveStudent(SelectedStudent.Id); // Assuming RemoveStudent takes the student ID
                    Update(); // Refresh the UI or data grid
                    
               
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainTab.SelectedIndex == 0) // Assuming tab 0 is for creating students
            {
                CreateStudent createStudent = new CreateStudent(studentDao);
                createStudent.Show();
            }
            else if (mainTab.SelectedIndex == 1) // Assuming tab 1 is for managing existing students
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show("Please select a student.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Determine which button was clicked
                Button clickedButton = sender as Button;
                if (clickedButton?.Name == "UpdateButton") // Update Button Logic
                {
                    UpdateStudent updateStudent = new UpdateStudent(SelectedStudent, studentDao);
                    updateStudent.Show();
                }
                else if (clickedButton?.Name == "DeleteButton") // Delete Button Logic
                {
                    var result = MessageBox.Show($"Are you sure you want to delete the student {SelectedStudent.Name} {SelectedStudent.Surname}?",
                                                  "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                      
                            studentDao.RemoveStudent(SelectedStudent.Id); // Assuming RemoveStudent takes the student ID
                            Update(); // Refresh the UI or data grid
                           
                    }
                }
            }
        }


    }
}
