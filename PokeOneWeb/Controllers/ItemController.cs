using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Dtos;
using System.Collections.Generic;
using PokeOneWeb.Services.Api;

namespace PokeOneWeb.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemApiService _itemApiService;

        public ItemController(IItemApiService itemApiService)
        {
            _itemApiService = itemApiService;
        }

        [Route("api/item/getitemstatboostsforpokemon")]
        [HttpGet]
        public ActionResult<IEnumerable<ItemStatBoostPokemonDto>> GetItemStatBoostsForPokemon([FromQuery] string name)
        {
            return Ok(_itemApiService.GetItemStatBoostsForPokemon(name));
        }
    }
}
