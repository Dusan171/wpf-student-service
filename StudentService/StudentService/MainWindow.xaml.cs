using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentService.Gui;

namespace StudentService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            }

            //ViewStudents viewStudents = new ViewStudents();
            //viewStudents.Show();

            //ViewProfessors viewProfessors = new ViewProfessors();
            //viewProfessors.Show();

            //ViewSubject viewSubject = new ViewSubject();
            //viewSubject.Show();

            // ViewIndex viewIndex = new ViewIndex();
            //viewIndex.Show();

            // ViewAdress viewAdress = new ViewAdress();
            // viewAdress.Show();

            // ViewDepartment viewDepartment = new ViewDepartment();
            // viewDepartment.Show();

            //ViewGrade viewGrade = new ViewGrade();
            // viewGrade.Show();
            private void ProfessorButton_Click(object sender, RoutedEventArgs e)
            {
                ViewProfessors viewProfessors = new ViewProfessors();
                viewProfessors.Show();
            }

            private void StudentButton_Click(object sender, RoutedEventArgs e)
            {
                ViewStudents viewStudents = new ViewStudents();
                viewStudents.Show();
            }

            private void SubjectButton_Click(object sender, RoutedEventArgs e)
            {
                ViewSubject viewSubject = new ViewSubject();
                viewSubject.Show();
            }
        
    }
}