using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Locations
{
    public class LocationMapper : ISpreadsheetEntityMapper<LocationSheetDto, Location>
    {
        private readonly ILogger<LocationMapper> _logger;

        private IDictionary<string, Region> _regions;
        private IDictionary<string, LocationGroup> _locationGroups;
        private IDictionary<string, Location> _locations;

        public LocationMapper(ILogger<LocationMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Location> Map(IDictionary<RowHash, LocationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _regions = new Dictionary<string, Region>();
            _locationGroups = new Dictionary<string, LocationGroup>();
            _locations = new Dictionary<string, Location>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Location DTO. Skipping.");
                    continue;
                }

                if (_locations.ContainsKey(dto.LocationName))
                {
                    _logger.LogWarning($"Found duplicate location while reading Google Spreadsheet: {dto.LocationName}. Skipping the duplicate location.");
                    continue;
                }

                var location = MapLocation(dto, rowHash);
                _locations.Add(dto.LocationName, location);
            }

            return _locations.Values;
        }

        public IEnumerable<Location> MapOnto(IList<Location> entities, IDictionary<RowHash, LocationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _regions = new Dictionary<string, Region>();
            _locationGroups = new Dictionary<string, LocationGroup>();
            _locations = new Dictionary<string, Location>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Location DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Location entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                if (_locations.ContainsKey(dto.LocationName))
                {
                    _logger.LogWarning($"Found duplicate location while reading Google Spreadsheet: {dto.LocationName}. Skipping the duplicate location.");
                    continue;
                }

                var location = MapLocation(dto, rowHash);
                _locations.Add(dto.LocationName, location);
            }

            return entities;
        }

        private bool IsValid(LocationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                !string.IsNullOrWhiteSpace(dto.LocationGroupName) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.RegionName);
        }

        private Location MapLocation(LocationSheetDto dto, RowHash rowHash, Location location = null)
        {
            location ??= new Location();

            location.IdHash = rowHash.IdHash;
            location.Hash = rowHash.ContentHash;
            location.ImportSheetId = rowHash.ImportSheetId;
            location.Name = dto.LocationName;
            location.LocationGroup = MapLocationGroup(dto, location.LocationGroup);
            location.SortIndex = dto.SortIndex;
            location.IsDiscoverable = dto.IsDiscoverable;
            location.Notes = dto.Notes;

            return location;
        }

        private LocationGroup MapLocationGroup(LocationSheetDto dto, LocationGroup locationGroup = null)
        {
            locationGroup ??= new LocationGroup();
            if (!_locationGroups.ContainsKey(dto.LocationGroupName))
            {
                locationGroup.Name = dto.LocationGroupName;
                locationGroup.ResourceName = dto.ResourceName;
                locationGroup.Region = MapRegion(dto);
                _locationGroups.Add(dto.LocationGroupName, locationGroup);
            }
            else
            {
                locationGroup = _locationGroups[dto.LocationGroupName];
            }

            return locationGroup;
        }

        private Region MapRegion(LocationSheetDto dto)
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
    }
}
