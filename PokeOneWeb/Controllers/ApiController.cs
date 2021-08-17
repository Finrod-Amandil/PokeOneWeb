using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Dtos;
using PokeOneWeb.Services.Api;

namespace PokeOneWeb.Controllers
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

        [Route("getentitytypeforpath")]
        [HttpGet]
        public ActionResult<EntityTypeDto> GetEntityTypeForPath([FromQuery] string path)
        {
            return Ok(_entityTypeApiService.GetEntityTypeForPath(path));
        }
    }
}
