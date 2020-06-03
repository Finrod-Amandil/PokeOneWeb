
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("MoveDamageClass")]
    public class MoveDamageClass
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
