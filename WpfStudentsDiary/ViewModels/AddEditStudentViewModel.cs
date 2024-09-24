﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfStudentsDiary.Commands;
using WpfStudentsDiary.Models;
using WpfStudentsDiary.Models.Wrappers;

namespace WpfStudentsDiary.ViewModels
{
    public class AddEditStudentViewModel : ViewModelBase
    {

        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if (student == null)
            {
                Student = new StudentWrapper();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }


        private StudentWrapper _student;
        public StudentWrapper Student
        {
            get { return _student; }
            set 
            { 
                _student = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GroupWrapper> _group;
        public ObservableCollection<GroupWrapper> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }


        private void Confirm(object obj)
        {
            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();

            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            //baza danych
        }

        private void AddStudent()
        {
            //baza danych
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }     

        private void InitGroups()
        {
            var groups = new ObservableCollection<GroupWrapper>
            {
            new GroupWrapper { Id=0, Name="-- brak --"},
            new GroupWrapper { Id=1, Name="1A"},
            new GroupWrapper { Id=2, Name="2A"},
            };

            Groups = new ObservableCollection<GroupWrapper>(groups);

            SelectedGroupId = Student.Group.Id;
        }
    }
}
