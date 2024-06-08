using System;
using lapora_ktm_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using lapora_ktm_api.Entities.Students;

namespace lapora_ktm_api.Config
{
	public class AppDbContext : IdentityDbContext<IdentityUser>
    {
		public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

