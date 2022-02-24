using PokeOneWeb.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.Api.Impl
{
    public class PokemonApiService : IPokemonApiService
    {
        private readonly ReadModelDbContext _dbContext;

        public PokemonApiService(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PokemonVarietyListDto> GetAllListPokemonVarieties()
        {
            return _dbContext.PokemonVarietyReadModels
                .Include(v => v.Urls)
                .AsSingleQuery()
                .AsNoTracking()
                .Select(ToListPokemonVariety());
        }

        public IEnumerable<BasicPokemonVarietyDto> GetAllBasicPokemonVarieties()
        {
            return _dbContext.PokemonVarietyReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Select(ToBasicPokemonVariety());
        }

        public PokemonVarietyDto GetPokemonVarietyByName(string pokemonVarietyResourceName)
        {
            var variety = _dbContext.PokemonVarietyReadModels
                .Where(v  => v.ResourceName.Equals(pokemonVarietyResourceName))
                .IncludeOptimized(p => p.Varieties)
                .IncludeOptimized(p => p.Forms)
                .IncludeOptimized(p => p.Urls)
                .IncludeOptimized(p => p.PrimaryEvolutionAbilities)
                .IncludeOptimized(p => p.SecondaryEvolutionAbilities)
                .IncludeOptimized(p => p.HiddenEvolutionAbilities)
                .IncludeOptimized(p => p.DefenseAttackEffectivities)
                .IncludeOptimized(p => p.Spawns)
                .IncludeOptimized(p => p.Spawns.Select(s => s.Seasons))
                .IncludeOptimized(p => p.Spawns.Select(s => s.TimesOfDay))
                .IncludeOptimized(p => p.Evolutions)
                .IncludeOptimized(p => p.LearnableMoves)
                .IncludeOptimized(p => p.LearnableMoves.Select(m => m.LearnMethods))
                .IncludeOptimized(p => p.HuntingConfigurations)
                .IncludeOptimized(p => p.Builds)
                .IncludeOptimized(p => p.Builds.Select(b => b.MoveOptions))
                .IncludeOptimized(p => p.Builds.Select(b => b.ItemOptions))
                .IncludeOptimized(p => p.Builds.Select(b => b.NatureOptions))
                .AsSingleQuery()
                .AsEnumerable()
                .Select(ToFullPokemonVariety())
                .SingleOrDefault();

            if (variety is null)
            {
                throw new ArgumentException($"No Pokémon Variety with name {pokemonVarietyResourceName} could be found.");
            }

            return variety;
        }

        public PokemonVarietyListDto GetListPokemonVarietyByName(string pokemonVarietyResourceName)
        {
            var variety = _dbContext.PokemonVarietyReadModels
                .Where(v  => v.ResourceName.Equals(pokemonVarietyResourceName))
                .Include(v => v.Urls)
                .AsSingleQuery()
                .AsNoTracking()
                .Select(ToListPokemonVariety())
                .SingleOrDefault();

            if (variety is null)
            {
                throw new ArgumentException($"No Pokémon Variety with name {pokemonVarietyResourceName} could be found.");
            }

            return variety;
        }

        public IEnumerable<PokemonVarietyNameDto> GetAllPokemonVarietiesForMoveSet(
            string move1Option1, string move1Option2, string move1Option3, string move1Option4, 
            string move2Option1, string move2Option2, string move2Option3, string move2Option4,
            string move3Option1, string move3Option2, string move3Option3, string move3Option4, 
            string move4Option1, string move4Option2, string move4Option3, string move4Option4)
        {
            var move1Options = new List<string> { move1Option1, move1Option2, move1Option3, move1Option4 }
                .Where(m => !string.IsNullOrWhiteSpace(m)).ToList();
            var move2Options = new List<string> { move2Option1, move2Option2, move2Option3, move2Option4 }
                .Where(m => !string.IsNullOrWhiteSpace(m)).ToList();
            var move3Options = new List<string> { move3Option1, move3Option2, move3Option3, move3Option4 }
                .Where(m => !string.IsNullOrWhiteSpace(m)).ToList();
            var move4Options = new List<string> { move4Option1, move4Option2, move4Option3, move4Option4 }
                .Where(m => !string.IsNullOrWhiteSpace(m)).ToList();

            var move1Query = _dbContext.SimpleLearnableMoveReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Where(lm => !move1Options.Any() || move1Options.Contains(lm.MoveResourceName))
                .Select(lm => lm.PokemonVarietyApplicationDbId);

            var move2Query = _dbContext.SimpleLearnableMoveReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Where(lm => !move2Options.Any() || move2Options.Contains(lm.MoveResourceName))
                .Select(lm => lm.PokemonVarietyApplicationDbId);

            var move3Query = _dbContext.SimpleLearnableMoveReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Where(lm => !move3Options.Any() || move3Options.Contains(lm.MoveResourceName))
                .Select(lm => lm.PokemonVarietyApplicationDbId);

            var move4Query = _dbContext.SimpleLearnableMoveReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Where(lm => !move4Options.Any() || move4Options.Contains(lm.MoveResourceName))
                .Select(lm => lm.PokemonVarietyApplicationDbId);

            return _dbContext.PokemonVarietyReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Where(p => move1Query.Contains(p.ApplicationDbId))
                .Where(p => move2Query.Contains(p.ApplicationDbId))
                .Where(p => move3Query.Contains(p.ApplicationDbId))
                .Where(p => move4Query.Contains(p.ApplicationDbId))
                .Select(ToPokemonVarietyName());
        }

        private static Func<PokemonVarietyReadModel, PokemonVarietyDto> ToFullPokemonVariety()
        {
            return v => new PokemonVarietyDto
            {
                ResourceName = v.ResourceName,
                SortIndex = v.SortIndex,
                PokedexNumber = v.PokedexNumber,
                Name = v.Name,
                SpriteName = v.SpriteName,
                PrimaryElementalType = v.PrimaryElementalType,
                SecondaryElementalType = v.SecondaryElementalType,

                Attack = v.Attack,
                SpecialAttack = v.SpecialAttack,
                Defense = v.Defense,
                SpecialDefense = v.SpecialDefense,
                Speed = v.Speed,
                HitPoints = v.HitPoints,
                StatTotal = v.Attack + v.SpecialAttack + v.Defense + v.SpecialDefense + v.Speed + v.HitPoints,

                PrimaryAbility = v.PrimaryAbility,
                PrimaryAbilityEffect = v.PrimaryAbilityEffect,
                SecondaryAbility = v.SecondaryAbility,
                SecondaryAbilityEffect = v.SecondaryAbilityEffect,
                HiddenAbility = v.HiddenAbility,
                HiddenAbilityEffect = v.HiddenAbilityEffect,

                Availability = v.Availability,
                AvailabilityDescription = v.AvailabilityDescription,
                PvpTier = v.PvpTier,
                PvpTierSortIndex = v.PvpTierSortIndex,
                Generation = v.Generation,
                IsFullyEvolved = v.IsFullyEvolved,
                IsMega = v.IsMega,
                CatchRate = v.CatchRate,
                HasGender = v.HasGender,
                MaleRatio = v.MaleRatio,
                FemaleRatio = v.FemaleRatio,
                EggCycles = v.EggCycles,
                Height = v.Height,
                Weight = v.Weight,
                ExpYield = v.ExpYield,
                
                AttackEv = v.AttackEv,
                SpecialAttackEv = v.SpecialAttackEv,
                DefenseEv = v.DefenseEv,
                SpecialDefenseEv = v.SpecialDefenseEv,
                SpeedEv = v.SpeedEv,
                HitPointsEv = v.HitPointsEv,

                Notes = v.Notes,

                PreviousPokemonResourceName = v.PreviousPokemonResourceName,
                PreviousPokemonSpriteName = v.PreviousPokemonSpriteName,
                PreviousPokemonName = v.PreviousPokemonName,
                NextPokemonResourceName = v.NextPokemonResourceName,
                NextPokemonSpriteName = v.NextPokemonSpriteName,
                NextPokemonName = v.NextPokemonName,

                PrimaryAbilityAttackBoost = v.PrimaryAbilityAttackBoost,
                PrimaryAbilitySpecialAttackBoost = v.PrimaryAbilitySpecialAttackBoost,
                PrimaryAbilityDefenseBoost = v.PrimaryAbilityDefenseBoost,
                PrimaryAbilitySpecialDefenseBoost = v.PrimaryAbilitySpecialDefenseBoost,
                PrimaryAbilitySpeedBoost = v.PrimaryAbilitySpeedBoost,
                PrimaryAbilityHitPointsBoost = v.PrimaryAbilityHitPointsBoost,
                PrimaryAbilityBoostConditions = v.PrimaryAbilityBoostConditions,

                SecondaryAbilityAttackBoost = v.SecondaryAbilityAttackBoost,
                SecondaryAbilitySpecialAttackBoost = v.SecondaryAbilitySpecialAttackBoost,
                SecondaryAbilityDefenseBoost = v.SecondaryAbilityDefenseBoost,
                SecondaryAbilitySpecialDefenseBoost = v.SecondaryAbilitySpecialDefenseBoost,
                SecondaryAbilitySpeedBoost = v.SecondaryAbilitySpeedBoost,
                SecondaryAbilityHitPointsBoost = v.SecondaryAbilityHitPointsBoost,
                SecondaryAbilityBoostConditions = v.SecondaryAbilityBoostConditions,

                HiddenAbilityAttackBoost = v.HiddenAbilityAttackBoost,
                HiddenAbilitySpecialAttackBoost = v.HiddenAbilitySpecialAttackBoost,
                HiddenAbilityDefenseBoost = v.HiddenAbilityDefenseBoost,
                HiddenAbilitySpecialDefenseBoost = v.HiddenAbilitySpecialDefenseBoost,
                HiddenAbilitySpeedBoost = v.HiddenAbilitySpeedBoost,
                HiddenAbilityHitPointsBoost = v.HiddenAbilityHitPointsBoost,
                HiddenAbilityBoostConditions = v.HiddenAbilityBoostConditions,

                Varieties = v.Varieties.Select(variety => new PokemonVarietyVarietyDto
                {
                    ResourceName = variety.ResourceName,
                    Name = variety.Name,
                    SortIndex = variety.SortIndex,
                    SpriteName = variety.SpriteName,
                    Availability = variety.Availability,
                    PrimaryType = variety.PrimaryType,
                    SecondaryType = variety.SecondaryType
                }),

                Forms = v.Forms.Select(f => new PokemonVarietyFormDto
                {
                    Name = f.Name,
                    SortIndex = f.SortIndex,
                    SpriteName = f.SpriteName,
                    Availability = f.Availability
                }),

                Urls = v.Urls.Select(u => new PokemonVarietyUrlDto
                {
                    Name = u.Name,
                    Url = u.Url
                }),

                PrimaryEvolutionAbilities = v.PrimaryEvolutionAbilities.Select(ToEvolutionAbilityDto()),
                SecondaryEvolutionAbilities = v.SecondaryEvolutionAbilities.Select(ToEvolutionAbilityDto()),
                HiddenEvolutionAbilities = v.HiddenEvolutionAbilities.Select(ToEvolutionAbilityDto()),

                DefenseAttackEffectivities = v.DefenseAttackEffectivities.Select(e => new AttackEffectivityDto
                {
                    TypeName = e.TypeName,
                    Effectivity = e.Effectivity
                }),

                Spawns = v.Spawns.Select(s => new SpawnDto
                {
                    PokemonFormSortIndex = s.PokemonFormSortIndex,
                    LocationSortIndex = s.LocationSortIndex,
                    PokemonResourceName = s.PokemonResourceName,
                    PokemonName = s.PokemonName,
                    SpriteName = s.SpriteName,
                    LocationName = s.LocationName,
                    LocationResourceName = s.LocationResourceName,
                    RegionName = s.RegionName,
                    RegionColor = s.RegionColor,
                    IsEvent = s.IsEvent,
                    EventName = s.EventName,
                    EventStartDate = s.EventStartDate,
                    EventEndDate = s.EventEndDate,
                    SpawnType = s.SpawnType,
                    SpawnTypeSortIndex = s.SpawnTypeSortIndex,
                    SpawnTypeColor = s.SpawnTypeColor,
                    IsSyncable = s.IsSyncable,
                    IsInfinite = s.IsInfinite,
                    LowestLevel = s.LowestLevel,
                    HighestLevel = s.HighestLevel,
                    RarityString = s.RarityString,
                    RarityValue = s.RarityValue,
                    Notes = s.Notes,

                    Seasons = s.Seasons.Select(season => new SeasonDto
                    {
                        Abbreviation = season.Abbreviation,
                        Name = season.Name,
                        SortIndex = season.SortIndex,
                        Color = season.Color
                    }),

                    TimesOfDay = s.TimesOfDay.Select(timeOfDay => new TimeOfDayDto
                    {
                        Abbreviation = timeOfDay.Abbreviation,
                        Name = timeOfDay.Name,
                        SortIndex = timeOfDay.SortIndex,
                        Color = timeOfDay.Color,
                        Times = timeOfDay.Times
                    })
                }),

                Evolutions = v.Evolutions.Select(e => new EvolutionDto
                {
                    BaseName = e.BaseName,
                    BaseResourceName = e.BaseResourceName,
                    BaseSpriteName = e.BaseSpriteName,
                    BasePrimaryElementalType = e.BasePrimaryElementalType,
                    BaseSecondaryElementalType = e.BaseSecondaryElementalType,
                    BaseSortIndex = e.BaseSortIndex,
                    BaseStage = e.BaseStage,

                    EvolvedName = e.EvolvedName,
                    EvolvedResourceName = e.EvolvedResourceName,
                    EvolvedSpriteName = e.EvolvedSpriteName,
                    EvolvedPrimaryElementalType = e.EvolvedPrimaryElementalType,
                    EvolvedSecondaryElementalType = e.EvolvedSecondaryElementalType,
                    EvolvedSortIndex = e.EvolvedSortIndex,
                    EvolvedStage = e.EvolvedStage,

                    EvolutionTrigger = e.EvolutionTrigger,
                    IsAvailable = e.IsAvailable,
                    IsReversible = e.IsReversible
                }),

                LearnableMoves = v.LearnableMoves.Select(l => new LearnableMoveDto
                {
                    IsAvailable = l.IsAvailable,
                    MoveName = l.MoveName,
                    ElementalType = l.ElementalType,
                    DamageClass = l.DamageClass,
                    BaseAttackPower = l.AttackPower,
                    EffectiveAttackPower = l.EffectivePower,
                    HasStab = l.HasStab,
                    Accuracy = l.Accuracy,
                    PowerPoints = l.PowerPoints,
                    Priority = l.Priority,
                    EffectDescription = l.EffectDescription,

                    LearnMethods = l.LearnMethods.Select(lm => new LearnMethodDto
                    {
                        IsAvailable = lm.IsAvailable,
                        LearnMethodName = lm.LearnMethodName,
                        Description = lm.Description,
                        SortIndex = lm.SortIndex
                    })
                }),

                HuntingConfigurations = v.HuntingConfigurations.Select(h => new HuntingConfigurationDto
                {
                    PokemonResourceName = h.PokemonResourceName,
                    PokemonName = h.PokemonName,
                    Nature = h.Nature,
                    NatureEffect = h.NatureEffect,
                    Ability = h.Ability
                }),

                Builds = v.Builds.Select(b => new BuildDto
                {
                    PokemonResourceName = b.PokemonResourceName,
                    PokemonName = b.PokemonName,
                    BuildName = b.BuildName,
                    BuildDescription = b.BuildDescription,
                    Ability = b.Ability,
                    AbilityDescription = b.AbilityDescription,

                    AtkEv = b.AtkEv,
                    SpaEv = b.SpaEv,
                    DefEv = b.DefEv,
                    SpdEv = b.SpdEv,
                    SpeEv = b.SpeEv,
                    HpEv = b.HpEv,

                    MoveOptions = b.MoveOptions.Select(mo => new MoveOptionDto
                    {
                        Slot = mo.Slot,
                        MoveName = mo.MoveName,
                        ElementalType = mo.ElementalType,
                        DamageClass = mo.DamageClass,
                        AttackPower = mo.AttackPower,
                        Accuracy = mo.Accuracy,
                        PowerPoints = mo.PowerPoints,
                        Priority = mo.Priority,
                        EffectDescription = mo.EffectDescription
                    }),
                    ItemOptions = b.ItemOptions.Select(io => new ItemOptionDto
                    {
                        ItemResourceName = io.ItemResourceName,
                        ItemName = io.ItemName
                    }),
                    NatureOptions = b.NatureOptions.Select(no => new NatureOptionDto
                    {
                        NatureName = no.NatureName,
                        NatureEffect = no.NatureEffect
                    })
                })
            };
        }

        private static Func<EvolutionAbilityReadModel, EvolutionAbilityDto> ToEvolutionAbilityDto()
        {
            return a => new EvolutionAbilityDto
            {
                RelativeStageIndex = a.RelativeStageIndex,
                PokemonResourceName = a.PokemonResourceName,
                PokemonSortIndex = a.PokemonSortIndex,
                PokemonName = a.PokemonName,
                SpriteName = a.SpriteName,
                AbilityName = a.AbilityName
            };
        }

        private static Expression<Func<PokemonVarietyReadModel, PokemonVarietyListDto>> ToListPokemonVariety()
        {
            return v => new PokemonVarietyListDto
            {
                ResourceName = v.ResourceName,
                SortIndex = v.SortIndex,
                PokedexNumber = v.PokedexNumber,
                Name = v.Name,
                SpriteName = v.SpriteName,
                PrimaryElementalType = v.PrimaryElementalType,
                SecondaryElementalType = v.SecondaryElementalType,

                Attack = v.Attack,
                SpecialAttack = v.SpecialAttack,
                Defense = v.Defense,
                SpecialDefense = v.SpecialDefense,
                Speed = v.Speed,
                HitPoints = v.HitPoints,
                StatTotal = v.Attack + v.SpecialAttack + v.Defense + v.SpecialDefense + v.Speed + v.HitPoints,

                PrimaryAbility = v.PrimaryAbility,
                PrimaryAbilityEffect = v.PrimaryAbilityEffect,
                SecondaryAbility = v.SecondaryAbility,
                SecondaryAbilityEffect = v.SecondaryAbilityEffect,
                HiddenAbility = v.HiddenAbility,
                HiddenAbilityEffect = v.HiddenAbilityEffect,

                Availability = v.Availability,
                PvpTier = v.PvpTier,
                PvpTierSortIndex = v.PvpTierSortIndex,
                Generation = v.Generation,
                IsFullyEvolved = v.IsFullyEvolved,
                IsMega = v.IsMega,

                Urls = v.Urls
                    .Select(u => new PokemonVarietyUrlDto
                    {
                        Name = u.Name,
                        Url = u.Url
                    })
                    .ToList(),

                Notes = v.Notes
            };
        }

        private static Expression<Func<PokemonVarietyReadModel, BasicPokemonVarietyDto>> ToBasicPokemonVariety()
        {
            return v => new BasicPokemonVarietyDto
            {
                ResourceName = v.ResourceName,
                SortIndex = v.SortIndex,
                PokedexNumber = v.PokedexNumber,
                Name = v.Name,
                SpriteName = v.SpriteName,
                Availability = v.Availability
            };
        }

        private static Expression<Func<PokemonVarietyReadModel, PokemonVarietyNameDto>> ToPokemonVarietyName()
        {
            return v => new PokemonVarietyNameDto
            {
                Name = v.Name,
                ResourceName = v.ResourceName
            };
        }
    }
}
