using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.WebApi.Dtos;
using PokeOneWeb.WebApi.Services.Api;

namespace PokeOneWeb.WebApi.Controllers
{
    [ApiController]
    [Route("api/locationGroups")]
    public class LocationGroupController : ControllerBase
    {
        private readonly ILocationGroupApiService _locationGroupApiService;

        public LocationGroupController(ILocationGroupApiService locationGroupApiService)
        {
            _locationGroupApiService = locationGroupApiService;
        }

        /// <summary>
        /// Returns a list of all location groups for a given region resource name.
        /// </summary>
        [Route("getallforregion")]
        [HttpGet]
        public ActionResult<IEnumerable<LocationGroupListDto>> GetAllForRegion([FromQuery] string regionName)
        {
            return Ok(_locationGroupApiService.GetAllListLocationGroupsForRegion(regionName));
        }
    }
}
