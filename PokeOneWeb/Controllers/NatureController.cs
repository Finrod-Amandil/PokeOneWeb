﻿using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Dtos;
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

        [Route("api/nature/getall")]
        [HttpGet]
        public ActionResult<IEnumerable<NatureDto>> GetNatures()
        {
            return Ok(_natureApiService.GetAllNatures());
        }
    }
}
