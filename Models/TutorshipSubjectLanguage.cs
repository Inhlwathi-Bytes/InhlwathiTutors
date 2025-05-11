using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InhlwathiTutors.Models
{
    public class TutorshipSubjectLanguage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("TutorshipSubject")]
        public int TutorshipSubjectId { get; set; }

        [Required]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }

        // Navigation properties
        public TutorshipSubject TutorshipSubject { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }

}
