using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;using System;


namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.Location
{
    public class LocationMapper : ISpreadsheetMapper<LocationDto, Data.Entities.Location>
    {
        private readonly ILogger<LocationMapper> _logger;

        private IDictionary<string, Region> _regions;
        private IDictionary<string, LocationGroup> _locationGroups;
        private IDictionary<string, Data.Entities.Location> _locations;

        public LocationMapper(ILogger<LocationMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Data.Entities.Location> Map(IEnumerable<LocationDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            _regions = new Dictionary<string, Region>();
            _locationGroups = new Dictionary<string, LocationGroup>();
            _locations = new Dictionary<string, Data.Entities.Location>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid location DTO with location name {dto.LocationName}. Skipping.");
                    continue;
                }

                var region = MapRegion(dto);

                var locationGroup = MapLocationGroup(dto, region);

                if (_locations.ContainsKey(dto.LocationName))
                {
                    _logger.LogWarning($"Found duplicate location while reading Google Spreadsheet: {dto.LocationName}. Skipping the duplicate location.");
                    continue;
                }

                var location = MapLocation(dto, locationGroup);

                locationGroup.Locations.Add(location);
                _locations.Add(dto.LocationName, location);
            }

            return _locations.Values;
        }

        private bool IsValid(LocationDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                !string.IsNullOrWhiteSpace(dto.LocationGroupName) &&
                !string.IsNullOrWhiteSpace(dto.RegionName);
        }

        private Region MapRegion(LocationDto dto)
        {
            Region region;
            if (!_regions.ContainsKey(dto.RegionName))
            {
                region = new Region { Name = dto.RegionName };
                _regions.Add(dto.RegionName, region);
            }
            else
            {
                region = _regions[dto.RegionName];
            }

            return region;
        }

        private LocationGroup MapLocationGroup(LocationDto dto, Region region)
        {
            LocationGroup locationGroup;
            if (!_locationGroups.ContainsKey(dto.LocationGroupName))
            {
                locationGroup = new LocationGroup { Name = dto.LocationGroupName, Region = region };
                region.LocationGroups.Add(locationGroup);
                _locationGroups.Add(dto.LocationGroupName, locationGroup);
            }
            else
            {
                locationGroup = _locationGroups[dto.LocationGroupName];
            }

            return locationGroup;
        }

        private Data.Entities.Location MapLocation(LocationDto dto, LocationGroup locationGroup)
        {
            return new Data.Entities.Location
            {
                Name = dto.LocationName,
                LocationGroup = locationGroup,
                SortIndex = dto.SortIndex,
                IsDiscoverable = dto.IsDiscoverable,
                Notes = dto.Notes
            };
        }
    }
}
