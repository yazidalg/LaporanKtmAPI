using System;
using lapora_ktm_api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lapora_ktm_api.Config
{
	public class AppDbContext : IdentityDbContext<Student>
    {
        // Setup database
		public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        // build a report table
        public DbSet<Report> Reports { get; set; }
        // build student table
        public DbSet<Student> Students { get; set; }

        // Will build the migration from model
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Build entity student with property id and the value will auto generated every time
            // the create action do.
            builder.Entity<Student>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Will insert the Student to Table AspNetUsers (Student == AspNetUsers)
            builder.Entity<Student>().ToTable("AspNetUsers");

            // Make the id primary key
            builder.Entity<Student>().HasKey(e => e.Id);
            builder.Entity<Report>().HasKey(e => e.Id);

            // Define the relation between student to report with one-to-one relation
            builder.Entity<Student>()
                .HasOne(e => e.Report)
                .WithOne(e => e.Student)
                .HasForeignKey<Report>(e => e.StudentId)
                .HasConstraintName("FK_StudentId_Constraint")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

