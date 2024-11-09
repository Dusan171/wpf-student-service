using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewProfessors.xaml
    /// </summary>
    public partial class ViewProfessors : Window, IObserver
    {
        public ObservableCollection<Professor> Professors { get; set; }
        public Professor SelectedProfessor { get; set; }
        private ProfessorDao professorDao;

        public ViewProfessors()
        {
            InitializeComponent();
            professorDao = new ProfessorDao();
            Professors = new ObservableCollection<Professor>();
            Update();
            professorDao.ProfessorSubject.Subscribe(this);
            DataContext = this;
        }

        public void Update()
        {
            Professors.Clear();
            var professorList = professorDao.GetAll();
            foreach (var professor in professorList)
            {
                Professors.Add(professor);
            }
        }

        private void Button_ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateProfessor createProfessor = new CreateProfessor(professorDao);
            createProfessor.Show();
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor == null)
            {
                MessageBox.Show("Please select a professor to update.");
                return;
            }
            UpdateProfessor updateProfessor = new UpdateProfessor(SelectedProfessor, professorDao);
            updateProfessor.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor == null)
            {
                MessageBox.Show("Please select a professor to delete.");
                return;
            }

            professorDao.RemoveProfessor(SelectedProfessor.Id);
            Update();
        }
    }
}
