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
        public string Bio { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string HighestAchievement { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int YearsOfExperience { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Rating { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public ICollection<TutorshipSubject> TutorshipSubjects { get; set; }
        public ICollection<TutorshipLanguage>? TutorshipLanguages { get; set; } = new List<TutorshipLanguage>();

    }

}
