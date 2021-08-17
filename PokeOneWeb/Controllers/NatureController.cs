using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Services.Api;
using System.Collections.Generic;

namespace PokeOneWeb.Controllers
{
    public class NatureController : ControllerBase
    {
        private readonly INatureApiService _natureApiService;

        public NatureController(INatureApiService natureApiService)
        {
            _natureApiService = natureApiService;
        }

        [Route("api/nature/getnatures")]
        [HttpGet]
        public ActionResult<IEnumerable<NatureReadModel>> GetNatures()
        {
            return Ok(_natureApiService.GetAllNatures());
        }
    }
}
