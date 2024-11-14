using StudentService.DAO;
using StudentService.Model.Enums;
using StudentService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Threading.Channels;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateSubjectt.xaml
    /// </summary>
    public partial class UpdateDAOSubjectt : Window, INotifyPropertyChanged
    {
        private readonly SubjectDao subjectDao;
        private Subject currentSubject;
        public string Name
        {
            get => currentSubject.Name;
            set
            {
                currentSubject.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Id
        {
            get => currentSubject.Id;
            set
            {
                currentSubject.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int ESPB
        {
            get => currentSubject.Espb;
            set
            {
                currentSubject.Espb = value;
                OnPropertyChanged(nameof(ESPB));
            }
        }
        public int YearOfStudy
        {
            get => currentSubject.YearOfStudy;
            set
            {
                currentSubject.YearOfStudy = value;
                OnPropertyChanged(nameof(YearOfStudy));
            }
        }
        public string Code
        {
            get => currentSubject.Code;
            set
            {
                currentSubject.Code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        public Semester Semester
        {
            get => currentSubject.Semester;
            set
            {
                currentSubject.Semester = value;
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
        public UpdateDAOSubjectt(Subject subject,SubjectDao subjectDao)
        {
            //InitializeComponent();//CRVENO
            this.subjectDao = subjectDao;
            this.currentSubject = subject;
            DataContext = this; 

            Semesters = new ObservableCollection<Semester>();
            Semesters.Add(Semester.SUMMER);
            Semesters.Add(Semester.WINTER);

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
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
    }
}
