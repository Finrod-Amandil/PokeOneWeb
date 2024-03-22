using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
{
    public class MoveLearnMethodLocationSheetMapper : SheetMapper<MoveLearnMethodLocation>
    {
        public MoveLearnMethodLocationSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<MoveLearnMethodLocation, object>> ValueToEntityMappings => new()
        {
            { "MoveLearnMethod", (e, v) => e.MoveLearnMethod.Name = v.ParseAsNonEmptyString() },
            { "TutorType", (e, v) => e.TutorType = v.ParseAsNonEmptyString() },
            { "NpcName", (e, v) => e.NpcName = v.ParseAsNonEmptyString() },
            { "Location", (e, v) => e.LocationName = v.ParseAsNonEmptyString() },
            { "PlacementDescription", (e, v) => e.PlacementDescription = v.ParseAsString() },
            { "PokeDollarPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Poké Dollar") },
            { "PokeGoldPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Poké Gold") },
            { "BigMushroomPrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Big Mushroom") },
            { "HeartScalePrice", (e, v) => AddPrice(e, v.ParseAsOptionalDecimal(), "Heart Scale") },
        };

        private static void AddPrice(MoveLearnMethodLocation entity, decimal? amount, string currencyName)
        {
            if (amount is null or 0)
            {
                return;
            }

            entity.Price.Add(new MoveLearnMethodLocationPrice
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