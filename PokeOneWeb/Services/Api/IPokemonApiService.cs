using System.Collections.Generic;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface IPokemonApiService
    {
        IEnumerable<PokemonVarietyListDto> GetAllListPokemonVarieties();

        IEnumerable<BasicPokemonVarietyDto> GetAllBasicPokemonVarieties();

        PokemonVarietyDto GetPokemonVarietyByName(string pokemonVarietyResourceName);

        PokemonVarietyListDto GetListPokemonVarietyByName(string pokemonVarietyResourceName);

        IEnumerable<PokemonVarietyNameDto> GetAllPokemonVarietiesForMoveSet(
            string move1Option1, string move1Option2, string move1Option3, string move1Option4,
            string move2Option1, string move2Option2, string move2Option3, string move2Option4,
            string move3Option1, string move3Option2, string move3Option3, string move3Option4,
            string move4Option1, string move4Option2, string move4Option3, string move4Option4);
    }
}