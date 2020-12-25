using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Currencies
{
    public class CurrencyMapper : ISpreadsheetEntityMapper<CurrencyDto, Currency>
    {
        private readonly ILogger<CurrencyMapper> _logger;

        public CurrencyMapper(ILogger<CurrencyMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Currency> Map(IEnumerable<CurrencyDto> dtos)
        {
            if (dtos is null)
            {
                throw new ArgumentNullException(nameof(dtos));
            }

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning("Found invalid Currency DTO. Skipping.");
                    continue;
                }

                yield return new Currency
                {
                    Item = new Item
                    {
                        Name = dto.ItemName
                    }
                };
            }
        }

        private bool IsValid(CurrencyDto dto)
        {
            return !string.IsNullOrWhiteSpace(dto.ItemName);
        }
    }
}
