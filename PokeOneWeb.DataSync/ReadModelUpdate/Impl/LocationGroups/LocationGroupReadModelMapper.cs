using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

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
                .Include(lg => lg.Locations)
                .ThenInclude(l => l.PlacedItems)
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

        private LocationReadModel GetLocationDetails(Data.Entities.Location l)
        {
            var locationReadModel = new LocationReadModel();

            locationReadModel.Name = l.Name;
            locationReadModel.SortIndex = l.SortIndex;
            locationReadModel.IsDiscoverable = l.IsDiscoverable;
            locationReadModel.Notes = l.Notes;
            l.PokemonSpawns.ForEach(spawn => locationReadModel.Spawns.Add(GetSpawnDetails(spawn)));

            return locationReadModel;
        }

        private SpawnReadModel GetSpawnDetails(Data.Entities.Spawn spawn)
        {
            return new()
            {
                PokemonName = spawn.PokemonForm.Name,
            };
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