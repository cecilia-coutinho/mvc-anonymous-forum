namespace AnonymousForum.Models
{
    public class Thread
    {
        public int ThreadId { get; set; }
        public string? ThreadTitle { get; set; }
        public string? ThreadDescription { get; set;}
        public int FkTopicId { get; set; }
    }
}
