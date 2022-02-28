using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Shared.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.Pokemon
{
    public class PokemonVarietyReadModelRepository : IReadModelRepository<PokemonVarietyReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public PokemonVarietyReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<PokemonVarietyReadModel, DbAction> entities)
        {
            foreach (var entity in entities.Keys)
            {
                var existingEntity =
                    _dbContext.PokemonVarietyReadModels
                        .Where(e  => e.ApplicationDbId == entity.ApplicationDbId)
                        .IncludeOptimized(e => e.Varieties)
                        .IncludeOptimized(e => e.Forms)
                        .IncludeOptimized(e => e.Urls)
                        .IncludeOptimized(e => e.PrimaryEvolutionAbilities)
                        .IncludeOptimized(e => e.SecondaryEvolutionAbilities)
                        .IncludeOptimized(e => e.HiddenEvolutionAbilities)
                        .IncludeOptimized(e => e.HuntingConfigurations)
                        .IncludeOptimized(e => e.DefenseAttackEffectivities)
                        .IncludeOptimized(e => e.Spawns)
                        .IncludeOptimized(e => e.Spawns.Select(s => s.Seasons))
                        .IncludeOptimized(e => e.Spawns.Select(s => s.TimesOfDay))
                        .IncludeOptimized(e => e.Evolutions)
                        .IncludeOptimized(e => e.LearnableMoves)
                        .IncludeOptimized(e => e.LearnableMoves.Select(lm => lm.LearnMethods))
                        .IncludeOptimized(e => e.Builds)
                        .IncludeOptimized(e => e.Builds.Select(b => b.MoveOptions))
                        .IncludeOptimized(e => e.Builds.Select(b => b.ItemOptions))
                        .IncludeOptimized(e => e.Builds.Select(b => b.NatureOptions))
                        .IncludeOptimized(e => e.Builds.Select(b => b.OffensiveCoverage))
                        .AsSingleQuery()
                        .SingleOrDefault();

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.PokemonVarietyReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private void UpdateExistingEntity(PokemonVarietyReadModel existingEntity, PokemonVarietyReadModel updatedEntity)
        {
            existingEntity.ApplicationDbId = updatedEntity.ApplicationDbId;
            existingEntity.ResourceName = updatedEntity.ResourceName;
            existingEntity.SortIndex = updatedEntity.SortIndex;
            existingEntity.PokedexNumber = updatedEntity.PokedexNumber;
            existingEntity.Name = updatedEntity.Name;
            existingEntity.SpriteName = updatedEntity.SpriteName;
            existingEntity.PrimaryElementalType = updatedEntity.PrimaryElementalType;
            existingEntity.SecondaryElementalType = updatedEntity.SecondaryElementalType;
            existingEntity.Attack = updatedEntity.Attack;
            existingEntity.SpecialAttack = updatedEntity.SpecialAttack;
            existingEntity.Defense = updatedEntity.Defense;
            existingEntity.SpecialDefense = updatedEntity.SpecialDefense;
            existingEntity.Speed = updatedEntity.Speed;
            existingEntity.HitPoints = updatedEntity.HitPoints;
            existingEntity.PrimaryAbility = updatedEntity.PrimaryAbility;
            existingEntity.PrimaryAbilityEffect = updatedEntity.PrimaryAbilityEffect;
            existingEntity.SecondaryAbility = updatedEntity.SecondaryAbility;
            existingEntity.SecondaryAbilityEffect = updatedEntity.SecondaryAbilityEffect;
            existingEntity.HiddenAbility = existingEntity.HiddenAbility;
            existingEntity.HiddenAbilityEffect = existingEntity.HiddenAbilityEffect;
            existingEntity.Availability = updatedEntity.Availability;
            existingEntity.PvpTier = updatedEntity.PvpTier;
            existingEntity.PvpTierSortIndex = updatedEntity.PvpTierSortIndex;
            existingEntity.Generation = updatedEntity.Generation;
            existingEntity.IsFullyEvolved = updatedEntity.IsFullyEvolved;
            existingEntity.IsMega = updatedEntity.IsMega;
            existingEntity.CatchRate = updatedEntity.CatchRate;
            existingEntity.HasGender = updatedEntity.HasGender;
            existingEntity.MaleRatio = updatedEntity.MaleRatio;
            existingEntity.FemaleRatio = updatedEntity.FemaleRatio;
            existingEntity.EggCycles = updatedEntity.EggCycles;
            existingEntity.Height = updatedEntity.Height;
            existingEntity.Weight = updatedEntity.Weight;
            existingEntity.ExpYield = updatedEntity.ExpYield;
            existingEntity.AttackEv = updatedEntity.AttackEv;
            existingEntity.SpecialAttackEv = updatedEntity.SpecialAttackEv;
            existingEntity.DefenseEv = updatedEntity.DefenseEv;
            existingEntity.SpecialDefenseEv = updatedEntity.SpecialDefenseEv;
            existingEntity.SpeedEv = updatedEntity.SpeedEv;
            existingEntity.HitPointsEv = updatedEntity.HitPointsEv;

            existingEntity.PreviousPokemonResourceName = updatedEntity.PreviousPokemonResourceName;
            existingEntity.PreviousPokemonSpriteName = updatedEntity.PreviousPokemonSpriteName;
            existingEntity.PreviousPokemonName = updatedEntity.PreviousPokemonName;
            existingEntity.NextPokemonResourceName = updatedEntity.NextPokemonResourceName;
            existingEntity.NextPokemonSpriteName = updatedEntity.NextPokemonSpriteName;
            existingEntity.NextPokemonName = updatedEntity.NextPokemonName;

            existingEntity.PrimaryAbilityAttackBoost = updatedEntity.PrimaryAbilityAttackBoost;
            existingEntity.PrimaryAbilitySpecialAttackBoost = updatedEntity.PrimaryAbilitySpecialAttackBoost;
            existingEntity.PrimaryAbilityDefenseBoost = updatedEntity.PrimaryAbilityDefenseBoost;
            existingEntity.PrimaryAbilitySpecialDefenseBoost = updatedEntity.PrimaryAbilitySpecialDefenseBoost;
            existingEntity.PrimaryAbilitySpeedBoost = updatedEntity.PrimaryAbilitySpeedBoost;
            existingEntity.PrimaryAbilityHitPointsBoost = updatedEntity.PrimaryAbilityHitPointsBoost;
            existingEntity.PrimaryAbilityBoostConditions = updatedEntity.PrimaryAbilityBoostConditions;

            existingEntity.SecondaryAbilityAttackBoost = updatedEntity.SecondaryAbilityAttackBoost;
            existingEntity.SecondaryAbilitySpecialAttackBoost = updatedEntity.SecondaryAbilitySpecialAttackBoost;
            existingEntity.SecondaryAbilityDefenseBoost = updatedEntity.SecondaryAbilityDefenseBoost;
            existingEntity.SecondaryAbilitySpecialDefenseBoost = updatedEntity.SecondaryAbilitySpecialDefenseBoost;
            existingEntity.SecondaryAbilitySpeedBoost = updatedEntity.SecondaryAbilitySpeedBoost;
            existingEntity.SecondaryAbilityHitPointsBoost = updatedEntity.SecondaryAbilityHitPointsBoost;
            existingEntity.SecondaryAbilityBoostConditions = updatedEntity.SecondaryAbilityBoostConditions;

            existingEntity.HiddenAbilityAttackBoost = updatedEntity.HiddenAbilityAttackBoost;
            existingEntity.HiddenAbilitySpecialAttackBoost = updatedEntity.HiddenAbilitySpecialAttackBoost;
            existingEntity.HiddenAbilityDefenseBoost = updatedEntity.HiddenAbilityDefenseBoost;
            existingEntity.HiddenAbilitySpecialDefenseBoost = updatedEntity.HiddenAbilitySpecialDefenseBoost;
            existingEntity.HiddenAbilitySpeedBoost = updatedEntity.HiddenAbilitySpeedBoost;
            existingEntity.HiddenAbilityHitPointsBoost = updatedEntity.HiddenAbilityHitPointsBoost;
            existingEntity.HiddenAbilityBoostConditions = updatedEntity.HiddenAbilityBoostConditions;

            UpdateVarieties(existingEntity.Varieties, updatedEntity.Varieties);
            UpdateForms(existingEntity.Forms, updatedEntity.Forms);
            UpdateUrls(existingEntity.Urls, updatedEntity.Urls);
            UpdateEvolutionAbilities(existingEntity.PrimaryEvolutionAbilities, updatedEntity.PrimaryEvolutionAbilities);
            UpdateEvolutionAbilities(existingEntity.SecondaryEvolutionAbilities, updatedEntity.SecondaryEvolutionAbilities);
            UpdateEvolutionAbilities(existingEntity.HiddenEvolutionAbilities, updatedEntity.HiddenEvolutionAbilities);
            UpdateAttackEffectivities(existingEntity.DefenseAttackEffectivities, updatedEntity.DefenseAttackEffectivities);
            UpdateSpawns(existingEntity.Spawns, updatedEntity.Spawns);
            UpdateEvolutions(existingEntity.Evolutions, updatedEntity.Evolutions);
            UpdateLearnableMoves(existingEntity.LearnableMoves, updatedEntity.LearnableMoves);
            UpdateHuntingConfigurations(existingEntity.HuntingConfigurations, updatedEntity.HuntingConfigurations);
            UpdateBuilds(existingEntity.Builds, updatedEntity.Builds);
        }

        private void UpdateVarieties(List<PokemonVarietyVarietyReadModel> existingVarieties, List<PokemonVarietyVarietyReadModel> updatedVarieties)
        {
            existingVarieties.RemoveAll(e =>
                !updatedVarieties.Select(u => u.ResourceName).Contains(e.ResourceName));

            foreach (var variety in updatedVarieties)
            {
                var existingVariety = existingVarieties
                    .SingleOrDefault(e => e.Name.EqualsExact(variety.ResourceName));

                if (existingVariety != null)
                {
                    existingVariety.ResourceName = variety.ResourceName;
                    existingVariety.Name = variety.Name;
                    existingVariety.SortIndex = variety.SortIndex;
                    existingVariety.SpriteName = variety.SpriteName;
                    existingVariety.Availability = variety.Availability;
                    existingVariety.PrimaryType = variety.PrimaryType;
                    existingVariety.SecondaryType = variety.SecondaryType;
                }
                else
                {
                    existingVarieties.Add(variety);
                }
            }
        }

        private void UpdateForms(List<PokemonVarietyFormReadModel> existingForms, List<PokemonVarietyFormReadModel> updatedForms)
        {
            existingForms.RemoveAll(e =>
                !updatedForms.Select(u => u.Name).Contains(e.Name));

            foreach (var form in updatedForms)
            {
                var existingForm = existingForms
                    .SingleOrDefault(e => e.Name.EqualsExact(form.Name));

                if (existingForm != null)
                {
                    existingForm.Name = form.Name;
                    existingForm.SortIndex = form.SortIndex;
                    existingForm.SpriteName = form.SpriteName;
                    existingForm.Availability = form.Availability;
                }
                else
                {
                    existingForms.Add(form);
                }
            }
        }

        private void UpdateUrls(
            List<PokemonVarietyUrlReadModel> existingUrls, 
            List<PokemonVarietyUrlReadModel> updatedUrls)
        {
            existingUrls.RemoveAll(u =>
                !updatedUrls.Select(uu => uu.ApplicationDbId).Contains(u.ApplicationDbId));

            foreach (var url in updatedUrls)
            {
                var existingUrl = existingUrls.SingleOrDefault(u => u.ApplicationDbId == url.ApplicationDbId);

                if (existingUrl != null)
                {
                    existingUrl.ApplicationDbId = url.ApplicationDbId;
                    existingUrl.Name = url.Name;
                    existingUrl.Url = url.Url;
                }
                else
                {
                    existingUrls.Add(url);
                }
            }
        }

        private void UpdateEvolutionAbilities(
            List<EvolutionAbilityReadModel> existingEvolutionAbilities,
            List<EvolutionAbilityReadModel> updatedEvolutionAbilities)
        {
            existingEvolutionAbilities.RemoveAll(a =>
                !updatedEvolutionAbilities.Select(a => a.PokemonResourceName).Contains(a.PokemonResourceName));

            foreach (var evolutionAbility in updatedEvolutionAbilities)
            {
                var existingEvolutionAbility = existingEvolutionAbilities
                    .SingleOrDefault(a => a.PokemonResourceName.EqualsExact(evolutionAbility.PokemonResourceName));

                if (existingEvolutionAbility != null)
                {
                    existingEvolutionAbility.RelativeStageIndex = evolutionAbility.RelativeStageIndex;
                    existingEvolutionAbility.PokemonResourceName = evolutionAbility.PokemonResourceName;
                    existingEvolutionAbility.PokemonSortIndex = evolutionAbility.PokemonSortIndex;
                    existingEvolutionAbility.PokemonName = evolutionAbility.PokemonName;
                    existingEvolutionAbility.SpriteName = evolutionAbility.SpriteName;
                    existingEvolutionAbility.AbilityName = evolutionAbility.AbilityName;
                }
                else
                {
                    existingEvolutionAbilities.Add(evolutionAbility);
                }
            }
        }

        private void UpdateAttackEffectivities(
            List<AttackEffectivityReadModel> existingEffectivities,
            List<AttackEffectivityReadModel> updatedEffectivities)
        {
            existingEffectivities.RemoveAll(e =>
                !updatedEffectivities.Select(u => u.TypeName).Contains(e.TypeName));

            foreach (var effectivity in updatedEffectivities)
            {
                var existingEffectivity = existingEffectivities
                    .SingleOrDefault(e => e.TypeName.EqualsExact(effectivity.TypeName));

                if (existingEffectivity != null)
                {
                    existingEffectivity.TypeName = effectivity.TypeName;
                    existingEffectivity.Effectivity = effectivity.Effectivity;
                }
                else
                {
                    existingEffectivities.Add(effectivity);
                }
            }
        }

        private void UpdateSpawns(List<SpawnReadModel> existingSpawns, List<SpawnReadModel> updatedSpawns)
        {
            existingSpawns.RemoveAll(s => 
                !updatedSpawns.Select(u => u.ApplicationDbId).Contains(s.ApplicationDbId));

            foreach (var spawn in updatedSpawns)
            {
                var existingSpawn = existingSpawns
                    .SingleOrDefault(s => s.ApplicationDbId == spawn.ApplicationDbId);

                if (existingSpawn != null)
                {
                    existingSpawn.ApplicationDbId = spawn.ApplicationDbId;
                    existingSpawn.PokemonFormSortIndex = spawn.PokemonFormSortIndex;
                    existingSpawn.LocationSortIndex = spawn.LocationSortIndex;
                    existingSpawn.PokemonResourceName = spawn.PokemonResourceName;
                    existingSpawn.PokemonName = spawn.PokemonName;
                    existingSpawn.SpriteName = spawn.SpriteName;
                    existingSpawn.LocationName = spawn.LocationName;
                    existingSpawn.LocationResourceName = spawn.LocationResourceName;
                    existingSpawn.RegionName = spawn.RegionName;
                    existingSpawn.RegionColor = spawn.RegionColor;
                    existingSpawn.IsEvent = spawn.IsEvent;
                    existingSpawn.EventName = spawn.EventName;
                    existingSpawn.EventStartDate = spawn.EventStartDate;
                    existingSpawn.EventEndDate = spawn.EventEndDate;
                    existingSpawn.SpawnType = spawn.SpawnType;
                    existingSpawn.SpawnTypeSortIndex = spawn.SpawnTypeSortIndex;
                    existingSpawn.SpawnTypeColor = spawn.SpawnTypeColor;
                    existingSpawn.IsSyncable = spawn.IsSyncable;
                    existingSpawn.IsInfinite = spawn.IsInfinite;
                    existingSpawn.LowestLevel = spawn.LowestLevel;
                    existingSpawn.HighestLevel = spawn.HighestLevel;
                    existingSpawn.RarityString = spawn.RarityString;
                    existingSpawn.RarityValue = spawn.RarityValue;
                    existingSpawn.Notes = spawn.Notes;

                    UpdateSeasons(existingSpawn.Seasons, spawn.Seasons);
                    UpdateTimesOfDay(existingSpawn.TimesOfDay, spawn.TimesOfDay);
                }
                else
                {
                    existingSpawns.Add(spawn);
                }
            }
        }

        private void UpdateSeasons(List<SeasonReadModel> existingSeasons, List<SeasonReadModel> updatedSeasons)
        {
            existingSeasons.RemoveAll(e =>
                !updatedSeasons.Select(u => u.Name).Contains(e.Name));

            foreach (var season in updatedSeasons)
            {
                var existingSeason = existingSeasons
                    .SingleOrDefault(e => e.Name.EqualsExact(season.Name));

                if (existingSeason != null)
                {
                    existingSeason.SortIndex = season.SortIndex;
                    existingSeason.Abbreviation = season.Abbreviation;
                    existingSeason.Name = season.Name;
                    existingSeason.Color = season.Color;
                }
                else
                {
                    existingSeasons.Add(season);
                }
            }
        }

        private void UpdateTimesOfDay(List<TimeOfDayReadModel> existingTimesOfDay, List<TimeOfDayReadModel> updatedTimesOfDay)
        {
            existingTimesOfDay.RemoveAll(e =>
                !updatedTimesOfDay.Select(u => u.Name).Contains(e.Name));

            foreach (var timeOfDay in updatedTimesOfDay)
            {
                var existingTimeOfDay = existingTimesOfDay
                    .SingleOrDefault(e => e.Name.EqualsExact(timeOfDay.Name));

                if (existingTimeOfDay != null)
                {
                    existingTimeOfDay.SortIndex = timeOfDay.SortIndex;
                    existingTimeOfDay.Abbreviation = timeOfDay.Abbreviation;
                    existingTimeOfDay.Name = timeOfDay.Name;
                    existingTimeOfDay.Color = timeOfDay.Color;
                    existingTimeOfDay.Times = timeOfDay.Times;
                }
                else
                {
                    existingTimesOfDay.Add(timeOfDay);
                }
            }
        }

        private void UpdateEvolutions(
            List<EvolutionReadModel> existingEvolutions,
            List<EvolutionReadModel> updatedEvolutions)
        {
            existingEvolutions.RemoveAll(e =>
                !updatedEvolutions.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var evolution in updatedEvolutions)
            {
                var existingEvolution = existingEvolutions
                    .SingleOrDefault(e => e.ApplicationDbId == evolution.ApplicationDbId);

                if (existingEvolution != null)
                {
                    existingEvolution.ApplicationDbId = evolution.ApplicationDbId;

                    existingEvolution.BaseName = evolution.BaseName;
                    existingEvolution.BaseResourceName = evolution.BaseResourceName;
                    existingEvolution.BaseSpriteName = evolution.BaseSpriteName;
                    existingEvolution.BasePrimaryElementalType = evolution.BasePrimaryElementalType;
                    existingEvolution.BaseSecondaryElementalType = evolution.BaseSecondaryElementalType;
                    existingEvolution.BaseSortIndex = evolution.BaseSortIndex;
                    existingEvolution.BaseStage = evolution.BaseStage;

                    existingEvolution.EvolvedName = evolution.EvolvedName;
                    existingEvolution.EvolvedResourceName = evolution.EvolvedResourceName;
                    existingEvolution.EvolvedSpriteName = evolution.EvolvedSpriteName;
                    existingEvolution.EvolvedPrimaryElementalType = evolution.EvolvedPrimaryElementalType;
                    existingEvolution.EvolvedSecondaryElementalType = evolution.EvolvedSecondaryElementalType;
                    existingEvolution.EvolvedSortIndex = evolution.EvolvedSortIndex;
                    existingEvolution.EvolvedStage = evolution.EvolvedStage;

                    existingEvolution.EvolutionTrigger = evolution.EvolutionTrigger;
                    existingEvolution.IsReversible = evolution.IsReversible;
                    existingEvolution.IsAvailable = existingEvolution.IsAvailable;
                }
                else
                {
                    existingEvolutions.Add(evolution);
                }
            }
        }

        private void UpdateLearnableMoves(
            List<LearnableMoveReadModel> existingLearnableMoves, 
            List<LearnableMoveReadModel> updatedLearnableMoves)
        {
            existingLearnableMoves.RemoveAll(e =>
                !updatedLearnableMoves.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var learnableMove in updatedLearnableMoves)
            {
                var existingLearnableMove = existingLearnableMoves
                    .SingleOrDefault(e => e.ApplicationDbId == learnableMove.ApplicationDbId);

                if (existingLearnableMove != null)
                {
                    existingLearnableMove.ApplicationDbId = learnableMove.ApplicationDbId;
                    existingLearnableMove.IsAvailable = learnableMove.IsAvailable;
                    existingLearnableMove.MoveName = learnableMove.MoveName;
                    existingLearnableMove.ElementalType = learnableMove.ElementalType;
                    existingLearnableMove.DamageClass = learnableMove.DamageClass;
                    existingLearnableMove.AttackPower = learnableMove.AttackPower;
                    existingLearnableMove.Accuracy = learnableMove.Accuracy;
                    existingLearnableMove.PowerPoints = learnableMove.PowerPoints;
                    existingLearnableMove.Priority = learnableMove.Priority;
                    existingLearnableMove.EffectDescription = learnableMove.EffectDescription;
                    existingLearnableMove.HasStab = learnableMove.HasStab;
                    existingLearnableMove.EffectivePower = learnableMove.EffectivePower;

                    UpdateLearnMethods(existingLearnableMove.LearnMethods, learnableMove.LearnMethods);
                }
                else
                {
                    existingLearnableMoves.Add(learnableMove);
                }
            }
        }

        private void UpdateLearnMethods(List<LearnMethodReadModel> existingLearnMethods, List<LearnMethodReadModel> updatedLearnMethods)
        {
            existingLearnMethods.RemoveAll(e =>
                !updatedLearnMethods.Select(u => u.LearnMethodName).Contains(e.LearnMethodName));

            foreach (var learnMethod in updatedLearnMethods)
            {
                var existingLearnMethod = existingLearnMethods
                    .SingleOrDefault(e => e.LearnMethodName.EqualsExact(learnMethod.LearnMethodName));

                if (existingLearnMethod != null)
                {
                    existingLearnMethod.IsAvailable = learnMethod.IsAvailable;
                    existingLearnMethod.LearnMethodName = learnMethod.LearnMethodName;
                    existingLearnMethod.Description = learnMethod.Description;
                    existingLearnMethod.SortIndex = learnMethod.SortIndex;
                }
                else
                {
                    existingLearnMethods.Add(learnMethod);
                }
            }
        }

        private void UpdateHuntingConfigurations(
            List<HuntingConfigurationReadModel> existingHuntingConfigurations, 
            List<HuntingConfigurationReadModel> updatedHuntingConfigurations)
        {
            existingHuntingConfigurations.RemoveAll(e =>
                !updatedHuntingConfigurations.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var huntingConfiguration in updatedHuntingConfigurations)
            {
                var existingHuntingConfiguration = existingHuntingConfigurations
                    .SingleOrDefault(e => e.ApplicationDbId == huntingConfiguration.ApplicationDbId);

                if (existingHuntingConfiguration != null)
                {
                    existingHuntingConfiguration.ApplicationDbId = huntingConfiguration.ApplicationDbId;
                    existingHuntingConfiguration.PokemonResourceName = huntingConfiguration.PokemonResourceName;
                    existingHuntingConfiguration.PokemonName = huntingConfiguration.PokemonName;
                    existingHuntingConfiguration.Nature = huntingConfiguration.Nature;
                    existingHuntingConfiguration.NatureEffect = huntingConfiguration.NatureEffect;
                    existingHuntingConfiguration.Ability = huntingConfiguration.Ability;
                }
                else
                {
                    existingHuntingConfigurations.Add(huntingConfiguration);
                }
            }
        }

        private void UpdateBuilds(List<BuildReadModel> existingBuilds, List<BuildReadModel> updatedBuilds)
        {
            existingBuilds.RemoveAll(e =>
                !updatedBuilds.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var build in updatedBuilds)
            {
                var existingBuild = existingBuilds
                    .SingleOrDefault(e => e.ApplicationDbId == build.ApplicationDbId);

                if (existingBuild != null)
                {
                    existingBuild.ApplicationDbId = build.ApplicationDbId;
                    existingBuild.PokemonResourceName = build.PokemonResourceName;
                    existingBuild.PokemonName = build.PokemonName;
                    existingBuild.BuildName = build.BuildName;
                    existingBuild.BuildDescription = build.BuildDescription;
                    existingBuild.Ability = build.Ability;
                    existingBuild.AbilityDescription = build.AbilityDescription;
                    existingBuild.AtkEv = build.AtkEv;
                    existingBuild.SpaEv = build.SpaEv;
                    existingBuild.DefEv = build.DefEv;
                    existingBuild.SpdEv = build.SpdEv;
                    existingBuild.SpeEv = build.SpeEv;
                    existingBuild.HpEv = build.HpEv;

                    UpdateMoveOptions(existingBuild.MoveOptions, build.MoveOptions);
                    UpdateNatureOptions(existingBuild.NatureOptions, build.NatureOptions);
                    UpdateItemOptions(existingBuild.ItemOptions, build.ItemOptions);
                }
                else
                {
                    existingBuilds.Add(build);
                }
            }
        }

        private void UpdateMoveOptions(List<MoveOptionReadModel> existingMoveOptions, List<MoveOptionReadModel> updatedMoveOptions)
        {
            existingMoveOptions.RemoveAll(e =>
                !updatedMoveOptions.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var moveOption in updatedMoveOptions)
            {
                var existingMoveOption = existingMoveOptions
                    .SingleOrDefault(e => e.ApplicationDbId == moveOption.ApplicationDbId);

                if (existingMoveOption != null)
                {
                    existingMoveOption.ApplicationDbId = moveOption.ApplicationDbId;
                    existingMoveOption.Slot = moveOption.Slot;
                    existingMoveOption.MoveName = moveOption.MoveName;
                    existingMoveOption.ElementalType = moveOption.ElementalType;
                    existingMoveOption.DamageClass = moveOption.DamageClass;
                    existingMoveOption.AttackPower = moveOption.AttackPower;
                    existingMoveOption.Accuracy = moveOption.Accuracy;
                    existingMoveOption.PowerPoints = moveOption.PowerPoints;
                    existingMoveOption.Priority = moveOption.Priority;
                    existingMoveOption.EffectDescription = moveOption.EffectDescription;
                }
                else
                {
                    existingMoveOptions.Add(moveOption);
                }
            }
        }

        private void UpdateNatureOptions(List<NatureOptionReadModel> existingNatureOptions, List<NatureOptionReadModel> updatedNatureOptions)
        {
            existingNatureOptions.RemoveAll(e =>
                !updatedNatureOptions.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var natureOption in updatedNatureOptions)
            {
                var existingNatureOption = existingNatureOptions
                    .SingleOrDefault(e => e.ApplicationDbId == natureOption.ApplicationDbId);

                if (existingNatureOption != null)
                {
                    existingNatureOption.ApplicationDbId = natureOption.ApplicationDbId;
                    existingNatureOption.NatureName = natureOption.NatureName;
                    existingNatureOption.NatureEffect = natureOption.NatureEffect;
                }
                else
                {
                    existingNatureOptions.Add(natureOption);
                }
            }
        }

        private void UpdateItemOptions(List<ItemOptionReadModel> existingItemOptions, List<ItemOptionReadModel> updatedItemOptions)
        {
            existingItemOptions.RemoveAll(e =>
                !updatedItemOptions.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var itemOption in updatedItemOptions)
            {
                var existingItemOption = existingItemOptions
                    .SingleOrDefault(e => e.ApplicationDbId == itemOption.ApplicationDbId);

                if (existingItemOption != null)
                {
                    existingItemOption.ApplicationDbId = itemOption.ApplicationDbId;
                    existingItemOption.ItemResourceName = itemOption.ItemResourceName;
                    existingItemOption.ItemName = itemOption.ItemName;
                }
                else
                {
                    existingItemOptions.Add(itemOption);
                }
            }
        }
    }
}
