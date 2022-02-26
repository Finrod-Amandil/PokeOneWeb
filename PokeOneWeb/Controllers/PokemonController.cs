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

        /// <summary>
        /// Returns a list of all Pokemon Varieties, with a medium amount of details.
        /// </summary>
        [Route("getall")]
        [HttpGet]
        public ActionResult<IEnumerable<PokemonVarietyListDto>> GetAll()
        {
            return Ok(_pokemonApiService.GetAllListPokemonVarieties());
        }

        /// <summary>
        /// Returns a list of all Pokemon Varieties with only a minimal set of details.
        /// </summary>
        [Route("getallbasic")]
        [HttpGet]
        public ActionResult<IEnumerable<BasicPokemonVarietyDto>> GetAllBasic()
        {
            return Ok(_pokemonApiService.GetAllBasicPokemonVarieties());
        }

        /// <summary>
        /// Returns one specific Pokemon Variety with its full set of details.
        /// </summary>
        /// <param name="name">The resource name of a specific Pokemon Variety</param>
        [Route("getbynamefull")]
        [HttpGet]
        public ActionResult<PokemonVarietyDto> GetByNameFull([FromQuery] string name)
        {
            return Ok(_pokemonApiService.GetPokemonVarietyByName(name));
        }

        /// <summary>
        /// Returns one specific Pokemon Variety with a medium amount of details.
        /// </summary>
        /// <param name="name">The resource name of a specific Pokemon Variety</param>
        [Route("getbyname")]
        [HttpGet]
        public ActionResult<PokemonVarietyListDto> GetByName([FromQuery] string name)
        {
            return Ok(_pokemonApiService.GetListPokemonVarietyByName(name));
        }

        /// <summary>
        /// Returns the names of all Pokemon Varieties which supply a given learnset, i.e. which are
        /// able to learn the given moves. The 16 parameters are understood in the following way:
        /// Show all Pokemon which can learn
        ///     (move "m11" OR "m12" OR "m13" OR "m14")
        /// AND (move "m21" OR "m22" OR "m23" OR "m24")
        /// AND (move "m31" OR "m32" OR "m33" OR "m34")
        /// AND (move "m41" OR "m42" OR "m43" OR "m44")
        ///
        /// Any of the parameters can be left unspecified and will then be ignored.
        /// </summary>
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
