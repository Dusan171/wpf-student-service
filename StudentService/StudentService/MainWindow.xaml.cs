using System.Windows;
using StudentService.DAO;
using StudentService.Gui;

namespace StudentService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StudentDao studentDao;
        private GradeDao gradeDao;

        public MainWindow()
        {
            InitializeComponent();

            // Inicijalizacija DAO objekata
            studentDao = new StudentDao();
            gradeDao = new GradeDao();

            // Otvaranje prozora za prikaz studenata
            ViewStudents viewStudents = new ViewStudents();
            viewStudents.Show();

            // Otvaranje prozora za prikaz profesora
            ViewProfessors viewProfessors = new ViewProfessors();
            viewProfessors.Show();

            // Otvaranje prozora za prikaz odeljenja
            ViewDepartments viewDepartments = new ViewDepartments();
            viewDepartments.Show();

            // Otvaranje prozora za prikaz ocena
            ViewGrade viewGrade = new ViewGrade(studentDao);
            viewGrade.Show();
        }
    }
}
