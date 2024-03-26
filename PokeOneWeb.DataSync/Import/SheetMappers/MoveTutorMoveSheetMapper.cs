using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class MoveTutorMoveSheetMapper : SheetMapper<MoveTutorMove>
    {
        public MoveTutorMoveSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<MoveTutorMove, object>> ValueToEntityMappings => new()
        {
            { "TutorName", (e, v) => e.MoveTutorName = v.ParseAsNonEmptyString() },
            { "Move", (e, v) => e.MoveName = v.ParseAsNonEmptyString() },
            { "RedShardPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Red Shard") },
            { "BlueShardPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Blue Shard") },
            { "GreenShardPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Green Shard") },
            { "YellowShardPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Yellow Shard") },
            { "PWTBPPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Battle Points (PWT)") },
            { "BFBPPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Battle Points (BF)") },
            { "PokeDollarPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Poké Dollar") },
            { "PokeGoldPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Poké Gold") },
        };

        private static void AddPrice(MoveTutorMove entity, decimal? amount, string currencyName)
        {
            if (amount is null or 0)
            {
                return;
            }

            entity.Price.Add(new MoveTutorMovePrice
            {
                CurrencyAmount = new CurrencyAmount
                {
                    CurrencyName = currencyName,
                    Amount = (decimal)amount
                },
            });
        }
    }
}