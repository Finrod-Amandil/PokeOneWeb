using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Dtos;
using PokeOneWeb.Services.Api;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Controllers
{
    public class NatureController : ControllerBase
    {
        private readonly INatureApiService _natureApiService;

        public NatureController(INatureApiService natureApiService)
        {
            _natureApiService = natureApiService;
        }

        /// <summary>
        /// Returns a list of all natures.
        /// </summary>
        [Route("api/nature/getall")]
        [HttpGet]
        public ActionResult<IEnumerable<NatureDto>> GetNatures()
        {
            return Ok(_natureApiService.GetAllNatures());
        }
    }
}
