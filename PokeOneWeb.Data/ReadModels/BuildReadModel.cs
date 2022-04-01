using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("BuildReadModel")]
    public class BuildReadModel : IReadModel
    {
        public int Id { get; set; }
        public int ApplicationDbId { get; set; }

        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }

        public string BuildName { get; set; }
        public string BuildDescription { get; set; }
        public List<MoveOptionReadModel> MoveOptions { get; set; } = new();
        public List<ItemOptionReadModel> ItemOptions { get; set; } = new();
        public List<NatureOptionReadModel> NatureOptions { get; set; } = new();
        public string Ability { get; set; }
        public string AbilityDescription { get; set; }

        public int AtkEv { get; set; }
        public int SpaEv { get; set; }
        public int DefEv { get; set; }
        public int SpdEv { get; set; }
        public int SpeEv { get; set; }
        public int HpEv { get; set; }
        public List<AttackEffectivityReadModel> OffensiveCoverage { get; set; } = new();
    }
}