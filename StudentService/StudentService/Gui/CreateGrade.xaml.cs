using System;
using System.Collections.Generic;
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

using StudentService.Model.Enums;
using StudentService.Model;
using System.ComponentModel;
using StudentService.DAO;
using System.Collections.ObjectModel;
using System.Net;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for CreateGrade.xaml
    /// </summary>
    public partial class CreateDAOGrade : Window, INotifyPropertyChanged
    {
        // public CreateGrade()
        //{
        //     InitializeComponent();
        // }
        private GradeDao gradeDao;

        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private Student passedStudent;
        public Student PassedStudent
        {
            get => passedStudent;
            set
            {
                passedStudent = value;
                OnPropertyChanged(nameof(PassedStudent));
            }
        }
        private Subject subject;
        public Subject Subject
        {
            get => subject;
            set
            {
                subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }
        private int vAlue;
        public int Value
        {
            get => vAlue;
            set
            {
                vAlue = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public CreateDAOGrade(GradeDao gradeDao)
        {
            //InitializeComponent(); //crveno
            this.gradeDao = gradeDao;
            DataContext = this;// crveno u svakom

        }
        /*
 *  public int Id { get; set; }e
public Student PassedStudent { get; set; }
public Subject Subject { get; set; }
public int Value { get; set; }
public DateOnly Date { get; set; }
 */
        private void ClearFields()
        {
            Id = 0;
            Value = 0;
            //PassedStudent?
            //Subject?
            //Date?

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Grade instance directly from properties
                Grade newGrade = new Grade
                {
                    Id =Id,
                    Value=Value,

                };

                gradeDao.Create(newGrade);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
