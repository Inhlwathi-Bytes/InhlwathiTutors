using System.ComponentModel.DataAnnotations;

namespace InhlwathiTutors.Models
{
    public class SubjectReview
    {
        public int Id { get; set; }

        public string SubjectId { get; set; } // FK to TutorshipSubject
        public TutorshipSubject Subject { get; set; }

        public string ReviewerId { get; set; } // FK to SystemUser or ApplicationUser
        public SystemUser Reviewer { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // Required star rating

        [Required]
        [MaxLength(1000)]
        public string ReviewText { get; set; } // Must be filled in when rating

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
