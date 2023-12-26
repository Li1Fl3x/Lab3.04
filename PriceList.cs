using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab3
{
    public class PriceList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? CodeBroadcast { get; set; }
        [Required]
        public string? NameBroadcast { get; set; }
        [Required]
        public string? PricePerMinute { get; set; }
        List<Registration> Registrations { get; set; } = new();
    }
}
