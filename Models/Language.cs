using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InhlwathiTutors.Models
{
    public class Language
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TutorshipLanguage> TutorshipLanguages { get; set; }
    }
}
