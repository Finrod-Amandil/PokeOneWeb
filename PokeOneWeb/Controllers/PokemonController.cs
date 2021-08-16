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
        public ActionResult<List<BasicPokemonVarietyDto>> GetAllBasic()
        {
            return _dbContext.PokemonVarietyReadModels
                .ToList()
                .Select(ToBasicPokemon)
                .ToList();
        }

        [Route("api/pokemon/getallnames")]
        [HttpGet]
        public ActionResult<List<PokemonVarietyNameDto>> GetAllNames()
        {
            return _dbContext.PokemonVarietyReadModels
                .ToList()
                .Select(ToPokemonName)
                .ToList();
        }

        [Route("api/pokemon/getbyname")]
        [HttpGet]
        public ActionResult<PokemonVarietyReadModel> GetByName([FromQuery] string name)
        {
            var pokemon = _dbContext.PokemonVarietyReadModels
                .Where(p => p.ResourceName.Equals(name))
                .IncludeOptimized(p => p.PrimaryEvolutionAbilities)
                .IncludeOptimized(p => p.SecondaryEvolutionAbilities)
                .IncludeOptimized(p => p.HiddenEvolutionAbilities)
                .IncludeOptimized(p => p.HuntingConfigurations)
                .IncludeOptimized(p => p.DefenseAttackEffectivities)
                .IncludeOptimized(p => p.Spawns)
                .IncludeOptimized(p => p.Spawns.Select(s => s.Seasons))
                .IncludeOptimized(p => p.Spawns.Select(s => s.TimesOfDay))
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
        public ActionResult<BasicPokemonVarietyDto> GetBasicByName([FromQuery] string name)
        {
            var pokemon = _dbContext.PokemonVarietyReadModels
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

            var matchingPokemon = _dbContext.PokemonVarietyReadModels
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

        private static BasicPokemonVarietyDto ToBasicPokemon(PokemonVarietyReadModel varietyReadModel)
        {
            return new BasicPokemonVarietyDto();
            /*{
                Id = varietyReadModel.Id,
                ResourceName = varietyReadModel.ResourceName,
                SortIndex = varietyReadModel.SortIndex,
                PokedexNumber = varietyReadModel.PokedexNumber,
                Name = varietyReadModel.Name,
                SpriteName = varietyReadModel.SpriteName,
                PrimaryElementalType = varietyReadModel.PrimaryType,
                SecondaryElementalType = varietyReadModel.SecondaryType,

                Atk = varietyReadModel.Attack,
                Spa = varietyReadModel.SpecialAttack,
                Def = varietyReadModel.Defense,
                Spd = varietyReadModel.SpecialDefense,
                Spe = varietyReadModel.Speed,
                Hp = varietyReadModel.HitPoints,
                StatTotal = varietyReadModel.StatTotal,

                PrimaryAbility = varietyReadModel.PrimaryAbility,
                PrimaryAbilityEffect = varietyReadModel.PrimaryAbilityEffect,
                SecondaryAbility = varietyReadModel.SecondaryAbility,
                SecondaryAbilityEffect = varietyReadModel.SecondaryAbilityEffect,
                HiddenAbility = varietyReadModel.HiddenAbility,
                HiddenAbilityEffect = varietyReadModel.HiddenAbilityEffect,

                Availability = varietyReadModel.Availability,
                PvpTier = varietyReadModel.PvpTier,
                PvpTierSortIndex = varietyReadModel.PvpTierSortIndex,
                Generation = varietyReadModel.Generation,
                IsFullyEvolved = varietyReadModel.IsFullyEvolved,
                IsMega = varietyReadModel.IsMega,

                SmogonUrl = varietyReadModel.SmogonUrl,
                BulbapediaUrl = varietyReadModel.BulbapediaUrl,
                PokeOneCommunityUrl = varietyReadModel.PokeOneCommunityUrl,
                PokemonShowDownUrl = varietyReadModel.PokemonShowDownUrl,
                SerebiiUrl = varietyReadModel.SerebiiUrl,
                PokemonDbUrl = varietyReadModel.PokemonDbUrl,

                Notes = varietyReadModel.Notes
            };*/
        }

        private static PokemonVarietyNameDto ToPokemonName(PokemonVarietyReadModel varietyReadModel)
        {
            return new PokemonVarietyNameDto
            {
                Id = varietyReadModel.Id,
                ResourceName = varietyReadModel.ResourceName,
                SortIndex = varietyReadModel.SortIndex,
                PokedexNumber = varietyReadModel.PokedexNumber,
                Name = varietyReadModel.Name,
                SpriteName = varietyReadModel.SpriteName,
                Availability = varietyReadModel.Availability
            };
        }
    }
}
