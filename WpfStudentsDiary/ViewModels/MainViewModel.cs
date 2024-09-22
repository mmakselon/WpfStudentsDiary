using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfStudentsDiary.Commands;

namespace WpfStudentsDiary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);
        }

        public ICommand RefreshStudentsCommand { get; set; }


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
