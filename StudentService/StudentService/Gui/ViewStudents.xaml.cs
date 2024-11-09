using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewStudents.xaml
    /// </summary>
    public partial class ViewStudents : Window, IObserver
    {
        public ObservableCollection<Student> Students { get; set; } 
        public Student SelectedStudent { get; set; }
        private StudentDao studentDao;
        public ViewStudents()
        {
            InitializeComponent();
            studentDao = new StudentDao();
            Students = new ObservableCollection<Student>(); 
            Update();
            studentDao.StudentSubject.Subscribe(this);
            DataContext = this; 
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

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateStudent createStudent = new CreateStudent(studentDao);
            createStudent.Show();

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
                return;
            }

            studentDao.RemoveStudent(SelectedStudent.Id);
            Update();

        }
    }
}
