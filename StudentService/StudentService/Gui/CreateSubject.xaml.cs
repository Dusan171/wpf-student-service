using StudentService.Model;
using System;
using System.ComponentModel;
using System.Windows;
using StudentService.DAO;
using StudentService.Model.Enums;
using System.Collections.ObjectModel;

namespace StudentService.Gui
{
    public partial class CreateSubject : Window, INotifyPropertyChanged
    {
        private SubjectDao subjectDao;
        private ProfessorDao professorDao;
     
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

        private string name;
        public string NameSubj
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(NameSubj));
            }
        }

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

        private Professor selectedProfessor;
        public Professor SelectedProfessor
        {
            get => selectedProfessor;
            set
            {
                selectedProfessor = value;
                OnPropertyChanged(nameof(SelectedProfessor));
            }
        }

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

        public ObservableCollection<Professor> Professors { get; set; }
        public ObservableCollection<Semester> AvailableSemesters { get; set; }

        public CreateSubject(SubjectDao subjectDao, ProfessorDao professorDao)
        {
            InitializeComponent();
            this.subjectDao = subjectDao;
            this.professorDao = professorDao;

            // Load available professors
            Professors = new ObservableCollection<Professor>(professorDao.GetAll());

            // Load available semesters
            AvailableSemesters = new ObservableCollection<Semester>
            {
                Semester.SUMMER,
                Semester.WINTER
            };
            DataContext = this;
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != nameof(AreFieldsValid))
                {
                    OnPropertyChanged(nameof(AreFieldsValid));
                }
            };
        }
        public bool AreFieldsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Code) &&
                       !string.IsNullOrEmpty(NameSubj) &&
                       SelectedProfessor != null &&
                       SemesterStud == Semester.WINTER || SemesterStud == Semester.SUMMER &&
                       YearOfStudy > 0 &&
                       Espb > 0;
            }
        }
        private void ClearFields()
        {
            Id = 0;
            Code = string.Empty;
            NameSubj = string.Empty;
            Espb = 0;
            YearOfStudy = 0;
            SemesterStud = default(Semester);
            SelectedProfessor = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void PotvrdiButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Subject newSubject = new Subject
                {
                    Id = Id,
                    Code = Code,
                    Name = NameSubj,
                    Semester = SemesterStud,
                    YearOfStudy = YearOfStudy,
                    Professor = SelectedProfessor,
                    Espb = Espb
                };

                subjectDao.Create(newSubject);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding subject: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OdustaniButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}