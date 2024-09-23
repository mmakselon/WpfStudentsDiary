using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfStudentsDiary.Commands;
using WpfStudentsDiary.Models;

namespace WpfStudentsDiary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);

            Students = new ObservableCollection<Student>
            {
                new Student
                {
                    FirstName="Kazimierz",
                    LastName="Górka",
                    Group=new Group{Id=1}
                },
                new Student
                {
                    FirstName="Marek",
                    LastName="Nowak",
                    Group=new Group{Id=2}
                },
                new Student
                {
                    FirstName="Jan",
                    LastName="Kowalski",
                    Group=new Group{Id=1}
                }
            };
        }

        public ICommand RefreshStudentsCommand { get; set; }

        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }


        private void RefreshStudents(object obj)
        {
            MessageBox.Show("RefreshStudents");
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        } 
    }
}
