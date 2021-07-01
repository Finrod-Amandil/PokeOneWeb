using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.LearnableMoveLearnMethods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
{
    public class PokemonReadModelMapper : IReadModelMapper<PokemonReadModel>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PokemonReadModelMapper> _logger;

        private List<ElementalTypeRelation> _elementalTypeRelations;
        private List<ElementalType> _elementalTypes;

        public PokemonReadModelMapper(ApplicationDbContext dbContext, ILogger<PokemonReadModelMapper> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<PokemonReadModel> MapFromDatabase()
        {
            var varietyIds = _dbContext.PokemonVarieties
                .Include(v => v.DefaultForm)
                .AsNoTracking()
                .Where(v => v.DoInclude)
                .OrderBy(v => v.DefaultForm.SortIndex)
                .Select(v => v.Id);

            _elementalTypeRelations = _dbContext.ElementalTypeRelations
                .AsNoTracking()
                .ToList();

            _elementalTypes = _dbContext.ElementalTypes
                .AsNoTracking()
                .ToList();

            var index = 0;
            foreach (var varietyId in varietyIds)
            {
                index++;
                var variety = LoadVariety(varietyId);
                
                var readModel = GetBasicReadModel(variety, index);
                readModel.DefenseAttackEffectivities = GetAttackEffectivityReadModels(variety);

                readModel.LearnableMoves = GetLearnableMoves(variety);

                var evolvedVarieties = GetEvolvedVarietiesWithAbilities(variety);
                readModel.PrimaryAbilityTurnsInto = GetPrimaryAbilityTurnsInto(variety, evolvedVarieties);
                readModel.SecondaryAbilityTurnsInto = GetSecondaryAbilityTurnsInto(variety, evolvedVarieties);
                readModel.HiddenAbilityTurnsInto = GetHiddenAbilityTurnsInto(variety, evolvedVarieties);

                var allVarietiesOfEvolutionLine = GetVarietiesOfEvolutionLine(variety);
                readModel.HuntingConfigurations = GetHuntingConfigurations(allVarietiesOfEvolutionLine);
                readModel.Spawns = GetSpawnReadModels(allVarietiesOfEvolutionLine);
                readModel.Evolutions = GetEvolutions(variety);

                readModel.Builds = GetBuilds(variety);

                yield return readModel;
            }
        }

        private PokemonVariety LoadVariety(int varietyId)
        {
            return _dbContext.PokemonVarieties
                .Include(v => v.PokemonSpecies)
                .Include(v => v.DefaultForm.Availability)
                .Include(v => v.PvpTier)
                .Include(v => v.PrimaryAbility)
                .Include(v => v.SecondaryAbility)
                .Include(v => v.HiddenAbility)
                .Include(v => v.PrimaryType)
                .Include(v => v.SecondaryType)
                .Include(v => v.BaseStats)
                .Include(v => v.EvYield)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.Move.DamageClass)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.Move.ElementalType)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.Price)
                .ThenInclude(lmp => lmp.Price.Currency.Item)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.RequiredItem)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveLearnMethod.Locations)
                .ThenInclude(mlml => mlml.Location.LocationGroup.Region)
                .Include(v => v.Builds)
                .ThenInclude(b => b.Ability)
                .Include(v => v.Builds)
                .ThenInclude(b => b.EvDistribution)
                .Include(v => v.Builds)
                .ThenInclude(b => b.Nature)
                .ThenInclude(no => no.Nature.StatBoost)
                .Include(v => v.Builds)
                .ThenInclude(b => b.Item)
                .ThenInclude(io => io.Item)
                .Include(v => v.Builds)
                .ThenInclude(b => b.Moves)
                .ThenInclude(mo => mo.Move)
                .AsNoTracking()
                .Single(v => v.Id == varietyId);
        }

        private PokemonReadModel GetBasicReadModel(PokemonVariety variety, int index)
        {
            return new PokemonReadModel
            {
                Id = index,

                ResourceName = variety.ResourceName,
                PokedexNumber = variety.PokemonSpecies.PokedexNumber,
                Name = variety.Name,

                SpriteName = variety.DefaultForm.SpriteName,

                Type1 = variety.PrimaryType.Name,
                Type2 = variety.SecondaryType?.Name,

                Atk = (int)variety.BaseStats.Attack,
                Spa = (int)variety.BaseStats.SpecialAttack,
                Def = (int)variety.BaseStats.Defense,
                Spd = (int)variety.BaseStats.SpecialDefense,
                Spe = (int)variety.BaseStats.Speed,
                Hp = (int)variety.BaseStats.HitPoints,
                StatTotal = (int)variety.BaseStats.Total,

                AtkEv = (int)variety.EvYield.Attack,
                SpaEv = (int)variety.EvYield.SpecialAttack,
                DefEv = (int)variety.EvYield.Defense,
                SpdEv = (int)variety.EvYield.SpecialDefense,
                SpeEv = (int)variety.EvYield.Speed,
                HpEv = (int)variety.EvYield.HitPoints,

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

                CatchRate = variety.CatchRate,

                SmogonUrl = variety.SmogonUrl,
                BulbapediaUrl = variety.BulbapediaUrl,
                PokeOneCommunityUrl = variety.PokeOneCommunityUrl,
                PokemonShowDownUrl = variety.PokemonShowDownUrl,
                SerebiiUrl = variety.SerebiiUrl,
                PokemonDbUrl = variety.PokemonDbUrl,

                Notes = variety.Notes
            };
        }

        private List<AbilityTurnsIntoReadModel> GetPrimaryAbilityTurnsInto(
            PokemonVariety variety, List<PokemonVariety> evolvedVarieties)
        {
            return evolvedVarieties
                .OrderBy(v => v.DefaultForm.SortIndex)
                .Select(v => new AbilityTurnsIntoReadModel
                {
                    PokemonResourceName = v.ResourceName,
                    PokemonSortIndex = v.DefaultForm.SortIndex,
                    PokemonName = v.Name,
                    AbilityName = v.PrimaryAbility.Name
                })
                .ToList();
        }

        private List<AbilityTurnsIntoReadModel> GetSecondaryAbilityTurnsInto(
            PokemonVariety variety, List<PokemonVariety> evolvedVarieties)
        {
            if (variety.SecondaryAbility is null)
            {
                return new List<AbilityTurnsIntoReadModel>();
            }

            return evolvedVarieties
                .OrderBy(v => v.DefaultForm.SortIndex)
                .Select(v => new AbilityTurnsIntoReadModel
                {
                    PokemonResourceName = v.ResourceName,
                    PokemonSortIndex = v.DefaultForm.SortIndex,
                    PokemonName = v.Name,
                    AbilityName = v.SecondaryAbility?.Name ?? v.PrimaryAbility.Name
                })
                .ToList();
        }

        private List<AbilityTurnsIntoReadModel> GetHiddenAbilityTurnsInto(
            PokemonVariety variety, List<PokemonVariety> evolvedVarieties)
        {
            if (variety.HiddenAbility is null)
            {
                return new List<AbilityTurnsIntoReadModel>();
            }

            return evolvedVarieties
                .OrderBy(v => v.DefaultForm.SortIndex)
                .Select(v => new AbilityTurnsIntoReadModel
                {
                    PokemonResourceName = v.ResourceName,
                    PokemonSortIndex = v.DefaultForm.SortIndex,
                    PokemonName = v.Name,
                    AbilityName = v.HiddenAbility?.Name ?? v.PrimaryAbility.Name
                })
                .ToList();
        }

        private List<PokemonVariety> GetEvolvedVarietiesWithAbilities(PokemonVariety variety)
        {
            var evolvedVarieties = new List<int>();
            var nextStageVarieties = new List<int> { variety.Id };

            do
            {
                nextStageVarieties = _dbContext.Evolutions
                    .AsNoTracking()
                    .Where(e => nextStageVarieties.Contains(e.BasePokemonVarietyId))
                    .Select(e => e.EvolvedPokemonVarietyId)
                    .ToList();

                nextStageVarieties.RemoveAll(v => evolvedVarieties.Contains(v));

                evolvedVarieties.AddRange(nextStageVarieties);
            } while (nextStageVarieties.Any());

            return _dbContext.PokemonVarieties
                .Include(v => v.DefaultForm)
                .Include(v => v.PrimaryAbility)
                .Include(v => v.SecondaryAbility)
                .Include(v => v.HiddenAbility)
                .AsNoTracking()
                .Where(v => evolvedVarieties.Contains(v.Id))
                .ToList();
        }

        private List<int> GetVarietiesOfEvolutionLine(PokemonVariety variety)
        {
            var allEvolutions = _dbContext.Evolutions.AsNoTracking();
            var varietyIds = new List<int>();

            varietyIds.Add(variety.Id);

            var hasFoundNewEvolutions = false;

            do
            {
                var newIds = new List<int>();
                newIds = newIds.Union(varietyIds).ToList();
                newIds = newIds.Union(
                        allEvolutions
                            .Where(e => varietyIds.Contains(e.BasePokemonVarietyId))
                            .Select(e => e.EvolvedPokemonVarietyId))
                    .ToList();

                newIds = newIds.Union(
                        allEvolutions
                            .Where(e => varietyIds.Contains(e.EvolvedPokemonVarietyId))
                            .Select(e => e.BasePokemonVarietyId))
                    .ToList();

                hasFoundNewEvolutions = newIds.Count > varietyIds.Count;
                varietyIds = newIds;
            } while (hasFoundNewEvolutions);

            return varietyIds;
        }

        private List<HuntingConfigurationReadModel> GetHuntingConfigurations(List<int> varietyIds)
        {
            return _dbContext.HuntingConfigurations
                .Include(hc => hc.Ability)
                .Include(hc => hc.Nature.StatBoost)
                .Include(hc => hc.PokemonVariety.DefaultForm)
                .AsNoTracking()
                .Where(hc => varietyIds.Contains(hc.PokemonVarietyId))
                .OrderBy(hc => hc.PokemonVariety.DefaultForm.SortIndex)
                .ToList()
                .Select(hc => new HuntingConfigurationReadModel 
                    {
                        PokemonResourceName = hc.PokemonVariety.ResourceName,
                        PokemonName = hc.PokemonVariety.Name,
                        Nature = hc.Nature.Name,
                        NatureEffect = CommonFormatHelper.GetNatureEffect(hc.Nature),
                        Ability = hc.Ability.Name
                    })
                .ToList();
        }

        

        private List<AttackEffectivityReadModel> GetAttackEffectivityReadModels(PokemonVariety variety)
        {
            var attackEffectivities = new List<AttackEffectivityReadModel>();

            foreach (var elementalType in _elementalTypes)
            {
                var effectivity = _elementalTypeRelations.Single(er =>
                        er.AttackingTypeId == elementalType.Id &&
                        er.DefendingTypeId == variety.PrimaryTypeId)
                    .AttackEffectivity;

                if (variety.SecondaryType != null)
                {
                    effectivity *= _elementalTypeRelations.Single(er =>
                            er.AttackingTypeId == elementalType.Id &&
                            er.DefendingTypeId == variety.SecondaryTypeId)
                        .AttackEffectivity;
                }

                attackEffectivities.Add(new AttackEffectivityReadModel
                {
                    TypeName = elementalType.Name,
                    Effectivity = effectivity
                });
            }

            return attackEffectivities;
        }

        private List<SpawnReadModel> GetSpawnReadModels(List<int> varietyIds)
        {
            return _dbContext.PokemonForms
                .Include(f => f.PokemonVariety)
                .Include(f => f.PokemonSpawns)
                .ThenInclude(s => s.Location.LocationGroup.Region.Event)
                .Include(f => f.PokemonSpawns)
                .ThenInclude(s => s.SpawnType)
                .Include(f => f.PokemonSpawns)
                .ThenInclude(s => s.SpawnOpportunities)
                .ThenInclude(o => o.Season)
                .Include(f => f.PokemonSpawns)
                .ThenInclude(s => s.SpawnOpportunities)
                .ThenInclude(o => o.TimeOfDay)
                .ThenInclude(t => t.SeasonTimes)
                .ThenInclude(st => st.Season)
                .AsNoTracking()
                .Where(f => varietyIds.Contains(f.PokemonVarietyId))
                .ToList()
                .SelectMany(f => f.PokemonSpawns)
                .SelectMany(GetSpawnReadModels)
                .ToList();
        }

        private List<SpawnReadModel> GetSpawnReadModels(Spawn spawn)
        {
            var spawnReadModels = new List<SpawnReadModel>();

            foreach (var spawnOpportunity in spawn.SpawnOpportunities)
            {
                var rarityString = GetRarityAsString(spawnOpportunity);
                var rarityValue = GetRarityValue(spawnOpportunity);
                var time = new TimeReadModel
                {
                    SortIndex = spawnOpportunity.TimeOfDay.SortIndex,
                    Name = spawnOpportunity.TimeOfDay.Name,
                    Abbreviation = spawnOpportunity.TimeOfDay.Abbreviation,
                    Color = spawnOpportunity.TimeOfDay.Color,
                    Times = GetTimesAsString(spawnOpportunity.TimeOfDay)
                };
                var season = new SeasonReadModel
                {
                    SortIndex = spawnOpportunity.Season.SortIndex,
                    Name = spawnOpportunity.Season.Name,
                    Abbreviation = spawnOpportunity.Season.Abbreviation,
                    Color = spawnOpportunity.Season.Color
                };

                var spawnWithSameRarity = spawnReadModels.SingleOrDefault(s => 
                    s.RarityString.Equals(rarityString, StringComparison.Ordinal));

                if (spawnWithSameRarity != null)
                {
                    if (!spawnWithSameRarity.Times.Any(t =>
                        t.Abbreviation.Equals(time.Abbreviation, StringComparison.Ordinal)))
                    {
                        spawnWithSameRarity.Times.Add(time);
                    }

                    if (!spawnWithSameRarity.Seasons.Any(s =>
                        s.Abbreviation.Equals(season.Abbreviation, StringComparison.Ordinal)))
                    {
                        spawnWithSameRarity.Seasons.Add(season);
                    }
                    
                    continue;
                }

                var spawnReadModel = new SpawnReadModel
                {
                    PokemonFormSortIndex = spawn.PokemonForm.SortIndex,
                    LocationSortIndex = spawn.Location.SortIndex,
                    PokemonResourceName = spawn.PokemonForm.PokemonVariety.ResourceName,
                    PokemonName = spawn.PokemonForm.Name,
                    SpriteName = spawn.PokemonForm.SpriteName,
                    LocationName = spawn.Location.Name,
                    LocationResourceName = spawn.Location.LocationGroup.ResourceName,
                    RegionName = spawn.Location.LocationGroup.Region.Name,
                    RegionColor = spawn.Location.LocationGroup.Region.Color,
                    IsEvent = spawn.Location.LocationGroup.Region.IsEventRegion,
                    EventName = spawn.Location.LocationGroup.Region.Event?.Name,
                    SpawnType = spawn.SpawnType.Name,
                    SpawnTypeSortIndex = spawn.SpawnType.SortIndex,
                    SpawnTypeColor = spawn.SpawnType.Color,
                    IsSyncable = spawn.SpawnType.IsSyncable,
                    IsInfinite = spawn.SpawnType.IsInfinite,
                    Times = new List<TimeReadModel> {time},
                    Seasons = new List<SeasonReadModel> {season},
                    RarityString = rarityString,
                    RarityValue = rarityValue,
                    Notes = spawn.Notes
                };

                var spawnEvent = spawn.Location.LocationGroup.Region.Event;

                if (spawnEvent != null)
                {
                    var dateTimeCulture = CultureInfo.CreateSpecificCulture("en-US");
                    var dateTimeFormat = "MMM d, yyyy";
                    var startDate = spawnEvent.StartDate?.ToString(dateTimeFormat, dateTimeCulture) ?? "???";
                    var endDate = spawnEvent.EndDate?.ToString(dateTimeFormat, dateTimeCulture) ?? "???";
                    spawnReadModel.EventDateRange = startDate + " - " + endDate;
                }

                spawnReadModels.Add(spawnReadModel);
            }

            return spawnReadModels;
        }

        private string GetTimesAsString(TimeOfDay timeOfDay)
        {
            if (timeOfDay.Name.Equals(TimeOfDay.ANY))
            {
                return "";
            }

            var timeStrings = new List<string>();
            foreach (var seasonTime in timeOfDay.SeasonTimes.OrderBy(st => st.Season.SortIndex))
            {
                timeStrings.Add(
                    $"{seasonTime.Season.Name}: " +
                    $"{GetTimeName(seasonTime.StartHour)} - " +
                    $"{GetTimeName(seasonTime.EndHour)}");
            }

            return string.Join("\n", timeStrings);
        }

        private string GetTimeName(int hour)
        {
            if (hour == 0 || hour == 24)
            {
                return "12am";
            }

            if (hour < 12)
            {
                return $"{hour}am";
            }

            if (hour == 12)
            {
                return "12 pm";
            }

            return $"{hour - 12}pm";
        }

        private string GetRarityAsString(SpawnOpportunity spawnOpportunity)
        {
            if (spawnOpportunity.SpawnProbability != null)
            {
                return $"{spawnOpportunity.SpawnProbability*100M:###.##}%";
            }

            return spawnOpportunity.SpawnCommonality ?? "?";
        }

        private decimal GetRarityValue(SpawnOpportunity spawnOpportunity)
        {
            if (spawnOpportunity.SpawnProbability != null)
            {
                return (decimal)spawnOpportunity.SpawnProbability;
            }

            switch (spawnOpportunity.SpawnCommonality.ToUpper())
            {
                case "COMMON": return 0.5M;
                case "UNCOMMON": return 0.1M;
                case "RARE": return 0.05M;
                case "VERY RARE": return 0.01M;
            }

            return 0M;
        }

        private List<EvolutionReadModel> GetEvolutions(PokemonVariety variety)
        {
            var evolutionBaseSpecies = GetEvolutionBaseSpecies(variety);
            var evolutions = _dbContext.Evolutions
                .Include(e => e.BasePokemonVariety.DefaultForm)
                .Include(e => e.BasePokemonVariety.PrimaryType)
                .Include(e => e.BasePokemonVariety.SecondaryType)
                .Include(e => e.EvolvedPokemonVariety.DefaultForm)
                .Include(e => e.EvolvedPokemonVariety.PrimaryType)
                .Include(e => e.EvolvedPokemonVariety.SecondaryType)
                .Where(e => e.BasePokemonSpeciesId == evolutionBaseSpecies);

            return evolutions
                .Select(e => new EvolutionReadModel
                    {
                        BaseName = e.BasePokemonVariety.Name,
                        BaseResourceName = e.BasePokemonVariety.ResourceName,
                        BaseSpriteName = e.BasePokemonVariety.DefaultForm.SpriteName,
                        BaseType1 = e.BasePokemonVariety.PrimaryType.Name,
                        BaseType2 = e.BasePokemonVariety.SecondaryType.Name,
                        BaseSortIndex = e.BasePokemonVariety.DefaultForm.SortIndex,
                        BaseStage = e.BaseStage,
                        EvolvedName = e.EvolvedPokemonVariety.Name,
                        EvolvedResourceName = e.EvolvedPokemonVariety.ResourceName,
                        EvolvedSpriteName = e.EvolvedPokemonVariety.DefaultForm.SpriteName,
                        EvolvedType1 = e.EvolvedPokemonVariety.PrimaryType.Name,
                        EvolvedType2 = e.EvolvedPokemonVariety.SecondaryType.Name,
                        EvolvedSortIndex = e.EvolvedPokemonVariety.DefaultForm.SortIndex,
                        EvolvedStage = e.EvolvedStage,
                        EvolutionTrigger = e.EvolutionTrigger,
                        IsReversible = e.IsReversible,
                        IsAvailable = e.IsAvailable
                    })
                .ToList();
        }

        private int GetEvolutionBaseSpecies(PokemonVariety variety)
        {
            var anyEvolution = _dbContext.Evolutions
                .AsNoTracking()
                .FirstOrDefault(e =>
                    e.BasePokemonVarietyId.Equals(variety.Id) ||
                    e.EvolvedPokemonVarietyId.Equals(variety.Id));

            return anyEvolution?.BasePokemonSpeciesId ?? variety.PokemonSpecies.Id;
        }

        private List<LearnableMoveReadModel> GetLearnableMoves(PokemonVariety variety)
        {
            return variety.LearnableMoves.Select(learnableMove => new LearnableMoveReadModel
                {
                    Name = learnableMove.Move.Name,
                    IsAvailable = learnableMove.LearnMethods.Any(lm => lm.IsAvailable),
                    Type = learnableMove.Move.ElementalType.Name,
                    DamageClass = learnableMove.Move.DamageClass.Name,
                    AttackPower = learnableMove.Move.AttackPower,
                    Accuracy = learnableMove.Move.Accuracy,
                    PowerPoints = learnableMove.Move.PowerPoints,
                    EffectDescription = learnableMove.Move.Effect,
                    LearnMethods = learnableMove.LearnMethods.Select(GetLearnMethod).ToList()
                })
                .ToList();
        }

        private LearnMethodReadModel GetLearnMethod(LearnableMoveLearnMethod learnMethod)
        {
            var readModel = new LearnMethodReadModel
            {
                IsAvailable = learnMethod.IsAvailable,
            };

            switch (learnMethod.MoveLearnMethod.Name)
            {
                case LearnableMoveConstants.LearnMethodName.EGG:
                    readModel.LearnMethodName = learnMethod.MoveLearnMethod.Name;
                    readModel.Description = string.Join(", ",
                        learnMethod.MoveLearnMethod.Locations
                            .OrderBy(l => l.Location.SortIndex)
                            .Select(l => $"{l.Location.Name} ({l.NpcName})"));
                    readModel.Price = GetPriceString(learnMethod.Price.Select(p => p.Price));
                    readModel.SortIndex = 3;
                    break;

                case LearnableMoveConstants.LearnMethodName.TUTOR:
                    readModel.LearnMethodName = learnMethod.MoveLearnMethod.Locations.FirstOrDefault()?.NpcName ??
                                                learnMethod.MoveLearnMethod.Name + " (unavailable)";
                    readModel.Description = string.Join(", ",
                        learnMethod.MoveLearnMethod.Locations
                            .OrderBy(l => l.Location.SortIndex)
                            .Select(l => $"{l.Location.Name}"));
                    readModel.Price = GetPriceString(learnMethod.Price.Select(p => p.Price));
                    readModel.SortIndex = 2;
                    break;

                case LearnableMoveConstants.LearnMethodName.PREEVOLUTION:
                    readModel.LearnMethodName = learnMethod.MoveLearnMethod.Name;
                    readModel.Description = string.Join(", ",
                        learnMethod.MoveLearnMethod.Locations
                            .OrderBy(l => l.Location.SortIndex)
                            .Select(l => $"{l.Location.Name} ({l.NpcName})"));
                    readModel.Price = GetPriceString(learnMethod.Price.Select(p => p.Price));
                    readModel.SortIndex = 4;
                    break;

                case LearnableMoveConstants.LearnMethodName.LEVELUP:
                    readModel.LearnMethodName = $"Level {learnMethod.LevelLearnedAt} / Move Reminder";
                    readModel.Description = string.Join(", ",
                        learnMethod.MoveLearnMethod.Locations
                            .OrderBy(l => l.Location.SortIndex)
                            .Select(l => $"{l.Location.Name} ({l.NpcName})"));
                    readModel.Price = GetPriceString(learnMethod.Price.Select(p => p.Price));
                    readModel.SortIndex = 0;
                    break;

                case LearnableMoveConstants.LearnMethodName.MACHINE:
                    readModel.LearnMethodName = learnMethod.RequiredItem?.Name ?? "TM/HM (unavailable)";
                    readModel.Description = "";
                    readModel.Price = "";
                    readModel.SortIndex = 1;
                    break;
            }

            return readModel;
        }

        private string GetPriceString(IEnumerable<CurrencyAmount> priceList)
        {
            return string.Join(", ", priceList.Select(ca => $"{ca.Amount:#####} {ca.Currency.Item.Name}"));
        }

        private List<BuildReadModel> GetBuilds(PokemonVariety variety)
        {
            return variety.Builds.Select(build => new BuildReadModel
                {
                    PokemonResourceName = variety.ResourceName,
                    PokemonName = variety.Name,
                    BuildName = build.Name,
                    BuildDescription = build.Description,
                    Move1 = GetBuildMoveString(build.Moves, 1),
                    Move2 = GetBuildMoveString(build.Moves, 2),
                    Move3 = GetBuildMoveString(build.Moves, 3),
                    Move4 = GetBuildMoveString(build.Moves, 4),
                    ItemOptions = GetBuildItemOptions(build.Item),
                    NatureOptions = GetBuildNatureOptions(build.Nature),
                    Ability = build.Ability.Name,
                    AbilityDescription = build.Ability.EffectDescription,
                    AtkEv = (int)build.EvDistribution.Attack,
                    SpaEv = (int)build.EvDistribution.SpecialAttack,
                    DefEv = (int)build.EvDistribution.Defense,
                    SpdEv = (int)build.EvDistribution.SpecialDefense,
                    SpeEv = (int)build.EvDistribution.Speed,
                    HpEv = (int)build.EvDistribution.HitPoints
                })
                .ToList();
        }

        private string GetBuildMoveString(List<MoveOption> moves, int slot)
        {
            return string.Join(" / ", moves
                .Where(mo => mo.Slot == slot)
                .Select(mo => mo.Move.Name));
        }

        private List<ItemOptionReadModel> GetBuildItemOptions(List<ItemOption> itemOptions)
        {
            return itemOptions.Select(io => new ItemOptionReadModel
            {
                ItemResourceName = io.Item.ResourceName,
                ItemName = io.Item.Name
            }).ToList();
        }

        private List<NatureOptionReadModel> GetBuildNatureOptions(List<NatureOption> natureOptions)
        {
            return natureOptions.Select(no => new NatureOptionReadModel
            {
                NatureName = no.Nature.Name,
                NatureEffect = CommonFormatHelper.GetNatureEffect(no.Nature)
            }).ToList();
        }
    }
}
