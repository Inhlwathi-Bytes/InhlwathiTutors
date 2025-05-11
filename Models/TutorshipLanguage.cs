using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InhlwathiTutors.Models
{
    public class TutorshipLanguage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TutorshipId { get; set; }

        public Tutorship Tutorship { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }
    }
}
