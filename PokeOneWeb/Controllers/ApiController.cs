using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.WebApi.Dtos;
using PokeOneWeb.WebApi.Services.Api;

namespace PokeOneWeb.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly IEntityTypeApiService _entityTypeApiService;

        public ApiController(IEntityTypeApiService entityTypeApiService)
        {
            _entityTypeApiService = entityTypeApiService;
        }

        /// <summary>
        /// For a given, unique resource name (i.e. "bulbasaur", "nugget") the entity type that
        /// belongs to this resource name (i.e. pokemon, item, location) is returned.
        /// </summary>
        /// <param name="path">A unique resource name / URL segment</param>
        /// <returns>The corresponding entity type. If resource name is unknown, returns EntityType.Unknown</returns>
        [Route("getentitytypeforpath")]
        [HttpGet]
        public ActionResult<EntityTypeDto> GetEntityTypeForPath([FromQuery] string path)
        {
            return Ok(_entityTypeApiService.GetEntityTypeForPath(path));
        }
    }
}
