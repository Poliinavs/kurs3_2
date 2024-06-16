using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7.Models
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SessionId { get; set; }
        public DateTime Stamp { get; init; } = DateTime.Now;
        public string Text { get; set; }
        public Links Link { get; set; }
        public string Role { get; set; }
    }
}
