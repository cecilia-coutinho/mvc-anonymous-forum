using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnonymousForum.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Invalid name or Format")]
        [StringLength(30, MinimumLength = 3), DisplayName("Topics"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string? TopicName { get; set; }

        [JsonIgnore]
        public List<Thread>? Threads { get; set; }
    }
}
