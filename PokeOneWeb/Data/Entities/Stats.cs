using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("Stats")]
    public class Stats
    {
        public int Id { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int HitPoints { get; set; }

        [NotMapped]
        public int Total => Attack + Defense + SpecialAttack + SpecialDefense + Speed + HitPoints;
    }
}
