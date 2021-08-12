using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Currencies
{
    public class CurrencyMapper : ISpreadsheetEntityMapper<CurrencyDto, Currency>
    {
        private readonly ILogger<CurrencyMapper> _logger;

        public CurrencyMapper(ILogger<CurrencyMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Currency> Map(IDictionary<RowHash, CurrencyDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Currency DTO. Skipping.");
                    continue;
                }

                yield return MapCurrency(dto, rowHash);
            }
        }

        public IEnumerable<Currency> MapOnto(IList<Currency> entities, IDictionary<RowHash, CurrencyDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid Currency DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching Currency entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapCurrency(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private bool IsValid(CurrencyDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.ItemName);
        }

        private Currency MapCurrency(CurrencyDto dto, RowHash rowHash, Currency currency = null)
        {
            currency ??= new Currency();

            currency.IdHash = rowHash.IdHash;
            currency.Hash = rowHash.ContentHash;
            currency.ImportSheetId = rowHash.ImportSheetId;
            currency.Item = new Item {Name = dto.ItemName};

            return currency;
        }
    }
}
