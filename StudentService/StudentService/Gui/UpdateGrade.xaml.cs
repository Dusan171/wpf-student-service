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
using System.Threading.Channels;

namespace StudentService.Gui
{
    /// <summary>
    /// Interaction logic for UpdateGrade.xaml
    /// </summary>
    public partial class UpdateGRADE : Window, INotifyPropertyChanged
    {
        // public UpdateGrade()
        // {
        //     InitializeComponent();
        //  }
        private readonly GradeDao gradeDao;
        private Grade currentGrade;
        public int Id
        {
            get => currentGrade.Id;
            set
            {
                currentGrade.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public Student PassedStudent
        {
            get => currentGrade.PassedStudent;
            set
            {
                currentGrade.PassedStudent = value;
                OnPropertyChanged(nameof(PassedStudent));
            }
        }
        public Subject Subject
        {
            get => currentGrade.Subject;
            set
            {
                currentGrade.Subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }
        public int Value
        {
            get => currentGrade.Value;
            set
            {
                currentGrade.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public UpdateGRADE(Grade grade, GradeDao gradeDao)
        {
           // InitializeComponent(); //crveno
            this.gradeDao = gradeDao;
            this.currentGrade = grade;
           // DataContext = this;// crveno u svakom

        }
        /*
 *  public int Id { get; set; }e
public Student PassedStudent { get; set; }
public Subject Subject { get; set; }
public int Value { get; set; }
public DateOnly Date { get; set; }
 */
      
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gradeDao.UpdateGrade(currentGrade);
                MessageBox.Show("Grade updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //Close(); //crveno
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating grade: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
