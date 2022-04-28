using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.LocationGroups
{
    public class LocationGroupReadModelMapper : IReadModelMapper<LocationGroupReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public LocationGroupReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<LocationGroupReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport importReport)
        {
            var locationGroups = _dbContext.LocationGroups
                .Include(lg => lg.Locations)
                .Include(lg => lg.Region)
                .ThenInclude(r => r.Event)
                .Include(lg => lg.Locations)
                .ThenInclude(l => l.PokemonSpawns)
                .ThenInclude(s => s.PokemonForm)
                .ThenInclude(f => f.PokemonVariety)
                .Include(lg => lg.Locations)
                .ThenInclude(l => l.PokemonSpawns)
                .ThenInclude(s => s.SpawnType)
                .Include(lg => lg.Locations)
                .ThenInclude(l => l.PokemonSpawns)
                .ThenInclude(s => s.SpawnOpportunities)
                .ThenInclude(o => o.Season)
                .Include(lg => lg.Locations)
                .ThenInclude(l => l.PokemonSpawns)
                .ThenInclude(s => s.SpawnOpportunities)
                .ThenInclude(o => o.TimeOfDay)
                .ThenInclude(t => t.SeasonTimes)
                .ThenInclude(st => st.Season)
                .Include(lg => lg.Locations)
                .ThenInclude(l => l.PlacedItems)
                .ThenInclude(pi => pi.Item)
                .AsNoTracking()
                .OrderBy(lg => lg.Locations.Min(l => l.SortIndex))
                .ToList();

            var result = new List<LocationGroupReadModel>();

            for (var i = 0; i < locationGroups.Count; i++)
            {
                var locationGroup = locationGroups[i];

                var readModel = GetBasicReadModel(locationGroup);

                var regionEvent = locationGroup.Region.Event;
                if (regionEvent != null)
                {
                    SetEventInformation(readModel, regionEvent);
                }

                var previous = i != 0 ? locationGroups[i - 1] : locationGroups[^1];
                var next = i != locationGroups.Count - 1 ? locationGroups[i + 1] : locationGroups[0];
                AttachPreviousAndNext(readModel, previous, next);
                locationGroup.Locations.ForEach(l => readModel.Locations.Add(GetLocationDetails(l)));

                result.Add(readModel);
            }

            return result.ToDictionary(x => x, _ => DbAction.Create);
        }

        private LocationReadModel GetLocationDetails(Location l)
        {
            var locationReadModel = new LocationReadModel();

            locationReadModel.Name = l.Name;
            locationReadModel.SortIndex = l.SortIndex;
            locationReadModel.IsDiscoverable = l.IsDiscoverable;
            locationReadModel.Notes = l.Notes;
            l.PokemonSpawns.ForEach(spawn => locationReadModel.Spawns.Add(GetSpawnReadModel(spawn)));
            l.PlacedItems.ForEach(item => locationReadModel.PlacedItems.Add(GetPlacedItem(item)));

            return locationReadModel;
        }

        private PlacedItemReadModel GetPlacedItem(PlacedItem pi)
        {
            return new()
            {
                ItemResourceName = pi.Item.ResourceName,
                ItemName = pi.Item.Name,
                ItemSpriteName = pi.Item.SpriteName,
                RegionName = pi.Location.LocationGroup.Region.Name,
                RegionColor = pi.Location.LocationGroup.Region.Color,
                LocationName = pi.Location.Name,
                LocationResourceName = pi.Location.LocationGroup.ResourceName,
                LocationSortIndex = pi.Location.SortIndex,
                SortIndex = pi.SortIndex,
                Index = pi.Index,
                PlacementDescription = pi.PlacementDescription,
                IsHidden = pi.IsHidden,
                IsConfirmed = pi.IsConfirmed,
                Quantity = pi.Quantity,
                Notes = pi.Notes,
                Screenshot = pi.ScreenshotName
            };
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

        private string GetTimesAsString(TimeOfDay timeOfDay)
        {
            if (timeOfDay.Name.Equals(TimeOfDay.ANY))
            {
                return string.Empty;
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

        private void AttachPreviousAndNext(LocationGroupReadModel readModel, Data.Entities.LocationGroup previous, Data.Entities.LocationGroup next)
        {
            readModel.PreviousLocationGroupName = previous.Name;
            readModel.PreviousLocationGroupResourceName = previous.ResourceName;
            readModel.NextLocationGroupName = next.Name;
            readModel.NextLocationGroupResourceName = next.ResourceName;
        }

        private static void SetEventInformation(LocationGroupReadModel readModel, Data.Entities.Event regionEvent)
        {
            var dateTimeCulture = CultureInfo.CreateSpecificCulture("en-US");
            var dateTimeFormat = "MMM d, yyyy";
            readModel.EventName = regionEvent.Name;
            readModel.EventStartDate = regionEvent.StartDate?.ToString(dateTimeFormat, dateTimeCulture);
            readModel.EventEndDate = regionEvent.EndDate?.ToString(dateTimeFormat, dateTimeCulture);
        }

        private LocationGroupReadModel GetBasicReadModel(Data.Entities.LocationGroup lg)
        {
            return new()
            {
                ApplicationDbId = lg.Id,
                ResourceName = lg.ResourceName,
                Name = lg.Name,
                RegionName = lg.Region.Name,
                RegionResourceName = lg.Region.ResourceName,
                SortIndex = lg.Locations.Min(l => l.SortIndex),
                IsEventRegion = lg.Region.IsEventRegion
            };
        }
    }
}