using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.WebApi.Dtos;
using PokeOneWeb.WebApi.Services.Api;

namespace PokeOneWeb.WebApi.Controllers
{
    [ApiController]
    [Route("api/region")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionApiService _regionApiService;

        public RegionController(IRegionApiService regionApiService)
        {
            _regionApiService = regionApiService;
        }

        /// <summary>
        /// Returns a list of all regions.
        /// </summary>
        [Route("getall")]
        [HttpGet]
        public ActionResult<IEnumerable<RegionListDto>> GetAll()
        {
            return Ok(_regionApiService.GetAllListRegions());
        }
    }
}