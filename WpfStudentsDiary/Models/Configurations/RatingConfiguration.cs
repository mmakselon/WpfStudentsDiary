using WpfStudentsDiary.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace WpfStudentsDiary.Models.Configurations
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");

            HasKey(x => x.Id);
        }
    }
}
