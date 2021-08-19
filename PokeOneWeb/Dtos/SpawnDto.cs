using System.Collections.Generic;

namespace PokeOneWeb.Dtos
{
    public class SpawnDto
    {
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
        public string RarityString { get; set; }
        public decimal RarityValue { get; set; }
        public string Notes { get; set; }

        public IEnumerable<SeasonDto> Seasons { get; set; }
        public IEnumerable<TimeOfDayDto> TimesOfDay { get; set; }
    }
}
