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

        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Tutorship> Tutorships { get; set; }
        public DbSet<TutorshipSubject> TutorshipSubjects { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<TutorshipLanguage> TutorshipLanguages { get; set; }
        public DbSet<TutorshipSubjectLanguage> TutorshipSubjectLanguages { get; set; }
        public DbSet<SubjectReview> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<StudentResident>()
            //    .HasIndex(sr => sr.StudentNumber)
            //    .IsUnique();
        }
    }
}
