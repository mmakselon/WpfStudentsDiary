using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfStudentsDiary.Models.Domains;
using WpfStudentsDiary.Models.Wrappers;
using System.Data.Entity;
using WpfStudentsDiary.Models.Converters;
using System;
using Diary.Models;

namespace WpfStudentsDiary
{
    public class Repository
    {

        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context
                    .Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)
                    .AsQueryable();

                if (groupId != 0)
                    students = students.Where(x => x.GroupId == groupId);

               return students
                    .ToList()
                    .Select(x=> x.ToWrapper())
                    .ToList();
            }
        }

        public void DeleteStudent(int id)
        {
            using(var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                var studentToUpdate = context.Students.Find(student.Id);
                studentToUpdate.Activities = student.Activities;
                studentToUpdate.Comments = student.Comments;
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.GroupId = student.GroupId;

                var studentsRaitings = context
                    .Ratings
                    .Where(x => x.StudentId == student.Id)
                    .ToList();

                var mathRatings = studentsRaitings
                    .Where(x => x.SubjectId == (int)Subject.Math)
                    .Select(x => x.Rate);

                var newMathRatings = ratings
                    .Where(x => x.SubjectId == (int)Subject.Math)
                    .Select(x => x.Rate);

                var mathRatingsToDelete = mathRatings.Except(newMathRatings).ToList();
                var mathRatingsToAdd = newMathRatings.Except(mathRatings).ToList();

                mathRatingsToDelete.ForEach(x =>
               {
                   var ratingToDelete = context.Ratings.First(y =>
                        y.Rate == x &&
                        y.StudentId == student.Id &&
                        y.SubjectId == (int)Subject.Math);

                   context.Ratings.Remove(ratingToDelete);
               });

                mathRatingsToAdd.ForEach(x =>
                {
                    var ratingToAdd = new Rating
                    {
                        Rate = x,
                        StudentId = student.Id,
                        SubjectId = (int)Subject.Math
                    };
                    context.Ratings.Add(ratingToAdd);     
                });


            }
        }

                public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using(var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                ratings.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });

                context.SaveChanges();
            }
        }
    }
}
