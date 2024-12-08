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

using StudentService.Model;
using StudentService.DAO;
using System;
using System.ComponentModel;
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
        private Subject currentSubject;

        // ublic int Id { get; set; }
        public int Id
        {
            get => currentSubject.Id;
            set
            {
                currentSubject.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //public string Code { get; set; }
        public string Code
        {
            get => currentSubject.Code;
            set
            {
                currentSubject.Code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        //public string Name { get; set; }
        public string Name
        {
            get => currentSubject.Name;
            set
            {
                currentSubject.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        // public Semester Semester { get; set; }
        public Semester Semester
        {
            get => currentSubject.Semester;
            set
            {
                currentSubject.Semester = value;
                OnPropertyChanged(nameof(Semester));
            }
        }
        // public int YearOfStudy { get; set; }
        public int YearOfStudy
        {
            get => currentSubject.YearOfStudy;
            set
            {
                currentSubject.YearOfStudy = value;
                OnPropertyChanged(nameof(YearOfStudy));
            }
        }
        // public Professor Professor { get; set; }
        public Professor Professor
        {
            get => currentSubject.Professor;
            set 
            {
                currentSubject.Professor = value;
                OnPropertyChanged(nameof(Professor));
            }
        }
        // public int Espb { get; set; }
        public int Espb
        {
            get => currentSubject.Espb;
            set
            {
                currentSubject.Espb = value;
                OnPropertyChanged(nameof(Espb));
            }
        }
        // public List<Student> PassedStudents { get; set; }
        public List<Student> PassedStudents
        {
            get => currentSubject.PassedStudents;
            set 
            {
                currentSubject.PassedStudents = value;
                OnPropertyChanged(nameof(PassedStudents));
            }
        }
        // public List<Student> AttendingStudents { get; set; }
        public List<Student> AttendingStudents
        {
            get => currentSubject.AttendingStudents;
            set 
            {
                currentSubject.AttendingStudents = value;
                OnPropertyChanged(nameof(AttendingStudents));
            }
        }

        public UpdateSubject(Subject subject, SubjectDao subjectDao)
        {
            InitializeComponent();
            this.subjectDao = subjectDao;
            this.currentSubject = subject;
            DataContext = this;
        }

        private void UpdateSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                subjectDao.UpdateSubject(currentSubject);
                MessageBox.Show("Subject updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subject: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
