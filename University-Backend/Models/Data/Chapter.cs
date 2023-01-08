namespace University_Backend.Models.Data
{
    public class Chapter : BaseEntity
    {
        public int CourseID { get; set; }
        public virtual Course Course { get; set; } = new Course();
        public string List { get; set; } = string.Empty;
    }
}