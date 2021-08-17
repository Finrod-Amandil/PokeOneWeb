using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Services.Api;
using System.Collections.Generic;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.Controllers
{
    [ApiController]
    [Route("api/move")]
    public class MoveController : ControllerBase
    {
        private readonly IMoveApiService _moveApiService;

        public MoveController(IMoveApiService moveApiService)
        {
            _moveApiService = moveApiService;
        }

        [Route("getall")]
        [HttpGet]
        public ActionResult<IEnumerable<MoveReadModel>> GetAll()
        {
            return Ok(_moveApiService.GetAllMoves());
        }

        [Route("getallnames")]
        [HttpGet]
        public ActionResult<IEnumerable<MoveNameDto>> GetAllNames()

        {
            return Ok(_moveApiService.GetAllMoveNames());
        }
    }
}
