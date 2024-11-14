using StudentService.DAO;
using StudentService.Model;
using StudentService.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for ViewSubjectt.xaml
    /// </summary>
    public partial class ViewSubjectt : Window, IObserver
    {
        public ObservableCollection<Subject> Subjects { get; set; }
        public Subject SelectedSubject { get; set; }
        private SubjectDao subjectDao;
        public ViewSubjectt()
        {
            InitializeComponent();
            subjectDao = new SubjectDao();
            Subjects = new ObservableCollection<Subject>();
            Update();
            subjectDao.SubjectSubject.Subscribe(this);
            //DataContext = this; //crveno
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
            CreateSubject createSubjectt = new CreateSubject(subjectDao);
           //createSubjectt.Show();//crveno
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if (SelectedSubject == null)
            {
                MessageBox.Show("Please select a professor to update.");
                return;
            }
            UpdateDAOSubjectt updateSubject = new UpdateDAOSubjectt(SelectedSubject, subjectDao);
            updateSubject.Show();

        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedSubject == null)
            {
                MessageBox.Show("Please select a professor to delete.");
                return;
            }

            subjectDao.RemoveSubject(SelectedSubject.Id);
            Update();
        }
    }
}
