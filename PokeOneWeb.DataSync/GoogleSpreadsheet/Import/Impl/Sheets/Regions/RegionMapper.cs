using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionMapper : XSpreadsheetEntityMapper<RegionSheetDto, Region>
    {
        private readonly Dictionary<string, Event> _events = new();

        public RegionMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.Region;

        protected override bool IsValid(RegionSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(RegionSheetDto dto)
        {
            return dto.ResourceName;
        }

        protected override Region MapEntity(RegionSheetDto dto, RowHash rowHash, Region region = null)
        {
            region ??= new Region();

            if (!string.IsNullOrWhiteSpace(dto.EventName))
            {
                Event eventEntity;
                if (_events.ContainsKey(dto.EventName))
                {
                    eventEntity = _events[dto.EventName];
                }
                else
                {
                    eventEntity = new Event { Name = dto.EventName };
                    _events.Add(dto.EventName, eventEntity);
                }

                region.Event = eventEntity;
            }

            region.IdHash = rowHash.IdHash;
            region.Hash = rowHash.Hash;
            region.ImportSheetId = rowHash.ImportSheetId;
            region.Name = dto.Name;
            region.ResourceName = dto.ResourceName;
            region.Color = dto.Color;
            region.Description = dto.Description;
            region.IsReleased = dto.IsReleased;
            region.IsMainRegion = dto.IsMainRegion;
            region.IsSideRegion = dto.IsSideRegion;
            region.IsEventRegion = dto.IsEventRegion;

            return region;
        }
    }
}