using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using StudentService.DAO;
using StudentService.Gui;
using StudentService.Model;
using System.Linq;
using System.Windows.Input;

namespace StudentService
{
    public partial class MainWindow : Window, StudentService.Observer.IObserver
    {
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Professor> Professors { get; set; }
        public ObservableCollection<Subject> Subjects { get; set; }

        private StudentDao studentDao;
        private ProfessorDao professorDao;
        private SubjectDao subjectDao;
        private GradeDao gradeDao;

        public Student SelectedStudent { get; set; }

        private DispatcherTimer _timer;

        public Professor SelectedProfessor { get; set; }
        public Subject SelectedSubject { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            studentDao = new StudentDao();
            professorDao = new ProfessorDao();
            subjectDao = new SubjectDao();

            Students = new ObservableCollection<Student>();
            Professors = new ObservableCollection<Professor>();
            Subjects = new ObservableCollection<Subject>();

            UpdateStudents();
            UpdateProfessors();
            UpdateSubjects();

            studentDao.StudentSubject.Subscribe(this);
            professorDao.ProfessorSubject.Subscribe(this);
            subjectDao.SubjectSubject.Subscribe(this);

            DataContext = this;

            InitializeShortcuts();
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

      /*  public void Update()
=======
            //Initialize keyboard shortcuts
            InitializeShortcuts();
        }*/
        private void InitializeShortcuts()
        {
            var addCommand = new RoutedCommand();
            addCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(addCommand, AddButton_Click));

            var updateCommand = new RoutedCommand();
            updateCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(updateCommand, Button_ClickUpdate));

            var deleteCommand = new RoutedCommand();
            deleteCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(deleteCommand, Button_ClickDelete));

            var searchCommand = new RoutedCommand();
            searchCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(searchCommand, SearchButton_Click));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainTab.SelectedIndex == 0) // Students tab
            {
                var createStudentWindow = new CreateStudent(studentDao);
                createStudentWindow.Owner = this;
                createStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                createStudentWindow.ShowDialog();
                UpdateStudents();
            }
            else if (mainTab.SelectedIndex == 1) // Professors tab
            {
                var createProfessorWindow = new CreateProfessor(professorDao);
                createProfessorWindow.Owner = this;
                createProfessorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                createProfessorWindow.ShowDialog();
                UpdateProfessors();
            }
            else if (mainTab.SelectedIndex == 2) // Subjects tab
            {
                var createSubjectWindow = new CreateSubject(subjectDao, professorDao);
                createSubjectWindow.Owner = this;
                createSubjectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                createSubjectWindow.ShowDialog();
                UpdateSubjects();
            }
        }

        private void UpdateStudents()
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

        private void UpdateProfessors()
        {
            Professors.Clear();
            var professorList = professorDao.GetAll();
            foreach (var professor in professorList)
            {
                Professors.Add(professor);
            }
        }

        private void UpdateSubjects()
        {
            Subjects.Clear();
            var subjectList = subjectDao.GetAll();
            foreach (var subject in subjectList)
            {
                Subjects.Add(subject);
            }
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (mainTab.SelectedIndex == 0 && SelectedStudent != null) // Students tab
            {
                var updateStudentWindow = new UpdateStudent(SelectedStudent, studentDao);
                updateStudentWindow.Owner = this;
                updateStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                updateStudentWindow.ShowDialog();
                UpdateStudents();
            }
            else if (mainTab.SelectedIndex == 1 && SelectedProfessor != null) // Professors tab
            {
                var updateProfessorWindow = new UpdateProfessor(SelectedProfessor, professorDao);
                updateProfessorWindow.Owner = this;
                updateProfessorWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                updateProfessorWindow.ShowDialog();
                UpdateProfessors();
            }
            else if (mainTab.SelectedIndex == 2 && SelectedSubject != null) // Subjects tab
            {
                var updateSubjectWindow = new UpdateSubject(SelectedSubject, subjectDao, professorDao);
                updateSubjectWindow.Owner = this;
                updateSubjectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                updateSubjectWindow.ShowDialog();
                UpdateSubjects();
            }
            //UpdateStudent updateStudent = new UpdateStudent(SelectedStudent, studentDao);
            //updateStudent.Show();
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (mainTab.SelectedIndex == 0 && SelectedStudent != null) // Students tab
            {
                var result = MessageBox.Show($"Are you sure you want to delete the student {SelectedStudent.Name} {SelectedStudent.Surname}?",
                    "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    studentDao.RemoveStudent(SelectedStudent.Id);
                    UpdateStudents();
                }
            }
            else if (mainTab.SelectedIndex == 1 && SelectedProfessor != null) // Professors tab
            {
                var result = MessageBox.Show($"Are you sure you want to delete the professor {SelectedProfessor.Name} {SelectedProfessor.Surname}?",
                    "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    professorDao.RemoveProfessor(SelectedProfessor.Id);
                    UpdateProfessors();
                }
            }
            else if (mainTab.SelectedIndex == 2 && SelectedSubject != null) // Subjects tab
            {
                var result = MessageBox.Show($"Are you sure you want to delete the subject {SelectedSubject.Name}?",
                    "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    subjectDao.RemoveSubject(SelectedSubject.Id);
                    UpdateSubjects();
                }
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainTab.SelectedIndex == 0) // Students tab
            {
                var filteredStudents = studentDao.GetAll().Where(s => s.Name.Contains(SearchBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                Students.Clear();
                foreach (var student in filteredStudents)
                {
                    Students.Add(student);
                }
            }
            else if (mainTab.SelectedIndex == 1) // Professors tab
            {
                var filteredProfessors = professorDao.GetAll().Where(p => p.Name.Contains(SearchBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                Professors.Clear();
                foreach (var professor in filteredProfessors)
                {
                    Professors.Add(professor);
                }
            }
            else if (mainTab.SelectedIndex == 2) // Subjects tab
            {
                var filteredSubjects = subjectDao.GetAll().Where(s => s.Name.Contains(SearchBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                Subjects.Clear();
                foreach (var subject in filteredSubjects)
                {
                    Subjects.Add(subject);
                }
            }
        }
        public void Update()
        {
            UpdateStudents();
            UpdateProfessors();
            UpdateSubjects();
        }
    }
}
