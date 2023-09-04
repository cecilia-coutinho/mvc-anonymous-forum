namespace AnonymousForum.Models.ViewModels
{
    public class TopicThreadReplyViewModel
    {
        public Topic? Topic { get; set; }
        public Thread? Thread { get; set; }
        public List<Reply>? Replies { get; set; }
    }
}
