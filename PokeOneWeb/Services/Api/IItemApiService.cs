using PokeOneWeb.WebApi.Dtos;
using System.Collections.Generic;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface IItemApiService
    {
        IEnumerable<ItemStatBoostPokemonDto> GetItemStatBoostsForPokemon(string pokemonVarietyResourceName);

        IEnumerable<ItemListDto> GetAllListItems();

        ItemDto GetItemByName(string itemResourceName);
    }
}
