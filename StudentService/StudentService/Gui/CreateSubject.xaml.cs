///using System;
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
using StudentService.Model.Enums;
using System.Collections.ObjectModel;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateSubject.xaml
    /// </summary>
    public partial class CreateSubject : Window, INotifyPropertyChanged
    {
        private SubjectDao subjectDao;

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
        //public string Name { get; set; }
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
        //public Semester Semester { get; set; }
        private Semester semester;
        public Semester SemesterStud
        {
            get => semester;
            set
                {
                semester = value;
                OnPropertyChanged(nameof(SemesterStud));
                }
        }
        //public int YearOfStudy { get; set; }
        private int yearOfStudy;
        public int YearOfStudy
        {
            get => yearOfStudy;
            set
            {
                yearOfStudy = value;
                OnPropertyChanged(nameof(YearOfStudy));
            }
        }
        //public Professor Professor { get; set; }
        private Professor professor;

        public Professor Professor
        {
            get => professor;
            set
            {
                professor = value;
                OnPropertyChanged(nameof(Professor));
            }
        }

        //public int Espb { get; set; }
        private int espb;
        public int Espb
        {
            get => espb;
            set
            {
                espb = value;
                OnPropertyChanged(nameof(Espb));
            }
        }
        //public List<Student> PassedStudents { get; set; }
        private List<Student> passedStudents;
        public List<Student> PassedStudents
        {
            get => passedStudents;
            set
            {
                passedStudents = value;
                OnPropertyChanged(nameof(PassedStudents));
            }
        }

        // public List<Student> AttendingStudents { get; set; }
        private List<Student> attendingStudents;
        public List<Student> AttendingStudents
        { 
             get=> attendingStudents;
            set
            {
                attendingStudents = value;
                OnPropertyChanged(nameof(AttendingStudents));
            }
        }
        public ObservableCollection<Semester> StudentSemester { get; set; }
        public CreateSubject(SubjectDao subjectDao)
        {
            InitializeComponent();
            this.subjectDao = subjectDao;
            DataContext = this;

            StudentSemester = new ObservableCollection<Semester>();
            StudentSemester.Add(Semester.SUMMER);
            StudentSemester.Add(Semester.WINTER);
        }

        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Subject newSubject = new Subject
                {
                     Id = Id,
                     Code = Code,
                     Name = Name,
                     Semester = SemesterStud,
                     YearOfStudy = YearOfStudy,
       // public Professor Professor { get; set; }
                     Professor = Professor,
                     Espb = Espb,
        //public List<Student> PassedStudents { get; set; }
                     PassedStudents = PassedStudents,
        //public List<Student> AttendingStudents { get; set; }
                     AttendingStudents = AttendingStudents
                };

                subjectDao.Create(newSubject);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding subject: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            Id = 0;
            Code = string.Empty;
            Name = string.Empty;
            Espb = 0;
            YearOfStudy = 0;
            SemesterStud = default;
          
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
