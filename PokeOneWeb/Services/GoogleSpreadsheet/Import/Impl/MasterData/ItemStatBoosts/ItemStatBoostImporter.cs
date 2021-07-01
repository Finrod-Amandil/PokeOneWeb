using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MasterData.ItemStatBoosts
{
    public class ItemStatBoostImporter : SpreadsheetEntityImporter<ItemStatBoostDto, ItemStatBoost>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ItemStatBoostImporter> _logger;

        public ItemStatBoostImporter(
            ISpreadsheetEntityReader<ItemStatBoostDto> reader, 
            ISpreadsheetEntityMapper<ItemStatBoostDto, ItemStatBoost> mapper,
            ApplicationDbContext dbContext,
            ILogger<ItemStatBoostImporter> logger) : base(reader, mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        protected override string GetSheetPrefix()
        {
            return Constants.SHEET_PREFIX_ITEM_STAT_BOOSTS;
        }

        protected override void WriteToDatabase(IEnumerable<ItemStatBoost> newItemStatBoosts)
        {
            var items = _dbContext.Items.ToList();
            var pokemonVarieties = _dbContext.PokemonVarieties.ToList();

            foreach (var newItemStatBoost in newItemStatBoosts)
            {
                var item = items.SingleOrDefault(i =>
                    i.Name.Equals(newItemStatBoost.Item.Name, StringComparison.Ordinal));

                if (item is null)
                {
                    _logger.LogWarning($"No unique matching item could be found for Item name {newItemStatBoost.Item.Name}. Skipping.");
                    continue;
                }

                newItemStatBoost.Item = item;

                foreach (var requiredPokemon in newItemStatBoost.RequiredPokemon)
                {
                    var pokemonVariety = pokemonVarieties.SingleOrDefault(p =>
                        p.Name.Equals(requiredPokemon.PokemonVariety.Name, StringComparison.Ordinal));

                    if (pokemonVariety is null)
                    {
                        _logger.LogWarning($"No unique matching pokemon variety could be found for Pokemon Variety name {requiredPokemon.PokemonVariety.Name}. Skipping.");
                    }

                    requiredPokemon.PokemonVariety = pokemonVariety;
                }

                newItemStatBoost.RequiredPokemon = newItemStatBoost.RequiredPokemon
                    .Where(rp => rp.PokemonVariety != null)
                    .ToList();

                _dbContext.ItemStatBoosts.Add(newItemStatBoost);
            }

            _dbContext.SaveChanges();
        }
    }
}
