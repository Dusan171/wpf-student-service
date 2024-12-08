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
using System;
using System.ComponentModel;
using System.Windows;
using StudentService.DAO;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateGrade.xaml
    /// </summary>
    public partial class CreateGrade : Window, INotifyPropertyChanged
    {
        private GradeDao gradeDao;
        //public int Id { get; set; }
        private int id;
        public int Id
        {
            get => id;
            set
            {
                 id= value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //public Student PassedStudent { get; set; }
        private Student passedStudent;
        public Student PassedStudent
        {
            get => passedStudent;
            set
            {
                passedStudent= value;
                OnPropertyChanged(nameof(PassedStudent));
            }
        }
        //public Subject Subject { get; set; }
        private Subject subject;
        public Subject Subject
        {
            get => subject;
            set
            {
                subject= value;
                OnPropertyChanged(nameof(Subject));
            }
        }
        //public int Value { get; set; }
        private int VALUE;
        public int Value
        {
            get => VALUE;
            set
            {
                VALUE = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        // public DateOnly Date { get; set; }
        
        private DateTime? date;
        public DateTime? Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public CreateGrade(GradeDao gradeDao)
        {
            InitializeComponent();
            this.gradeDao = gradeDao;
            DataContext = this;
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grade newGrade = new Grade
                {
                    Date = DateOnly.FromDateTime(Date.Value),
                    Id=Id,
                    Value=Value,
                    /*
                     *     
        public Student PassedStudent { get; set; }
        public Subject Subject { get; set; }
                     */
                    Subject = Subject,
                    PassedStudent = PassedStudent

                };

                gradeDao.Create(newGrade);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding grade: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        { 
            Id = 0;
            Value = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
