using System.Collections.Generic;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.WebApi.Dtos
{
    public class BuildDto
    {
        public string PokemonResourceName { get; set; }
        public string PokemonName { get; set; }
        public string BuildName { get; set; }
        public string BuildDescription { get; set; }
        public string Ability { get; set; }
        public string AbilityDescription { get; set; }
        public int AtkEv { get; set; }
        public int SpaEv { get; set; }
        public int DefEv { get; set; }
        public int SpdEv { get; set; }
        public int SpeEv { get; set; }
        public int HpEv { get; set; }

        public IEnumerable<MoveOptionDto> MoveOptions { get; set; }
        public IEnumerable<ItemOptionDto> ItemOptions { get; set; }
        public IEnumerable<NatureOptionDto> NatureOptions { get; set; }
    }
}
