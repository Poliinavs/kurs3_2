using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7.Models
{
    public class Links
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public uint Minus { get; set; }
        public uint Plus { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
