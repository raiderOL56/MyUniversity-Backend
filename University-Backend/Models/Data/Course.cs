using System.ComponentModel.DataAnnotations;

namespace University_Backend.Models.Data
{
    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public Level Level { get; set; } = Level.Basic;
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        [Required]
        public Chapter Index { get; set; } = new Chapter();
    }

    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }
}