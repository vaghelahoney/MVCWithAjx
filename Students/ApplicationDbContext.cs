using Microsoft.EntityFrameworkCore;
using Students.Models;

namespace Students
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        
        public DbSet<StudentDetails> StudentDetails { get; set; }

        public DbSet<Cities> Cities { get; set; }

        public DbSet<States> States { get; set; }
     
        public DbSet<Countries> Countries { get; set; }
        
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankDetails> BankDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentDetails)
                .WithOne()
                .HasForeignKey<StudentDetails>(sd => sd.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bank>()
                .HasMany(b => b.BankDetails)
                .WithOne()
                .HasForeignKey(bd => bd.BankId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
