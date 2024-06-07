using System;
using lapora_ktm_api.Models;
using Microsoft.EntityFrameworkCore;

namespace lapora_ktm_api.Config
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Report> Reports { get; set; }
	}
}

