using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PokeOneWeb.Configuration;
using PokeOneWeb.Extensions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Abilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Availabilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.BagCategories;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Builds;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Currencies;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypeRelations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ElementalTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Events;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Evolutions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.HuntingConfigurations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Items;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.ItemStatBoosts;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.LearnableMoveLearnMethods;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Locations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveDamageClasses;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveLearnMethodLocations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Moves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutorMoves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Natures;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PlacedItems;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Pokemon;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.PvpTiers;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Regions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Seasons;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SeasonTimesOfDay;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Spawns;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.SpawnTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.TimesOfDay;
using System;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveTutors;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl
{
    public class SheetNameHelper : ISheetNameHelper
    {
        private readonly IOptions<GoogleSpreadsheetsSettings> _settings;
        private readonly IServiceProvider _serviceProvider;

        public SheetNameHelper(
            IOptions<GoogleSpreadsheetsSettings> settings,
            IServiceProvider serviceProvider)
        {
            _settings = settings;
            _serviceProvider = serviceProvider;
        }

        public ISheetRepository GetSheetRepositoryForSheetName(string sheetName)
        {
            var sheetNames = _settings.Value.Import.SheetNames;
            var sheetPrefixes = _settings.Value.Import.SheetPrefixes;

            if (sheetNames.Abilities.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<AbilitySheetRepository>();
            if (sheetNames.Availabilities.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<AvailabilitySheetRepository>();
            if (sheetNames.BagCategories.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<BagCategorySheetRepository>();
            if (sheetNames.Builds.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<BuildSheetRepository>();
            if (sheetNames.Currencies.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<CurrencySheetRepository>();
            if (sheetNames.ElementalTypeRelations.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<ElementalTypeRelationSheetRepository>();
            if (sheetNames.ElementalTypes.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<ElementalTypeSheetRepository>();
            if (sheetNames.Events.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<EventSheetRepository>();
            if (sheetNames.Evolutions.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<EvolutionSheetRepository>();
            if (sheetNames.HuntingConfigurations.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<HuntingConfigurationSheetRepository>();
            if (sheetNames.Items.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<ItemSheetRepository>();
            if (sheetNames.ItemStatBoosts.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<ItemStatBoostSheetRepository>();
            if (sheetNames.MoveDamageClasses.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<MoveDamageClassSheetRepository>();
            if (sheetNames.MoveLearnMethodLocations.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<MoveLearnMethodLocationSheetRepository>();
            if (sheetNames.Moves.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<MoveSheetRepository>();
            if (sheetNames.MoveTutors.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<MoveTutorSheetRepository>();
            if (sheetNames.TutorMoves.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<MoveTutorMoveSheetRepository>();
            if (sheetNames.Natures.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<NatureSheetRepository>();
            if (sheetNames.Pokemon.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<PokemonSheetRepository>();
            if (sheetNames.PvpTiers.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<PvpTierSheetRepository>();
            if (sheetNames.Regions.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<RegionSheetRepository>();
            if (sheetNames.Seasons.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<SeasonSheetRepository>();
            if (sheetNames.SeasonTimesOfDay.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<SeasonTimeOfDaySheetRepository>();
            if (sheetNames.SpawnTypes.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<SpawnTypeSheetRepository>();
            if (sheetNames.TimesOfDay.EqualsExact(sheetName)) return _serviceProvider.GetRequiredService<TimeOfDaySheetRepository>();

            if (sheetName.StartsWith(sheetPrefixes.LearnableMoves)) return _serviceProvider.GetRequiredService<LearnableMoveLearnMethodSheetRepository>();
            if (sheetName.StartsWith(sheetPrefixes.Locations)) return _serviceProvider.GetRequiredService<LocationSheetRepository>();
            if (sheetName.StartsWith(sheetPrefixes.PlacedItems)) return _serviceProvider.GetRequiredService<PlacedItemSheetRepository>();
            if (sheetName.StartsWith(sheetPrefixes.Spawns)) return _serviceProvider.GetRequiredService<SpawnSheetRepository>();

            throw new ArgumentOutOfRangeException();
        }
    }
}
