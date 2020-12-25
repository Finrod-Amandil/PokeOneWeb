using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.Currencies
{
    public class CurrencyImporter : SpreadsheetEntityImporter<CurrencyDto, Currency>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CurrencyImporter> _logger;

        public CurrencyImporter(
            ISpreadsheetEntityReader<CurrencyDto> reader, 
            ISpreadsheetEntityMapper<CurrencyDto, Currency> mapper,
            ApplicationDbContext dbContext,
            ILogger<CurrencyImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_CURRENCIES;
        }

        protected override void WriteToDatabase(IEnumerable<Currency> entities)
        {
            var items = _dbContext.Items.ToList();

            if (!items.Any())
            {
                throw new Exception("Tried to import currencies, but no items were" +
                                    "present in the database.");
            }

            foreach (var currency in entities)
            {
                var item = items.SingleOrDefault(i =>
                    i.Name.Equals(currency.Item.Name, StringComparison.Ordinal));

                if (item is null)
                {
                    _logger.LogWarning($"No unique matching item could be found for currency item {currency.Item.Name}. Skipping.");
                    continue;
                }

                currency.Item = item;

                _dbContext.Currencies.Add(currency);
            }

            _dbContext.SaveChanges();
        }
    }
}
