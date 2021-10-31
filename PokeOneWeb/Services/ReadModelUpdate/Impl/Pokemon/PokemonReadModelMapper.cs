using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Extensions;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.Pokemon
{
    public class PokemonReadModelMapper : IReadModelMapper<PokemonVarietyReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        private List<ElementalTypeRelation> _elementalTypeRelations;
        private List<ElementalType> _elementalTypes;
        private List<Evolution> _evolutions;

        public PokemonReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PokemonVarietyReadModel> MapFromDatabase()
        {
            var varietyIds = _dbContext.PokemonVarieties
                .Where(v => v.DoInclude)
                .Include(v => v.DefaultForm)
                .AsNoTracking()
                .OrderBy(v => v.DefaultForm.SortIndex)
                .Select(v => v.Id)
                .ToList();

            _elementalTypeRelations = _dbContext.ElementalTypeRelations
                .AsNoTracking()
                .ToList();

            _elementalTypes = _dbContext.ElementalTypes
                .AsNoTracking()
                .ToList();

            _evolutions = _dbContext.Evolutions
                .Include(e => e.BasePokemonVariety.DefaultForm)
                .Include(e => e.BasePokemonVariety.PrimaryAbility)
                .Include(e => e.BasePokemonVariety.SecondaryAbility)
                .Include(e => e.BasePokemonVariety.HiddenAbility)
                .Include(e => e.BasePokemonVariety.PrimaryType)
                .Include(e => e.BasePokemonVariety.SecondaryType)
                .Include(e => e.EvolvedPokemonVariety.DefaultForm)
                .Include(e => e.EvolvedPokemonVariety.PrimaryAbility)
                .Include(e => e.EvolvedPokemonVariety.SecondaryAbility)
                .Include(e => e.EvolvedPokemonVariety.HiddenAbility)
                .Include(e => e.EvolvedPokemonVariety.PrimaryType)
                .Include(e => e.EvolvedPokemonVariety.SecondaryType)
                .Include(e => e.BasePokemonSpecies)
                .AsNoTracking()
                .ToList();

            for (var i = 0; i < varietyIds.Count; i++)
            {
                var varietyId = varietyIds[i];

                var variety = LoadVariety(varietyId);
                
                var readModel = GetBasicReadModel(variety);

                AttachVarieties(readModel, variety);
                AttachForms(readModel, variety);

                AttachEvolutionAbilities(readModel, variety);

                int previousId = i != 0 ? varietyIds[i - 1] : varietyIds[^1];
                int nextId = i != varietyIds.Count - 1 ? varietyIds[i + 1] : varietyIds[0];
                AttachPreviousAndNext(readModel, previousId, nextId);

                readModel.DefenseAttackEffectivities = GetAttackEffectivityReadModels(variety);

                var allVarietiesOfEvolutionLine = GetVarietiesOfEvolutionLine(variety);
                readModel.Spawns = GetSpawnReadModels(allVarietiesOfEvolutionLine);
                readModel.Evolutions = GetEvolutions(variety);

                readModel.LearnableMoves = GetLearnableMoves(variety);
                readModel.HuntingConfigurations = GetHuntingConfigurations(allVarietiesOfEvolutionLine);

                readModel.Builds = GetBuilds(variety);

                readModel.Urls = GetUrls(variety);

                yield return readModel;
            }
        }

        private PokemonVariety LoadVariety(int varietyId)
        {
            return _dbContext.PokemonVarieties
                .Where(v => v.Id == varietyId)
                .Include(v => v.PokemonSpecies.Varieties)
                .ThenInclude(v => v.PrimaryType)
                .Include(v => v.PokemonSpecies.Varieties)
                .ThenInclude(v => v.SecondaryType)
                .Include(v => v.PokemonSpecies.Varieties)
                .ThenInclude(v => v.DefaultForm.Availability)
                .Include(v => v.Forms)
                .ThenInclude(f => f.Availability)
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
                .ThenInclude(lmlm => lmlm.MoveTutorMove.MoveTutor.Location.LocationGroup.Region.Event)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveTutorMove.Move.ElementalType)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveTutorMove.Move.DamageClass)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveTutorMove.Price)
                .ThenInclude(mtmp => mtmp.CurrencyAmount.Currency.Item)
                .Include(v => v.LearnableMoves)
                .ThenInclude(lm => lm.LearnMethods)
                .ThenInclude(lmlm => lmlm.MoveLearnMethod.Locations)
                .ThenInclude(mlml => mlml.Location.LocationGroup.Region.Event)
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
                .Include(v => v.HuntingConfigurations)
                .ThenInclude(hc => hc.Ability)
                .Include(v => v.HuntingConfigurations)
                .ThenInclude(hc => hc.Nature)
                .Include(v => v.Urls)
                .Single();
        }

        private PokemonVarietyReadModel GetBasicReadModel(PokemonVariety variety)
        {
            return new()
            {
                ApplicationDbId = variety.Id,

                ResourceName = variety.ResourceName,
                SortIndex = variety.DefaultForm.SortIndex,
                PokedexNumber = variety.PokemonSpecies.PokedexNumber,
                Name = variety.Name,

                SpriteName = variety.DefaultForm.SpriteName,

                PrimaryElementalType = variety.PrimaryType.Name,
                SecondaryElementalType = variety.SecondaryType?.Name,

                Attack = variety.Attack,
                SpecialAttack = variety.SpecialAttack,
                Defense = variety.Defense,
                SpecialDefense = variety.SpecialDefense,
                Speed = variety.Speed,
                HitPoints = variety.HitPoints,

                PrimaryAbility = variety.PrimaryAbility?.Name,
                PrimaryAbilityEffect = variety.PrimaryAbility?.EffectDescription,
                SecondaryAbility = variety.SecondaryAbility?.Name,
                SecondaryAbilityEffect = variety.SecondaryAbility?.EffectDescription,
                HiddenAbility = variety.HiddenAbility?.Name,
                HiddenAbilityEffect = variety.HiddenAbility?.EffectDescription,

                Availability = variety.DefaultForm.Availability.Name,
                AvailabilityDescription = variety.DefaultForm.Availability.Description,

                PvpTier = variety.PvpTier?.Name,
                PvpTierSortIndex = variety.PvpTier?.SortIndex ?? int.MaxValue,

                Generation = variety.Generation,
                IsFullyEvolved = variety.IsFullyEvolved,
                IsMega = variety.IsMega,
                CatchRate = variety.CatchRate,
                HasGender = variety.HasGender,
                MaleRatio = variety.MaleRatio,
                FemaleRatio = 100 - variety.MaleRatio,
                EggCycles = variety.EggCycles,
                Height = variety.Height,
                Weight = variety.Weight,
                ExpYield = variety.ExpYield,

                AttackEv = variety.AttackEv,
                SpecialAttackEv = variety.SpecialAttackEv,
                DefenseEv = variety.DefenseEv,
                SpecialDefenseEv = variety.SpecialDefenseEv,
                SpeedEv = variety.SpeedEv,
                HitPointsEv = variety.HitPointsEv,

                Notes = variety.Notes,

                PrimaryAbilityAttackBoost = variety.PrimaryAbility?.AttackBoost ?? 1,
                PrimaryAbilitySpecialAttackBoost = variety.PrimaryAbility?.SpecialAttackBoost ?? 1,
                PrimaryAbilityDefenseBoost = variety.PrimaryAbility?.DefenseBoost ?? 1,
                PrimaryAbilitySpecialDefenseBoost = variety.PrimaryAbility?.SpecialDefenseBoost ?? 1,
                PrimaryAbilitySpeedBoost = variety.PrimaryAbility?.SpeedBoost ?? 1,
                PrimaryAbilityHitPointsBoost = variety.PrimaryAbility?.HitPointsBoost ?? 1,
                PrimaryAbilityBoostConditions = variety.PrimaryAbility?.BoostConditions,

                SecondaryAbilityAttackBoost = variety.SecondaryAbility?.AttackBoost ?? 1,
                SecondaryAbilitySpecialAttackBoost = variety.SecondaryAbility?.SpecialAttackBoost ?? 1,
                SecondaryAbilityDefenseBoost = variety.SecondaryAbility?.DefenseBoost ?? 1,
                SecondaryAbilitySpecialDefenseBoost = variety.SecondaryAbility?.SpecialDefenseBoost ?? 1,
                SecondaryAbilitySpeedBoost = variety.SecondaryAbility?.SpeedBoost ?? 1,
                SecondaryAbilityHitPointsBoost = variety.SecondaryAbility?.HitPointsBoost ?? 1,
                SecondaryAbilityBoostConditions = variety.SecondaryAbility?.BoostConditions,

                HiddenAbilityAttackBoost = variety.HiddenAbility?.AttackBoost ?? 1,
                HiddenAbilitySpecialAttackBoost = variety.HiddenAbility?.SpecialAttackBoost ?? 1,
                HiddenAbilityDefenseBoost = variety.HiddenAbility?.DefenseBoost ?? 1,
                HiddenAbilitySpecialDefenseBoost = variety.HiddenAbility?.SpecialDefenseBoost ?? 1,
                HiddenAbilitySpeedBoost = variety.HiddenAbility?.SpeedBoost ?? 1,
                HiddenAbilityHitPointsBoost = variety.HiddenAbility?.HitPointsBoost ?? 1,
                HiddenAbilityBoostConditions = variety.HiddenAbility?.BoostConditions,
            };
        }

        private void AttachVarieties(PokemonVarietyReadModel readModel, PokemonVariety variety)
        {
            readModel.Varieties = variety.PokemonSpecies.Varieties.Select(v => new PokemonVarietyVarietyReadModel
                {
                    ResourceName = v.ResourceName,
                    Name = v.Name,
                    SortIndex = v.DefaultForm.SortIndex,
                    SpriteName = v.DefaultForm.SpriteName,
                    Availability = v.DefaultForm.Availability.Name,
                    PrimaryType = v.PrimaryType.Name,
                    SecondaryType = v.SecondaryType?.Name
                })
                .ToList();
        }

        private void AttachForms(PokemonVarietyReadModel readModel, PokemonVariety variety)
        {
            readModel.Forms = variety.Forms.Select(f => new PokemonVarietyFormReadModel
                {
                    Name = f.Name,
                    SpriteName = f.SpriteName,
                    SortIndex = f.SortIndex,
                    Availability = f.Availability.Name
                })
                .ToList();
        }

        private void AttachEvolutionAbilities(PokemonVarietyReadModel readModel, PokemonVariety variety)
        {
            var allCoveredVarieties = new HashSet<string> { variety.ResourceName };
            var postEvolutions = new List<PokemonVariety> { variety }; 
            var relativeStageIndex = 0;
            do
            {
                postEvolutions = _evolutions
                    .Where(e => postEvolutions.Select(v => v.Id).Contains(e.BasePokemonVariety.Id))
                    .Select(e => e.EvolvedPokemonVariety)
                    .Where(v => !allCoveredVarieties.Contains(v.ResourceName))
                    .DistinctBy(v => v.ResourceName) // Required for nodes with multiple pre-evos, i.e. Ultra Necrozma
                    .ToList();

                postEvolutions.ForEach(v => allCoveredVarieties.Add(v.ResourceName));

                relativeStageIndex++;

                foreach (var postEvolution in postEvolutions)
                {
                    readModel.PrimaryEvolutionAbilities.Add(
                        GetEvolutionAbility(postEvolution, postEvolution.PrimaryAbility, relativeStageIndex)
                    );
                    readModel.SecondaryEvolutionAbilities.Add(
                        GetEvolutionAbility(postEvolution, postEvolution.SecondaryAbility ?? postEvolution.PrimaryAbility, relativeStageIndex)
                    );
                    readModel.HiddenEvolutionAbilities.Add(
                        GetEvolutionAbility(postEvolution, postEvolution.HiddenAbility ?? postEvolution.PrimaryAbility, relativeStageIndex)
                    );
                }

            } while (postEvolutions.Any());

            var preEvolutions = new List<PokemonVariety> { variety };
            relativeStageIndex = 0;
            do
            {
                preEvolutions = _evolutions
                    .Where(e => preEvolutions.Select(v => v.Id).Contains(e.EvolvedPokemonVariety.Id))
                    .Select(e => e.BasePokemonVariety)
                    .Where(v => !allCoveredVarieties.Contains(v.ResourceName))
                    .DistinctBy(v => v.ResourceName) // Required for nodes with multiple pre-evos, i.e. Ultra Necrozma
                    .ToList();

                preEvolutions.ForEach(v => allCoveredVarieties.Add(v.ResourceName));

                relativeStageIndex--;

                foreach (var preEvolution in preEvolutions)
                {
                    readModel.PrimaryEvolutionAbilities.Add(
                        GetEvolutionAbility(preEvolution, preEvolution.PrimaryAbility, relativeStageIndex)
                    );
                    readModel.SecondaryEvolutionAbilities.Add(
                        GetEvolutionAbility(preEvolution, preEvolution.SecondaryAbility ?? preEvolution.PrimaryAbility, relativeStageIndex)
                    );
                    readModel.HiddenEvolutionAbilities.Add(
                        GetEvolutionAbility(preEvolution, preEvolution.HiddenAbility ?? preEvolution.PrimaryAbility, relativeStageIndex)
                    );
                }

            } while (preEvolutions.Any());
        }

        public void AttachPreviousAndNext(PokemonVarietyReadModel readModel, int previousId, int nextId)
        {
            var previous = _dbContext.PokemonVarieties
                .Include(v => v.DefaultForm)
                .AsNoTracking()
                .Single(v => v.Id == previousId);

            var next = _dbContext.PokemonVarieties
                .Include(v => v.DefaultForm)
                .AsNoTracking()
                .Single(v => v.Id == nextId);

            readModel.PreviousPokemonResourceName = previous.ResourceName;
            readModel.PreviousPokemonSpriteName = previous.DefaultForm.SpriteName;
            readModel.PreviousPokemonName = previous.Name;

            readModel.NextPokemonResourceName = next.ResourceName;
            readModel.NextPokemonSpriteName = next.DefaultForm.SpriteName;
            readModel.NextPokemonName = next.Name;
        }

        private EvolutionAbilityReadModel GetEvolutionAbility(PokemonVariety variety, Ability ability,
            int relativeStageIndex)
        {
            return new()
            {
                RelativeStageIndex = relativeStageIndex,
                PokemonResourceName = variety.ResourceName,
                PokemonSortIndex = variety.DefaultForm.SortIndex,
                PokemonName = variety.Name,
                SpriteName = variety.DefaultForm.SpriteName,
                AbilityName = ability.Name
            };
        }

        private List<int> GetVarietiesOfEvolutionLine(PokemonVariety variety)
        {
            var allEvolutions = _dbContext.Evolutions.AsNoTracking();
            var varietyIds = new List<int>();

            varietyIds.Add(variety.Id);

            bool hasFoundNewEvolutions;

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
                .Include(hc => hc.Nature)
                .Include(hc => hc.PokemonVariety.DefaultForm)
                .AsNoTracking()
                .Where(hc => varietyIds.Contains(hc.PokemonVarietyId))
                .OrderBy(hc => hc.PokemonVariety.DefaultForm.SortIndex)
                .ToList()
                .Select(hc => new HuntingConfigurationReadModel 
                    {
                        ApplicationDbId = hc.Id,
                        PokemonResourceName = hc.PokemonVariety.ResourceName,
                        PokemonName = hc.PokemonVariety.Name,
                        Nature = hc.Nature.Name,
                        NatureEffect = hc.Nature.GetDescription(),
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
                .Where(f => varietyIds.Contains(f.PokemonVarietyId))
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
                .ToList()
                .SelectMany(f => f.PokemonSpawns)
                .Select(GetSpawnReadModel)
                .ToList();
        }

        private SpawnReadModel GetSpawnReadModel(Spawn spawn)
        {
            var rarityString = GetRarityAsString(spawn);
            var rarityValue = GetRarityValue(spawn);

            var spawnReadModel = new SpawnReadModel
            {
                ApplicationDbId = spawn.Id,
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
                LowestLevel = spawn.LowestLevel,
                HighestLevel = spawn.HighestLevel,
                TimesOfDay = new List<TimeOfDayReadModel>(),
                Seasons = new List<SeasonReadModel>(),
                RarityString = rarityString,
                RarityValue = rarityValue,
                Notes = spawn.Notes
            };

            var spawnEvent = spawn.Location.LocationGroup.Region.Event;

            if (spawnEvent != null)
            {
                var dateTimeCulture = CultureInfo.CreateSpecificCulture("en-US");
                var dateTimeFormat = "MMM d, yyyy";
                var startDate = spawnEvent.StartDate?.ToString(dateTimeFormat, dateTimeCulture);
                var endDate = spawnEvent.EndDate?.ToString(dateTimeFormat, dateTimeCulture);
                spawnReadModel.EventStartDate = startDate;
                spawnReadModel.EventEndDate = endDate;
            }

            foreach (var spawnOpportunity in spawn.SpawnOpportunities)
            {
                if (!spawnReadModel.TimesOfDay.Any(t => t.Name.EqualsExact(spawnOpportunity.TimeOfDay.Name)))
                {
                    var time = new TimeOfDayReadModel
                    {
                        SortIndex = spawnOpportunity.TimeOfDay.SortIndex,
                        Name = spawnOpportunity.TimeOfDay.Name,
                        Abbreviation = spawnOpportunity.TimeOfDay.Abbreviation,
                        Color = spawnOpportunity.TimeOfDay.Color,
                        Times = GetTimesAsString(spawnOpportunity.TimeOfDay)
                    };

                    spawnReadModel.TimesOfDay.Add(time);
                }

                if (!spawnReadModel.Seasons.Any(s => s.Name.EqualsExact(spawnOpportunity.Season.Name)))
                {
                    var season = new SeasonReadModel
                    {
                        SortIndex = spawnOpportunity.Season.SortIndex,
                        Name = spawnOpportunity.Season.Name,
                        Abbreviation = spawnOpportunity.Season.Abbreviation,
                        Color = spawnOpportunity.Season.Color
                    };

                    spawnReadModel.Seasons.Add(season);
                }
            }

            return spawnReadModel;
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
                return "12pm";
            }

            return $"{hour - 12}pm";
        }

        private string GetRarityAsString(Spawn spawn)
        {
            if (spawn.SpawnProbability != null)
            {
                return $"{spawn.SpawnProbability * 100M:###.##}%";
            }

            return spawn.SpawnCommonality ?? "?";
        }

        private decimal GetRarityValue(Spawn spawn)
        {
            if (spawn.SpawnProbability != null)
            {
                return (decimal)spawn.SpawnProbability;
            }

            switch (spawn.SpawnCommonality?.ToUpper())
            {
                case "COMMON": return 0.5M;
                case "UNCOMMON": return 0.15M;
                case "RARE": return 0.05M;
                case "VERY RARE": return 0.01M;
            }

            return 0M;
        }

        private List<EvolutionReadModel> GetEvolutions(PokemonVariety variety)
        {
            var evolutionBaseSpecies = GetEvolutionBaseSpecies(variety);
            var evolutions = _evolutions
                .Where(e => e.BasePokemonSpeciesId == evolutionBaseSpecies);

            return evolutions
                .Select(e => new EvolutionReadModel
                    {
                        ApplicationDbId = e.Id,
                        BaseName = e.BasePokemonVariety.Name,
                        BaseResourceName = e.BasePokemonVariety.ResourceName,
                        BaseSpriteName = e.BasePokemonVariety.DefaultForm.SpriteName,
                        BasePrimaryElementalType = e.BasePokemonVariety.PrimaryType.Name,
                        BaseSecondaryElementalType = e.BasePokemonVariety.SecondaryType?.Name,
                        BaseSortIndex = e.BasePokemonVariety.DefaultForm.SortIndex,
                        BaseStage = e.BaseStage,
                        EvolvedName = e.EvolvedPokemonVariety.Name,
                        EvolvedResourceName = e.EvolvedPokemonVariety.ResourceName,
                        EvolvedSpriteName = e.EvolvedPokemonVariety.DefaultForm.SpriteName,
                        EvolvedPrimaryElementalType = e.EvolvedPokemonVariety.PrimaryType.Name,
                        EvolvedSecondaryElementalType = e.EvolvedPokemonVariety.SecondaryType?.Name,
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
            var anyEvolution = _evolutions
                .FirstOrDefault(e =>
                    e.BasePokemonVarietyId.Equals(variety.Id) ||
                    e.EvolvedPokemonVarietyId.Equals(variety.Id));

            return anyEvolution?.BasePokemonSpeciesId ?? variety.PokemonSpecies.Id;
        }

        private List<LearnableMoveReadModel> GetLearnableMoves(PokemonVariety variety)
        {
            return variety.LearnableMoves.Select(learnableMove =>
                {
                    var hasStab = learnableMove.Move.ElementalType.Name.EqualsExact(variety.PrimaryType.Name) ||
                                  learnableMove.Move.ElementalType.Name.EqualsExact(variety.SecondaryType?.Name);

                    return new LearnableMoveReadModel
                    {
                        ApplicationDbId = learnableMove.Id,
                        MoveName = learnableMove.Move.Name,
                        IsAvailable = learnableMove.LearnMethods.Any(lm => lm.IsAvailable),
                        ElementalType = learnableMove.Move.ElementalType.Name,
                        DamageClass = learnableMove.Move.DamageClass.Name,
                        AttackPower = learnableMove.Move.AttackPower,
                        EffectivePower = hasStab ? (int)(learnableMove.Move.AttackPower * 1.5) : learnableMove.Move.AttackPower,
                        HasStab = hasStab,
                        Accuracy = learnableMove.Move.Accuracy,
                        PowerPoints = learnableMove.Move.PowerPoints,
                        EffectDescription = learnableMove.Move.Effect,
                        LearnMethods = learnableMove.LearnMethods.Select(GetLearnMethod).ToList()
                    };
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
                    if (learnMethod.MoveTutorMove != null)
                    {
                        readModel.Description =
                            $"{learnMethod.MoveTutorMove?.MoveTutor?.Location?.Name}, " +
                            $"{GetPriceString(learnMethod.MoveTutorMove?.Price.Select(p => p.CurrencyAmount))}";
                    }
                    else
                    {
                        readModel.Description = "";
                    }
                    
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
                    ApplicationDbId = build.Id,
                    PokemonResourceName = variety.ResourceName,
                    PokemonName = variety.Name,
                    BuildName = build.Name,
                    BuildDescription = build.Description,
                    MoveOptions = GetBuildMoveOptions(build.MoveOptions),
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

        private List<MoveOptionReadModel> GetBuildMoveOptions(List<MoveOption> moveOptions)
        {
            return moveOptions.Select(mo => new MoveOptionReadModel
            {
                ApplicationDbId = mo.Id,
                Slot = mo.Slot,
                MoveName = mo.Move.Name,
                ElementalType = mo.Move.ElementalType.Name,
                DamageClass = mo.Move.DamageClass.Name,
                AttackPower = mo.Move.AttackPower,
                Accuracy = mo.Move.Accuracy,
                PowerPoints = mo.Move.PowerPoints,
                Priority = mo.Move.Priority,
                EffectDescription = mo.Move.Effect
            }).ToList();
        }

        private List<ItemOptionReadModel> GetBuildItemOptions(List<ItemOption> itemOptions)
        {
            return itemOptions.Select(io => new ItemOptionReadModel
            {
                ApplicationDbId = io.Id,
                ItemResourceName = io.Item.ResourceName,
                ItemName = io.Item.Name
            }).ToList();
        }

        private List<NatureOptionReadModel> GetBuildNatureOptions(List<NatureOption> natureOptions)
        {
            return natureOptions.Select(no => new NatureOptionReadModel
            {
                ApplicationDbId = no.Id,
                NatureName = no.Nature.Name,
                NatureEffect = no.Nature.GetDescription()
            }).ToList();
        }

        private List<PokemonVarietyUrlReadModel> GetUrls(PokemonVariety variety)
        {
            return variety.Urls.Select(u => new PokemonVarietyUrlReadModel
            {
                ApplicationDbId = u.Id,
                Name = u.Name,
                Url = u.Url
            }).ToList();
        }
    }
}
