using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InhlwathiTutors.Models
{
        public class Booking
        {
            [Key]
            public int Id { get; set; }

            // Foreign Key: Student who made the booking
            [Required]
            public string StudentId { get; set; }

            [ForeignKey("StudentId")]
            public SystemUser SystemUser { get; set; }

            // Foreign Key: TutorshipSubject being booked
            [Required]
            public int TutorshipSubjectId { get; set; }

            [ForeignKey("TutorshipSubjectId")]
            public TutorshipSubject TutorshipSubject { get; set; }

            // Optional description or goal of the booking
            [MaxLength(1000)]
            public string Note { get; set; }

            // Time and date of the requested session
            [Required]
            public DateTime ScheduledAt { get; set; }

            // Duration in minutes (e.g., 60 for 1 hour)
            [Required]
            public int DurationMinutes { get; set; }

            // Booking status
            [Required]
            [MaxLength(50)]
            public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Cancelled

            // Timestamp metadata
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; set; }
        }

}
