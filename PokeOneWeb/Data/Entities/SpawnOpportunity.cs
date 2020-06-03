using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("SpawnOpportunity")]
    public class SpawnOpportunity
    {
        public int Id { get; set; }

        [ForeignKey("PokemonSpawnId")]
        public Spawn PokemonSpawn { get; set; }
        public int PokemonSpawnId { get; set; }

        [ForeignKey("SeasonId")]
        public Season Season { get; set; }
        public int SeasonId { get; set; }

        [ForeignKey("TimeOfDayId")]
        public TimeOfDay TimeOfDay { get; set; }
        public int TimeOfDayId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? SpawnProbability { get; set; }

        public int? EncounterCount { get; set; }

        public string SpawnCommonality { get; set; }
    }
}
