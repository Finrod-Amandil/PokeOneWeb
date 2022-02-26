using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Dtos;
using PokeOneWeb.Services.Api;
using System.Collections.Generic;

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

        /// <summary>
        /// Returns a list of all moves, including move details (type, power, PP, priority etc.)
        /// </summary>
        [Route("getall")]
        [HttpGet]
        public ActionResult<IEnumerable<MoveDto>> GetAll()
        {
            return Ok(_moveApiService.GetAllMoves());
        }

        /// <summary>
        /// Returns a list of only the names of all moves, no details.
        /// </summary>
        [Route("getallnames")]
        [HttpGet]
        public ActionResult<IEnumerable<MoveNameDto>> GetAllNames()

        {
            return Ok(_moveApiService.GetAllMoveNames());
        }
    }
}
