using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            studentDao = new StudentDao();
            Students = new ObservableCollection<Student>();
            Update();
            studentDao.StudentSubject.Subscribe(this);
            DataContext = this;

            InitializeDateTime();
        }

        private void InitializeDateTime()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (sender, e) =>
            {
                // Ažurira TextBlock u statusnoj traci
                DateTimeTextBlock.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            };
            _timer.Start();
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

        private void MainTab_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Get the selected tab header and update the status bar
            var selectedTab = mainTab.SelectedItem as System.Windows.Controls.TabItem;
            if (selectedTab != null)
            {
                CurrentTabTextBlock.Text = selectedTab.Header.ToString();
            }
        }

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
