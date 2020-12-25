using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("BuildReadModel")]
    public class BuildReadModel
    {
        public int Id { get; set; }

        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }

        public string BuildName { get; set; }
        public string BuildDescription { get; set; }
        public string Move1 { get; set; }
        public string Move2 { get; set; }
        public string Move3 { get; set; }
        public string Move4 { get; set; }
        public List<ItemOptionReadModel> ItemOptions { get; set; }
        public List<NatureOptionReadModel> NatureOptions { get; set; }
        public string Ability { get; set; }
        public string AbilityDescription { get; set; }
        public int AtkEv { get; set; }
        public int SpaEv { get; set; }
        public int DefEv { get; set; }
        public int SpdEv { get; set; }
        public int SpeEv { get; set; }
        public int HpEv { get; set; }
        public List<AttackEffectivityReadModel> OffensiveCoverage { get; set; }
    }
}
