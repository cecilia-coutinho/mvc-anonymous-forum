using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnonymousForum.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Invalid name or Format")]
        [StringLength(30, MinimumLength = 3), DisplayName("Topic Name"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string? TopicName { get; set; }
    }
}
