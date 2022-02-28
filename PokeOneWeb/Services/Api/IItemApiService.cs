using System.Collections.Generic;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface IItemApiService
    {
        IEnumerable<ItemStatBoostPokemonDto> GetItemStatBoostsForPokemon(string pokemonVarietyResourceName);

        IEnumerable<ItemListDto> GetAllListItems();

        ItemDto GetItemByName(string itemResourceName);
    }
}
