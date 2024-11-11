using System;
using System.Collections.Generic;
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

using StudentService.Model.Enums;
using StudentService.Model;
using System.ComponentModel;
using StudentService.DAO;
using System.Collections.ObjectModel;
using System.Net;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateSubject.xaml
    /// </summary>
    public partial class CreateSubject : Window, INotifyPropertyChanged

    {
        private SubjectDao subjectDao;

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

        private int espb;
        public int ESPB
        {
            get => espb;
            set
            {
                espb = value;
                OnPropertyChanged(nameof(ESPB));
            }
        }
        private int yearofstudy;
        public int YearOfStudy
        {
            get => yearofstudy;
            set
            {
                espb = value;
                OnPropertyChanged(nameof(YearOfStudy));
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
        
        private Semester semester;
        public Semester Semester
        {
            get => semester;
            set
            {
                semester = value;
                OnPropertyChanged(nameof(Semester));
            }
        }
        // public Professor Professor { get; set; }
        private Professor? professor;
        public Professor Professor
        {
            get => professor;
            set
            {
                professor = value;
                OnPropertyChanged(nameof(Professor));
            }
        }
        public ObservableCollection<Semester> Semesters { get; set; }
        public CreateSubject(SubjectDao subjectDao)
        {
            InitializeComponent();
            this.subjectDao = subjectDao;
           // DataContext = this; //??CRVENO

            Semesters = new ObservableCollection<Semester>();
            Semesters.Add(Semester.SUMMER);
            Semesters.Add(Semester.WINTER);
         
        }
        
        private void ClearFields()
        {
            Name = string.Empty;
            Id = 0;
            Code = string.Empty;
            ESPB = 0;
            YearOfStudy = 0;
            //semester, je l treba?
            //professor, je l treba?
        }
        /*da pratim sta ima
         *   public int Id { get; set;}
        public string Code { get; set; }
        public string Name { get; set; }
        public Semester Semester { get; set; }
        public int YearOfStudy { get; set; }
        public Professor Professor { get; set; }
        public int Espb { get; set; }

        public List<Student> PassedStudents { get; set; }
        public List<Student> AttendingStudents { get; set; }

         */
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //samo se ovo mijenja u view i update
        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Subject instance directly from properties
                Subject newSubject = new Subject
                {
                    Name = Name,
                    Id = Id,
                    YearOfStudy = YearOfStudy,
                    Code = Code,
                    Semester = Semester,
                    Professor = Professor,
                    Espb = espb
                };

                subjectDao.Create(newSubject);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding subject: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
