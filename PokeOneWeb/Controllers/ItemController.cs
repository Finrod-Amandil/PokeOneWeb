using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Services.Api;
using PokeOneWeb.WebApi.Dtos;
using System.Collections.Generic;
using PokeOneWeb.WebApi.Services.Api;

namespace PokeOneWeb.WebApi.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemApiService _itemApiService;

        public ItemController(IItemApiService itemApiService)
        {
            _itemApiService = itemApiService;
        }

        /// <summary>
        /// Returns a list of all items.
        /// </summary>
        [Route("getall")]
        [HttpGet]
        public ActionResult<IEnumerable<ItemListDto>> GetAll()
        {
            return Ok(_itemApiService.GetAllListItems());
        }

        /// <summary>
        /// Returns details of one item.
        /// </summary>
        /// <param name="name">The resource name of an item</param>
        [Route("getbyname")]
        [HttpGet]
        public ActionResult<ItemDto> GetByName([FromQuery] string name)
        {
            return Ok(_itemApiService.GetItemByName(name));
        }

        /// <summary>
        /// Returns all items, which boost (or reduce) a stat when held by the given Pokémon. Some items work on all
        /// Pokémon (i.e. Choice Scarf), other items only work on specific Pokémon (i.e. Eviolite on not-fully-evolved
        /// Pokémon, Thick Club only on Cubone and Marowak). Such Pokémon-specific items are only included in the list,
        /// if they have an effect for the given Pokémon.
        /// </summary>
        /// <param name="name">The resource name of a Pokémon Variety</param>
        /// <returns>A list of relevant stat-boosting items, along with which stats they boost and by how much.</returns>
        [Route("getitemstatboostsforpokemon")]
        [HttpGet]
        public ActionResult<IEnumerable<ItemStatBoostPokemonDto>> GetItemStatBoostsForPokemon([FromQuery] string name)
        {
            return Ok(_itemApiService.GetItemStatBoostsForPokemon(name));
        }
    }
}
