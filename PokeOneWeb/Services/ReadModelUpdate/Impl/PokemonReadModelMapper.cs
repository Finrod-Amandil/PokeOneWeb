using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
{
    public class PokemonReadModelMapper : IReadModelMapper<PokemonReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public PokemonReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PokemonReadModel> MapFromDatabase()
        {
            var varieties = _dbContext.PokemonVarieties
                .Include(v => v.PokemonSpecies)
                .Include(v => v.DefaultForm)
                .ThenInclude(f => f.Availability)
                .Include(v => v.PvpTier)
                .Include(v => v.PrimaryAbility)
                .Include(v => v.SecondaryAbility)
                .Include(v => v.HiddenAbility)
                .Include(v => v.ElementalTypeCombination)
                .ThenInclude(e => e.PrimaryType)
                .Include(v => v.ElementalTypeCombination)
                .ThenInclude(e => e.SecondaryType)
                .Include(v => v.BaseStats)
                .Include(v => v.EvYield)
                .Where(v => v.DoInclude)
                .OrderBy(v => v.PokemonSpecies.PokedexNumber)
                .ThenBy(v => v.ResourceName);

            var index = 0;
            foreach (var variety in varieties)
            {
                index++;

                yield return new PokemonReadModel
                {
                    Id = index,

                    ResourceName = variety.ResourceName,
                    PokedexNumber = variety.PokemonSpecies.PokedexNumber,
                    Name = variety.Name,

                    SpriteName = variety.DefaultForm.SpriteName,

                    Type1 = variety.ElementalTypeCombination.PrimaryType.Name,
                    Type2 = variety.ElementalTypeCombination.SecondaryType?.Name,

                    Atk = variety.BaseStats.Attack,
                    Spa = variety.BaseStats.SpecialAttack,
                    Def = variety.BaseStats.Defense,
                    Spd = variety.BaseStats.SpecialDefense,
                    Spe = variety.BaseStats.Speed,
                    Hp = variety.BaseStats.HitPoints,
                    StatTotal = variety.BaseStats.Total,

                    PrimaryAbility = variety.PrimaryAbility?.Name,
                    PrimaryAbilityEffect = variety.PrimaryAbility?.EffectDescription,
                    SecondaryAbility = variety.SecondaryAbility?.Name,
                    SecondaryAbilityEffect = variety.SecondaryAbility?.EffectDescription,
                    HiddenAbility = variety.HiddenAbility?.Name,
                    HiddenAbilityEffect = variety.HiddenAbility?.EffectDescription,

                    Availability = variety.DefaultForm.Availability.Name,

                    PvpTier = variety.PvpTier?.Name,
                    PvpTierSortIndex = variety.PvpTier?.SortIndex ?? int.MaxValue,

                    Generation = variety.Generation,
                    IsFullyEvolved = variety.IsFullyEvolved,
                    IsMega = variety.IsMega,

                    SmogonUrl = variety.SmogonUrl,
                    BulbapediaUrl = variety.BulbapediaUrl,
                    PokeOneCommunityUrl = variety.PokeOneCommunityUrl,
                    PokemonShowDownUrl = variety.PokemonShowDownUrl,
                    SerebiiUrl = variety.SerebiiUrl,
                    PokemonDbUrl = variety.PokemonDbUrl,
                };
            }
        }
    }
}
