using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Locations
{
    public class LocationMapper : SpreadsheetEntityMapper<LocationSheetDto, Location>
    {
        private readonly Dictionary<string, Region> _regions = new();
        private readonly Dictionary<string, LocationGroup> _locationGroups = new();

        public LocationMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.Location;

        protected override bool IsValid(LocationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.LocationName) &&
                !string.IsNullOrWhiteSpace(dto.LocationGroupName) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.RegionName);
        }

        protected override string GetUniqueName(LocationSheetDto dto)
        {
            return dto.LocationName;
        }

        protected override Location MapEntity(LocationSheetDto dto, RowHash rowHash, Location location = null)
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
