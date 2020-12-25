using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("SpawnType")]
    public class SpawnType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsSyncable { get; set; }

        public bool IsInfinite { get; set; }
    }
}
