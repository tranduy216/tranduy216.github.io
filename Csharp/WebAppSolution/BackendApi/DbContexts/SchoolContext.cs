using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.DbContexts
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<CourseModel> Courses { get; set; }

        public DbSet<StudentModel> Students { get; set; }

        public DbSet<EnrollmentModel> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseModel>().ToTable("courses");
            builder.Entity<StudentModel>().ToTable("students");
            builder.Entity<EnrollmentModel>().ToTable("enrollments");
        }
    }
}
