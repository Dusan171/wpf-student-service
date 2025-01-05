using StudentService.Model;
using StudentService.DAO;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using StudentService.Model.Enums;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateSubject.xaml
    /// </summary>
    public partial class UpdateSubject : Window, INotifyPropertyChanged
    {
        private readonly SubjectDao subjectDao;
        private readonly ProfessorDao professorDao;
        private Subject currentSubject;

        public ObservableCollection<Semester> AvailableSemesters { get; set; }
        public ObservableCollection<Professor> AvailableProfessors { get; set; }
        public Semester SemesterStud
        {
            get => currentSubject.Semester;
            set
            {
                currentSubject.Semester = value;
                OnPropertyChanged(nameof(SemesterStud));
            }
        }
        public ObservableCollection<Professor> Professors { get; } = new ObservableCollection<Professor>();

        public int Id
        {
            get => currentSubject.Id;
            set
            {
                currentSubject.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Code
        {
            get => currentSubject.Code;
            set
            {
                currentSubject.Code = value;
                OnPropertyChanged(nameof(Code));
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public string NameSubj
        {
            get => currentSubject.Name;
            set
            {
                currentSubject.Name = value;
                OnPropertyChanged(nameof(NameSubj));
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public int YearOfStudy
        {
            get => currentSubject.YearOfStudy;
            set
            {
                currentSubject.YearOfStudy = value;
                OnPropertyChanged(nameof(YearOfStudy));
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public Professor Professor
        {
            get => currentSubject.Professor;
            set
            {
                currentSubject.Professor = value;
                OnPropertyChanged(nameof(Professor));
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public int Espb
        {
            get => currentSubject.Espb;
            set
            {
                currentSubject.Espb = value;
                OnPropertyChanged(nameof(Espb));
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(Code) &&
            !string.IsNullOrWhiteSpace(NameSubj) &&
            YearOfStudy > 0 &&
            Espb > 0 &&
            Professor != null;

        public UpdateSubject(Subject subject, SubjectDao subjectDao, ProfessorDao professorDao)
        {
            InitializeComponent();
            this.subjectDao = subjectDao;
            this.professorDao = professorDao;
            this.currentSubject = subject;
            AvailableSemesters = new ObservableCollection<Semester>(Enum.GetValues(typeof(Semester)).Cast<Semester>());
            AvailableProfessors = new ObservableCollection<Professor>(professorDao.GetAll());
            // LoadProfessors();

            DataContext = this;
        }

        /* private void LoadProfessors()
         {
             var professors = professorDao.GetAll();
             Professors.Clear();

             foreach (var professor in professors)
             {
                 Professors.Add(professor);
             }
         }*/

        private void UpdateSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsValid)
                {
                    MessageBox.Show("All fields must be valid before updating the subject.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                subjectDao.UpdateSubject(currentSubject);

                MessageBox.Show("Subject updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subject: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
