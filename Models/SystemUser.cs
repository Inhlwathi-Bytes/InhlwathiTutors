using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InhlwathiTutors.Models
{
    public class SystemUser : IdentityUser
    {
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string mode { get; set; }
        public Tutorship Tutorship { get; set; }
        public SystemUser() { }
        public SystemUser(string studentNumber, string firstName, string lastName, string email)
        {
            StudentNumber = studentNumber ?? throw new ArgumentNullException(nameof(studentNumber));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email;
        }
    }
}