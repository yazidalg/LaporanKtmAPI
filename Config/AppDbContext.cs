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

		public DbSet<Report> Reports { get; set; }
	}
}

