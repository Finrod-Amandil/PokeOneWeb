using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Dtos;
using PokeOneWeb.Services.Api;
using System.Collections.Generic;

namespace PokeOneWeb.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonApiService _pokemonApiService;

        public PokemonController(IPokemonApiService pokemonApiService)
        {
            _pokemonApiService = pokemonApiService;
        }

        [Route("getall")]
        [HttpGet]
        public ActionResult<IEnumerable<PokemonVarietyListDto>> GetAll()
        {
            return Ok(_pokemonApiService.GetAllListPokemonVarieties());
        }

        [Route("getallbasic")]
        [HttpGet]
        public ActionResult<IEnumerable<BasicPokemonVarietyDto>> GetAllBasic()
        {
            return Ok(_pokemonApiService.GetAllBasicPokemonVarieties());
        }

        [Route("getbynamefull")]
        [HttpGet]
        public ActionResult<PokemonVarietyDto> GetByNameFull([FromQuery] string name)
        {
            return Ok(_pokemonApiService.GetPokemonVarietyByName(name));
        }

        [Route("getbyname")]
        [HttpGet]
        public ActionResult<PokemonVarietyListDto> GetByName([FromQuery] string name)
        {
            return _pokemonApiService.GetListPokemonVarietyByName(name);
        }

        [Route("getallformoveset")]
        [HttpGet]
        public ActionResult<IEnumerable<PokemonVarietyNameDto>> GetAllForMoveSet(
            [FromQuery] string m11, [FromQuery] string m12, [FromQuery] string m13, [FromQuery] string m14,
            [FromQuery] string m21, [FromQuery] string m22, [FromQuery] string m23, [FromQuery] string m24,
            [FromQuery] string m31, [FromQuery] string m32, [FromQuery] string m33, [FromQuery] string m34,
            [FromQuery] string m41, [FromQuery] string m42, [FromQuery] string m43, [FromQuery] string m44)
        {
            return Ok(_pokemonApiService.GetAllPokemonVarietiesForMoveSet(
                m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44));
        }
    }
}
