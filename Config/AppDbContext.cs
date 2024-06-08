using System;
using lapora_ktm_api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace lapora_ktm_api.Config
{
	public class AppDbContext : IdentityDbContext<Student>
    {
		public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Report> Reports { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

