﻿using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.Regions
{
    public class RegionMapper : SpreadsheetEntityMapper<RegionSheetDto, Region>
    {
        private readonly Dictionary<string, Event> _events = new();

        public RegionMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.Region;

        protected override bool IsValid(RegionSheetDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.Name);
        }

        protected override string GetUniqueName(RegionSheetDto dto)
        {
            return dto.Name;
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
            region.Hash = rowHash.ContentHash;
            region.ImportSheetId = rowHash.ImportSheetId;
            region.Name = dto.Name;
            region.Color = dto.Color;
            region.IsEventRegion = dto.IsEventRegion;

            return region;
        }
    }
}