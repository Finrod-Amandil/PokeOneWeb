using System;
using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Data;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Controllers
{
    public class ApiController : Controller
    {
        private readonly ReadModelDbContext _dbContext;

        public ApiController(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult GetAllPokemon()
        {
            return Json(_dbContext.PokemonReadModels
                .Select(p => new 
                {
                    p.Id, p.ResourceName, p.PokedexNumber, p.Name, p.SpriteName,
                    p.Type1, 
                    p.Type2, p.Atk, p.Spa, p.Def, p.Spd, p.Spe, p.Hp, p.StatTotal,
                    p.PrimaryAbility, p.PrimaryAbilityEffect,
                    p.SecondaryAbility, p.SecondaryAbilityEffect,
                    p.HiddenAbility, p.HiddenAbilityEffect,
                    p.Availability, p.PvpTier, p.PvpTierSortIndex,
                    p.Generation, p.IsFullyEvolved, p.IsMega,
                    p.SmogonUrl, p.BulbapediaUrl, p.PokeOneCommunityUrl,
                    p.PokemonShowDownUrl, p.SerebiiUrl, p.PokemonDbUrl
                }));
        }

        public IActionResult GetPokemon([FromQuery] string resourceName)
        {
            var pokemon = _dbContext.PokemonReadModels
                .SingleOrDefault(p => p.ResourceName.Equals(resourceName, StringComparison.Ordinal));

            if (pokemon is null)
            {
                return NotFound();
            }
            return Json(pokemon);
        }

        public IActionResult GetBaseStatsForPokemon([FromQuery] string resourceName)
        {
            var pokemon = _dbContext.PokemonReadModels
                .SingleOrDefault(p => p.ResourceName.Equals(resourceName, StringComparison.Ordinal));

            if (pokemon is null)
            {
                return NotFound();
            }
            return Json(new
            {
                pokemon.Atk,
                pokemon.Spa,
                pokemon.Def,
                pokemon.Spd,
                pokemon.Spe,
                pokemon.Hp,
                pokemon.StatTotal
            });
        }

        public IActionResult GetAllMoves()
        {
            return Json(_dbContext.MoveReadModels);
        }

        public IActionResult GetAllMoveNames()
        {
            return Json(_dbContext.MoveReadModels.Select(m => m.Name));
        }

        public IActionResult GetEntityTypeForPath([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return Json(new {EntityType = EntityType.Unknown});
            }

            var matchingMapping = _dbContext.EntityTypeReadModels.SingleOrDefault(e => e.ResourceName.Equals(path));

            return Json(matchingMapping is null ? new { EntityType = EntityType.Unknown } : new { matchingMapping.EntityType });
        }

        public IActionResult GetAllPokemonForMoveSet(
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

            return Json(matchingPokemon);
        }
    }
}
