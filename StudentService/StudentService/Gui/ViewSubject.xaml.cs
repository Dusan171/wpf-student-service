
using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewSubject.xaml
    /// </summary>
    public partial class ViewSubject : Window, IObserver
    {
        public ObservableCollection<Subject> Subjects { get; set; }
        public Subject SelectedSubject { get; set; }
        private SubjectDao subjectDao;

        public ViewSubject()
        {
            InitializeComponent();
            subjectDao = new SubjectDao();
            Subjects = new ObservableCollection<Subject>();
            Update();
            subjectDao.SubjectSubject.Subscribe(this);
            DataContext = this;
        }

        public void Update()
        {
            Subjects.Clear();
            var subjectList = subjectDao.GetAll();
            foreach (var subject in subjectList)
            {
                Subjects.Add(subject);
            }
        }

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateSubject createSubject = new CreateSubject(subjectDao);
            createSubject.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedSubject == null)
            {
                MessageBox.Show("Please select a subject to update.");
                return;
            }
            // Make sure to handle errors gracefully in the update process
            try
            {
                UpdateSubject updateSubject = new UpdateSubject(SelectedSubject, subjectDao);
                updateSubject.ShowDialog(); // Changed to ShowDialog
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening update form: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedSubject == null)
            {
                MessageBox.Show("Please select a subject to delete.");
                return;
            }

            // Confirm deletion with the user
            var result = MessageBox.Show("Are you sure you want to delete this subject?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    subjectDao.RemoveSubject(SelectedSubject.Id);
                    Update(); // Refresh the list after deletion
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting subject: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
