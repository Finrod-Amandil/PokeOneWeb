using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("SpawnReadModel")]
    public class SpawnReadModel
    {
        public int Id { get; set; }

        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }

        public string LocationName { get; set; }
        public string SpawnType { get; set; }
        public bool IsSyncable { get; set; }
        public bool IsInfinite { get; set; }
        public List<SeasonReadModel> Season { get; set; }
        public List<TimeReadModel> Time { get; set; }
        public string Rarity { get; set; }

    }
}
