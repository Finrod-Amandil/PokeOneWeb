using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.ReadModels;
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
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.Move.DamageClass)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.Move.ElementalType)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.RequiredItem)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveTutorMove.MoveTutor.Location)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveLearnMethod.Locations)
                .ThenInclude(mlml => mlml.Location.LocationGroup.Region)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveLearnMethod.Locations)
                .ThenInclude(mlml => mlml.Price)
                .ThenInclude(mlmlp => mlmlp.CurrencyAmount.Currency.Item)
                .Include(v => v.Builds)
                .ThenInclude(b => b.Ability)
                .Include(v => v.Builds)
                .ThenInclude(b => b.NatureOptions)
                .ThenInclude(no => no.Nature)
                .Include(v => v.Builds)
                .ThenInclude(b => b.ItemOptions)
                .ThenInclude(io => io.Item)
                .Include(v => v.Builds)
                .ThenInclude(b => b.MoveOptions)
                .ThenInclude(mo => mo.Move.DamageClass)
                .Include(v => v.Builds)
                .ThenInclude(b => b.MoveOptions)
                .ThenInclude(mo => mo.Move.ElementalType)
                .Include(v => v.Urls)
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

                Atk = variety.Attack,
                Spa = variety.SpecialAttack,
                Def = variety.Defense,
                Spd = variety.SpecialDefense,
                Spe = variety.Speed,
                Hp = variety.HitPoints,

                AtkEv = variety.AttackEv,
                SpaEv = variety.SpecialAttackEv,
                DefEv = variety.DefenseEv,
                SpdEv = variety.SpecialDefenseEv,
                SpeEv = variety.SpeedEv,
                HpEv = variety.HitPointsEv,

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

                //TODO: Urls

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

            var rarityString = GetRarityAsString(spawn);
            var rarityValue = GetRarityValue(spawn);

            foreach (var spawnOpportunity in spawn.SpawnOpportunities)
            {
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

        private string GetRarityAsString(Spawn spawn)
        {
            if (spawn.SpawnProbability != null)
            {
                return $"{spawn.SpawnProbability*100M:###.##}%";
            }

            return spawn.SpawnCommonality ?? "?";
        }

        private decimal GetRarityValue(Spawn spawn)
        {
            if (spawn.SpawnProbability != null)
            {
                return (decimal)spawn.SpawnProbability;
            }

            switch (spawn.SpawnCommonality.ToUpper())
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
                case "Egg":
                    readModel.LearnMethodName = learnMethod.MoveLearnMethod.Name;
                    readModel.Description = string.Join(", ",
                        learnMethod.MoveLearnMethod.Locations
                            .OrderBy(l => l.Location.SortIndex)
                            .Select(l => 
                                $"{l.Location.Name} ({l.NpcName}), " +
                                $"{GetPriceString(l.Price.Select(p => p.CurrencyAmount))}"));
                    readModel.SortIndex = 3;
                    break;

                case "Tutor":
                    readModel.LearnMethodName = learnMethod.MoveTutorMove?.MoveTutor?.Name ??
                                                learnMethod.MoveLearnMethod.Name + " (unavailable)";
                    readModel.Description =
                        $"{learnMethod.MoveTutorMove?.MoveTutor?.Location?.Name}, " +
                        $"{GetPriceString(learnMethod.MoveTutorMove?.Price.Select(p => p.CurrencyAmount))}";
                    readModel.SortIndex = 2;
                    break;

                case "Pre-Evolution move tutor":
                    readModel.LearnMethodName = learnMethod.MoveLearnMethod.Name;
                    readModel.Description = string.Join(", ",
                        learnMethod.MoveLearnMethod.Locations
                            .OrderBy(l => l.Location.SortIndex)
                            .Select(l => 
                                $"{l.Location.Name} ({l.NpcName}), " +
                                $"{GetPriceString(l.Price.Select(p => p.CurrencyAmount))}"));
                    readModel.SortIndex = 4;
                    break;

                case "Level up":
                    readModel.LearnMethodName = $"Level {learnMethod.LevelLearnedAt} / Move Reminder";
                    readModel.Description = string.Join(", ",
                        learnMethod.MoveLearnMethod.Locations
                            .OrderBy(l => l.Location.SortIndex)
                            .Select(l => 
                                $"{l.Location.Name} ({l.NpcName}), " +
                                $"{GetPriceString(l.Price.Select(p => p.CurrencyAmount))}"));
                    readModel.SortIndex = 0;
                    break;

                case "Machine":
                    readModel.LearnMethodName = learnMethod.RequiredItem?.Name ?? "TM/HM (unavailable)";
                    readModel.Description = "";
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
                    Move1 = GetBuildMoveString(build.MoveOptions, 1),
                    Move2 = GetBuildMoveString(build.MoveOptions, 2),
                    Move3 = GetBuildMoveString(build.MoveOptions, 3),
                    Move4 = GetBuildMoveString(build.MoveOptions, 4),
                    ItemOptions = GetBuildItemOptions(build.ItemOptions),
                    NatureOptions = GetBuildNatureOptions(build.NatureOptions),
                    Ability = build.Ability.Name,
                    AbilityDescription = build.Ability.EffectDescription,
                    AtkEv = build.AttackEv,
                    SpaEv = build.SpecialAttackEv,
                    DefEv = build.DefenseEv,
                    SpdEv = build.SpecialDefenseEv,
                    SpeEv = build.SpeedEv,
                    HpEv = build.HitPointsEv
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
