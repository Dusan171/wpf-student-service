using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

using StudentService.Model.Enums;
using StudentService.Model;
using System.ComponentModel;
using StudentService.DAO;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.RightsManagement;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for StudentIndex.xaml
    /// </summary>
    public partial class CreateStudentIndex : Window, INotifyPropertyChanged
    {
        private IndexDao indexDao;

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

        private string courseCode;
        public string CourseCode
        {
            get => courseCode;
            set
            {
                courseCode = value;
                OnPropertyChanged(nameof(CourseCode));

            }
        }
        private int registerNumber;
        public int RegisterNumber
        {
            get => registerNumber;
            set
            {
                registerNumber = value;
                OnPropertyChanged(nameof(RegisterNumber));
            }
        }
        private int registerYear;
        public int RegisterYear
        {
            get => registerYear;
            set
            {
                registerYear = value;
                OnPropertyChanged(nameof(RegisterYear));
            }
        }
        private void ClearFields()
        {
            Id = 0;
            CourseCode = string.Empty;
            RegisterNumber = 0;
            RegisterYear = 0;
        }
        public CreateStudentIndex(IndexDao indexDao)
        {
           // InitializeComponent(); crveno
            this.indexDao = indexDao;
            DataContext = this;

            //StudentStatuses = new ObservableCollection<StudentStatus>();
            //StudentStatuses.Add(StudentStatus.BUDGET);
            //StudentStatuses.Add(StudentStatus.SELF_FINANCE);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddIndexButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new Index instance directly from properties
                StudentIndex newStudentIndex = new StudentIndex
                {
                    //studentIndex gleda kao gui a ne iz klase model
                   // Id = Id,

                };

                //indexDao.Create(newStudentIndex);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /*
         *  public int Id { get; set; }
        public string CourseCode { get; set; }
        public int RegisterNumber { get; set; }
        public int RegisterYear { get; set; }

         */
       // public CreateStudentIndex()
       // {
           // InitializeComponent();
      // }
    }
}
