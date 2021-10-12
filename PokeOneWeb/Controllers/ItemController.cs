using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Dtos;
using System.Collections.Generic;
using PokeOneWeb.Services.Api;

namespace PokeOneWeb.Controllers
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

        [Route("getall")]
        [HttpGet]
        public ActionResult<IEnumerable<ItemListDto>> GetAll()
        {
            return Ok(_itemApiService.GetAllListItems());
        }

        [Route("getbyname")]
        [HttpGet]
        public ActionResult<ItemDto> GetByName([FromQuery] string name)
        {
            return Ok(_itemApiService.GetItemByName(name));
        }

        [Route("getitemstatboostsforpokemon")]
        [HttpGet]
        public ActionResult<IEnumerable<ItemStatBoostPokemonDto>> GetItemStatBoostsForPokemon([FromQuery] string name)
        {
            return Ok(_itemApiService.GetItemStatBoostsForPokemon(name));
        }
    }
}
