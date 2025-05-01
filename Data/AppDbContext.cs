using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using InhlwathiTutors.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace InhlwathiTutors.Data
{
    public class AppDbContext : IdentityDbContext<SystemUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<StudentResident>()
            //    .HasIndex(sr => sr.StudentNumber)
            //    .IsUnique();
        }
    }
}
