using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PokeOneWeb.Configuration;
using PokeOneWeb.Extensions;
using System;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Abilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Availabilities;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.BagCategories;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Builds;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Currencies;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Events;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Evolutions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.HuntingConfigurations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Items;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Locations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveDamageClasses;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Moves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutors;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Natures;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.PlacedItems;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Pokemon;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.PvpTiers;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Regions;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Seasons;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.SeasonTimesOfDay;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Spawns;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.SpawnTypes;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.TimesOfDay;

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
