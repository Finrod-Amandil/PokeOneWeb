using Microsoft.Extensions.Logging;
using PokeOneWeb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Seasons
{
    public class SeasonMapper : ISpreadsheetEntityMapper<SeasonSheetDto, Season>
    {
        private readonly ILogger _logger;

        public SeasonMapper(ILogger<SeasonSheetDto> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Season> Map(IDictionary<RowHash, SeasonSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Season DTO. Skipping.");
                    continue;
                }

                yield return MapSeason(dto, rowHash);
            }
        }

        public IEnumerable<Season> MapOnto(IList<Season> entities, IDictionary<RowHash, SeasonSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Season DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Season entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapSeason(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(SeasonSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.Abbreviation);
        }

        private Season MapSeason(SeasonSheetDto dto, RowHash rowHash, Season season = null)
        {
            season ??= new Season();

            season.IdHash = rowHash.IdHash;
            season.Hash = rowHash.ContentHash;
            season.ImportSheetId = rowHash.ImportSheetId;
            season.SortIndex = dto.SortIndex;
            season.Name = dto.Name;
            season.Abbreviation = dto.Abbreviation;
            season.Color = dto.Color;

            return season;
        }
    }
}
