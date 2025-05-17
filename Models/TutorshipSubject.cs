using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InhlwathiTutors.Models
{
    
    public class TutorshipSubject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public string Availability { get; set; }

        [Required]
        public string Outline { get; set; }
        
        [Required]
        public string DeliveryMode { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal HourlyRate { get; set; } // New

        [Required] 
        public string Level { get; set; } // e.g., Beginner, Intermediate, Advanced
        public string? CoverImagePath { get; set; }
        public string? IntroVideoLink { get; set; }  // optional YouTube/Vimeo/etc.

        // Foreign Key
        [Required]
        [ForeignKey("Tutorship")]
        public int TutorshipId { get; set; }
        public Tutorship Tutorship { get; set; }
        
        public ICollection<TutorshipSubjectLanguage> TutorshipSubjectLanguages { get; set; } = new List<TutorshipSubjectLanguage>();

    }

}
