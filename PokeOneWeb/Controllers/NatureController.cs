using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.WebApi.Dtos;
using System.Collections.Generic;
using PokeOneWeb.WebApi.Services.Api;

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
