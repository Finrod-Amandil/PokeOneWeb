using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("PvpTier")]
    public class PvpTier
    {
        public int Id { get; set; }

        public int SortIndex { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
