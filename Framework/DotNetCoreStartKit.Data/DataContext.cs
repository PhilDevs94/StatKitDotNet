using DotNetCoreStartKit.Core.DataContext;
using DotNetCoreStartKit.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreStartKit.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public override DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentCourse>()
                 .HasOne(x => x.Course)
                 .WithMany(m => m.StudentCourses)
                 .HasForeignKey(x => x.CourseId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(x => x.Student)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(x => x.StudentId);

        }
        public override int SaveChanges()
        {
            var changes = base.SaveChanges();
            return changes;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            return changesAsync;
        }

        internal Task<int> SaveChangesAsync()
        {
            var changes = base.SaveChangesAsync();
            return changes;
        }
    }
}
