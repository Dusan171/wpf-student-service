
using System.Collections.Generic;
using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewGrade.xaml
    /// </summary>
    public partial class ViewGrade : Window, IObserver
    {
        //public int Id { get; set; }
        // public Student PassedStudent { get; set; }
        // public Subject Subject { get; set; }
        // public int Value { get; set; }
        // public DateOnly Date { get; set; }

        public ObservableCollection<Grade> Grades { get; set; }
        public Grade SelectedGrade { get; set; }
        private GradeDao gradeDao;

        public ViewGrade()
        {
            InitializeComponent();
            this.gradeDao = gradeDao;
            Grades = new ObservableCollection<Grade>();
            Update();
            gradeDao.StudentGrade.Subscribe(this);
            DataContext = this;
        }

        public void Update()
        {
            Grades.Clear();
            var gradeList = gradeDao.GetAll();
            foreach (var grade in gradeList)
            {
                Grades.Add(grade);
            }
        }

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateGrade createGrade = new CreateGrade(gradeDao);
            createGrade.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                MessageBox.Show("Please select a grade to update.");
                return;
            }
            UpdateGrade updateGrade = new UpdateGrade(SelectedGrade, gradeDao);
            updateGrade.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null)
            {
                MessageBox.Show("Please select a grade to delete.");
                return;
            }

            try
            {
                // Attempt to remove the grade
                gradeDao.RemoveGrade(SelectedGrade.Id);
                Update(); // Refresh the grade list after deletion
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting grade: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
