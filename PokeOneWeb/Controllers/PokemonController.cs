using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Controllers.Dtos;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ReadModelDbContext _dbContext;

        public PokemonController(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/pokemon/getallbasic")]
        [HttpGet]
        public ActionResult<List<BasicPokemonDto>> GetAllBasic()
        {
            return _dbContext.PokemonReadModels
                .ToList()
                .Select(ToBasicPokemon)
                .ToList();
        }

        [Route("api/pokemon/getallnames")]
        [HttpGet]
        public ActionResult<List<PokemonNameDto>> GetAllNames()
        {
            return _dbContext.PokemonReadModels
                .ToList()
                .Select(ToPokemonName)
                .ToList();
        }

        [Route("api/pokemon/getbyname")]
        [HttpGet]
        public ActionResult<PokemonReadModel> GetByName([FromQuery] string name)
        {
            var pokemon = _dbContext.PokemonReadModels
                .Where(p => p.ResourceName.Equals(name))
                .IncludeOptimized(p => p.PrimaryAbilityTurnsInto)
                .IncludeOptimized(p => p.SecondaryAbilityTurnsInto)
                .IncludeOptimized(p => p.HiddenAbilityTurnsInto)
                .IncludeOptimized(p => p.HuntingConfigurations)
                .IncludeOptimized(p => p.DefenseAttackEffectivities)
                .IncludeOptimized(p => p.Spawns)
                .IncludeOptimized(p => p.Spawns.Select(s => s.Seasons))
                .IncludeOptimized(p => p.Spawns.Select(s => s.Times))
                .IncludeOptimized(p => p.Evolutions)
                .IncludeOptimized(p => p.LearnableMoves)
                .IncludeOptimized(p => p.LearnableMoves.Select(m => m.LearnMethods))
                .IncludeOptimized(p => p.Builds)
                .IncludeOptimized(p => p.Builds.Select(b => b.ItemOptions))
                .IncludeOptimized(p => p.Builds.Select(b => b.NatureOptions))
                .SingleOrDefault();

            if (pokemon is null)
            {
                return NotFound();
            }
            return pokemon;
        }

        [Route("api/pokemon/getbasicbyname")]
        [HttpGet]
        public ActionResult<BasicPokemonDto> GetBasicByName([FromQuery] string name)
        {
            var pokemon = _dbContext.PokemonReadModels
                .SingleOrDefault(p => p.ResourceName.Equals(name));

            if (pokemon is null)
            {
                return NotFound();
            }

            return ToBasicPokemon(pokemon);
        }

        [Route("api/pokemon/getallformoveset")]
        [HttpGet]
        public ActionResult<List<string>> GetAllForMoveSet(
            [FromQuery] string m11, [FromQuery] string m12, [FromQuery] string m13, [FromQuery] string m14,
            [FromQuery] string m21, [FromQuery] string m22, [FromQuery] string m23, [FromQuery] string m24,
            [FromQuery] string m31, [FromQuery] string m32, [FromQuery] string m33, [FromQuery] string m34,
            [FromQuery] string m41, [FromQuery] string m42, [FromQuery] string m43, [FromQuery] string m44)
        {
            var move1Options = new List<string> { m11, m12, m13, m14 }.Where(m => !string.IsNullOrWhiteSpace(m)).ToList();
            var move2Options = new List<string> { m21, m22, m23, m24 }.Where(m => !string.IsNullOrWhiteSpace(m)).ToList();
            var move3Options = new List<string> { m31, m32, m33, m34 }.Where(m => !string.IsNullOrWhiteSpace(m)).ToList();
            var move4Options = new List<string> { m41, m42, m43, m44 }.Where(m => !string.IsNullOrWhiteSpace(m)).ToList();

            var move1Query = _dbContext.SimpleLearnableMoveReadModels
                .Where(lm => !move1Options.Any() || move1Options.Contains(lm.MoveName))
                .Select(lm => lm.PokemonName);

            var move2Query = _dbContext.SimpleLearnableMoveReadModels
                .Where(lm => !move2Options.Any() || move2Options.Contains(lm.MoveName))
                .Select(lm => lm.PokemonName);

            var move3Query = _dbContext.SimpleLearnableMoveReadModels
                .Where(lm => !move3Options.Any() || move3Options.Contains(lm.MoveName))
                .Select(lm => lm.PokemonName);

            var move4Query = _dbContext.SimpleLearnableMoveReadModels
                .Where(lm => !move4Options.Any() || move4Options.Contains(lm.MoveName))
                .Select(lm => lm.PokemonName);

            var matchingPokemon = _dbContext.PokemonReadModels
                .Select(p => p.Name)
                .Where(p => move1Query.Contains(p))
                .Where(p => move2Query.Contains(p))
                .Where(p => move3Query.Contains(p))
                .Where(p => move4Query.Contains(p))
                .ToList();

            return matchingPokemon;
        }

        [Route("api/pokemon/getnatures")]
        [HttpGet]
        public ActionResult<List<NatureReadModel>> GetNatures()
        {
            return _dbContext.NatureReadModels.ToList();
        }

        private static BasicPokemonDto ToBasicPokemon(PokemonReadModel readModel)
        {
            return new BasicPokemonDto
            {
                Id = readModel.Id,
                ResourceName = readModel.ResourceName,
                SortIndex = readModel.SortIndex,
                PokedexNumber = readModel.PokedexNumber,
                Name = readModel.Name,
                SpriteName = readModel.SpriteName,
                Type1 = readModel.Type1,
                Type2 = readModel.Type2,

                Atk = readModel.Atk,
                Spa = readModel.Spa,
                Def = readModel.Def,
                Spd = readModel.Spd,
                Spe = readModel.Spe,
                Hp = readModel.Hp,
                StatTotal = readModel.StatTotal,

                PrimaryAbility = readModel.PrimaryAbility,
                PrimaryAbilityEffect = readModel.PrimaryAbilityEffect,
                SecondaryAbility = readModel.SecondaryAbility,
                SecondaryAbilityEffect = readModel.SecondaryAbilityEffect,
                HiddenAbility = readModel.HiddenAbility,
                HiddenAbilityEffect = readModel.HiddenAbilityEffect,

                Availability = readModel.Availability,
                PvpTier = readModel.PvpTier,
                PvpTierSortIndex = readModel.PvpTierSortIndex,
                Generation = readModel.Generation,
                IsFullyEvolved = readModel.IsFullyEvolved,
                IsMega = readModel.IsMega,

                SmogonUrl = readModel.SmogonUrl,
                BulbapediaUrl = readModel.BulbapediaUrl,
                PokeOneCommunityUrl = readModel.PokeOneCommunityUrl,
                PokemonShowDownUrl = readModel.PokemonShowDownUrl,
                SerebiiUrl = readModel.SerebiiUrl,
                PokemonDbUrl = readModel.PokemonDbUrl,

                Notes = readModel.Notes
            };
        }

        private static PokemonNameDto ToPokemonName(PokemonReadModel readModel)
        {
            return new PokemonNameDto
            {
                Id = readModel.Id,
                ResourceName = readModel.ResourceName,
                SortIndex = readModel.SortIndex,
                PokedexNumber = readModel.PokedexNumber,
                Name = readModel.Name,
                SpriteName = readModel.SpriteName,
                Availability = readModel.Availability
            };
        }
    }
}
