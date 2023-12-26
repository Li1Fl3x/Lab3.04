using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab3
{
    public class Registration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public DateTime? DateBroadcast { get; set; }
        [Required]
        public string? CodeBroadcast { get; set; }
        [Required]
        public string? NameBroadcast { get; set; }
        [Required]
        public string? Regularity { get; set; }
        [Required]
        public string? TimeOnBroadcast { get; set; }
        [Required]
        public string? CostBroadcast { get; set; }
        [Required]
        public int PriceListId { get; set; }
        public PriceList? PriceList { get; set; }
    }
}
