using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SpawnReadModel")]
    public class SpawnReadModel
    {
        public int Id { get; set; }
        public int PokemonFormSortIndex { get; set; }
        public int LocationSortIndex { get; set; }
        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }
        public string SpriteName { get; set; }

        public string LocationName { get; set; }
        public string LocationResourceName { get; set; }
        public string RegionName { get; set; }
        public string RegionColor { get; set; }
        public bool IsEvent { get; set; }
        public string EventName { get; set; }
        public string EventDateRange { get; set; }
        public string SpawnType { get; set; }
        public int SpawnTypeSortIndex { get; set; }
        public string SpawnTypeColor { get; set; }
        public bool IsSyncable { get; set; }
        public bool IsInfinite { get; set; }
        public List<SeasonReadModel> Seasons { get; set; }
        public List<TimeReadModel> Times { get; set; }
        public string RarityString { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal RarityValue { get; set; }
        public string Notes { get; set; }
    }
}
