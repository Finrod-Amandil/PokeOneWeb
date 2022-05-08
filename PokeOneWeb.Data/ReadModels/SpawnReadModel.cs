using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SpawnReadModel")]
    public class SpawnReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ApplicationDbId { get; set; }

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
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }
        public string SpawnType { get; set; }
        public int SpawnTypeSortIndex { get; set; }
        public string SpawnTypeColor { get; set; }
        public bool IsSyncable { get; set; }
        public bool IsInfinite { get; set; }
        public int LowestLevel { get; set; }
        public int HighestLevel { get; set; }
        public List<SeasonReadModel> Seasons { get; set; } = new();
        public List<TimeOfDayReadModel> TimesOfDay { get; set; } = new();
        public string RarityString { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal RarityValue { get; set; }

        public string Notes { get; set; }
    }
}