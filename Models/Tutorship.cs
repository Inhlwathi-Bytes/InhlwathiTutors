using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InhlwathiTutors.Models
{

    public class Tutorship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("SystemUser")]
        public string SystemUserId { get; set; }

        public SystemUser SystemUser { get; set; }

        public string Bio { get; set; } // Summary of tutor's approach, experience, etc.

        public string Qualifications { get; set; }

        public string Achievements { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int YearsOfExperience { get; set; }
        public DateTime CreatedAt { get; set; }

        public double Rating { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public ICollection<TutorshipSubject> TutorshipSubjects { get; set; }
        public ICollection<TutorshipLanguage>? TutorshipLanguages { get; set; } = new List<TutorshipLanguage>();

    }

}
