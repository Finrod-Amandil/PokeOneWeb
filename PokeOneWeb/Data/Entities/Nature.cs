using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Nature")]
    public class Nature
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("StatBoostId")]
        public Stats StatBoost { get; set; }
        public int? StatBoostId { get; set; }
    }
}
