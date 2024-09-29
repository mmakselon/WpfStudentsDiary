using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfStudentsDiary.Models.Domains;
using WpfStudentsDiary.Models.Wrappers;
using System.Data.Entity;
using WpfStudentsDiary.Models.Converters;

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
    }
}
