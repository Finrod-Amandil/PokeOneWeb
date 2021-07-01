using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ReadModelDbContext _dbContext;

        public ItemController(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/item/getitemstatboostsforpokemon")]
        [HttpGet]
        public ActionResult<List<ItemStatBoostReadModel>> GetItemStatBoostsForPokemon([FromQuery] string name)
        {
            var itemStatBoosts = _dbContext.ItemStatBoostReadModels
                .Where(i =>
                    !i.HasRequiredPokemon ||
                    i.RequiredPokemonResourceName.Equals(name))
                .ToList();

            return itemStatBoosts;
        }
    }
}
