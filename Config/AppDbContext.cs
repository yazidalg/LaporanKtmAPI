using System;
using lapora_ktm_api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lapora_ktm_api.Config
{
	public class AppDbContext : IdentityDbContext<Student>
    {
		public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Student>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Student>().ToTable("AspNetUsers");
            builder.Entity<Student>().HasKey(e => e.Id);
            builder.Entity<Report>().HasKey(e => e.Id);
            builder.Entity<Student>()
                .HasOne(e => e.Report)
                .WithOne(e => e.Student)
                .HasForeignKey<Report>(e => e.StudentId)
                .HasConstraintName("FK_StudentId_Constraint")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

