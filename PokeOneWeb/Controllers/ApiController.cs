using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using System.Linq;
using PokeOneWeb.Controllers.Dtos;

namespace PokeOneWeb.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ReadModelDbContext _dbContext;

        public ApiController(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/getentitytypeforpath")]
        [HttpGet]
        public ActionResult<EntityTypeDto> GetEntityTypeForPath([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return new EntityTypeDto {EntityType = EntityType.Unknown};
            }

            var matchingMapping = _dbContext.EntityTypeReadModels
                .SingleOrDefault(e => e.ResourceName.Equals(path));

            return matchingMapping is null ? 
                new EntityTypeDto { EntityType = EntityType.Unknown } : 
                new EntityTypeDto { EntityType = matchingMapping.EntityType };
        }
    }
}
