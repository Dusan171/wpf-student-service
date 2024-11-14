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
//using System.Windows.Navigation;
//using System.Windows.Shapes;

using StudentService.Model;
using StudentService.DAO;
using System;
using System.ComponentModel;
using System.Windows;
using StudentService.Model.Enums;

using System.Collections.ObjectModel;
using System.Net;
using System.Security.RightsManagement;
using System.Threading.Channels;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateStudentIndex.xaml
    /// </summary>
    public partial class UpdateStudentIndex : Window, INotifyPropertyChanged
    {
        private readonly IndexDao indexDao;
        private StudentIndex currentIndex;
        public int Id
        {
            get => currentIndex.Id;
            set
            {
                currentStudentIndex.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string CourseCode
        {
            get => currentStudentIndex.CurseCode;
            set
            {
                courseCode = value;
                OnPropertyChanged(nameof(CourseCode));

            }
        }
        public int RegisterNumber
        {
            get => currentIndex.RegisterNumber;
            set
            {
                currentIndex.RegisterNumber = value;
                OnPropertyChanged(nameof(RegisterNumber));
            }
        }
        public int RegisterYear
        {
            get => currentINdex.RegisterYear;
            set
            {
                currentIndex.RegisterYear = value;
                OnPropertyChanged(nameof(RegisterYear));
            }
        }
        public UpdateStudentIndex(StudentIndex studentIndex, IndexDao indexDao)
        {
            InitializeComponent(); //crveno
            this.indexDao = indexDao;
            this.currentIndex= studentIndex;
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

        private void UpdateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                indexDao.UpdateIndex(currentIndex);
                MessageBox.Show("Index updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
