using Microsoft.EntityFrameworkCore;

namespace Presentation.Domain
{
    public class SchoolManagementDbContext: DbContext
    {
        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

    }
}
