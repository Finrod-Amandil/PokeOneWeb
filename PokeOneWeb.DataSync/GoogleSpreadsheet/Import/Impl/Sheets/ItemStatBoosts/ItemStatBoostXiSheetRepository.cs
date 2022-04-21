using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ItemStatBoosts
{
    public class ItemStatBoostXiSheetRepository
        : XSheetRepository<ItemStatBoostSheetDto, ItemStatBoostPokemon>
    {
        public ItemStatBoostXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<ItemStatBoostSheetDto> parser,
            XISpreadsheetEntityMapper<ItemStatBoostSheetDto, ItemStatBoostPokemon> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<ItemStatBoostPokemon> DbSet => DbContext.ItemStatBoostPokemon;

        protected override Entity Entity => Entity.ItemStatBoostPokemon;

        protected override List<ItemStatBoostPokemon> AttachRelatedEntities(List<ItemStatBoostPokemon> entities)
        {
            var varieties = DbContext.PokemonVarieties.ToList();
            var items = DbContext.Items.ToList();
            var itemStatBoosts = DbContext.ItemStatBoosts
                .IncludeOptimized(i => i.Item)
                .ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                // Pokemon Variety (optional)
                if (entity.PokemonVariety != null)
                {
                    var pokemonVariety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.PokemonVariety.Name));

                    if (pokemonVariety is null)
                    {
                        Reporter.ReportError(Entity, entity.IdHash,
                            $"Could not find matching variety {entity.PokemonVariety.Name}, skipping item stat boost pokemon.");

                        entities.Remove(entity);
                        i--;
                        continue;
                    }

                    entity.PokemonVarietyId = pokemonVariety.Id;
                    entity.PokemonVariety = pokemonVariety;
                }

                // Item Stat boost, may already exist --> update, else insert
                var existingItemStatBoost = itemStatBoosts
                    .SingleOrDefault(i => i.Item.Name.EqualsExact(entity.ItemStatBoost.Item.Name));

                if (existingItemStatBoost != null)
                {
                    existingItemStatBoost.ItemId = entity.ItemStatBoost.Item.Id;
                    existingItemStatBoost.Item = entity.ItemStatBoost.Item;
                    existingItemStatBoost.AttackBoost = entity.ItemStatBoost.AttackBoost;
                    existingItemStatBoost.SpecialAttackBoost = entity.ItemStatBoost.SpecialAttackBoost;
                    existingItemStatBoost.DefenseBoost = entity.ItemStatBoost.SpecialDefenseBoost;
                    existingItemStatBoost.SpeedBoost = entity.ItemStatBoost.SpeedBoost;
                    existingItemStatBoost.HitPointsBoost = entity.ItemStatBoost.HitPointsBoost;

                    entity.ItemStatBoostId = existingItemStatBoost.Id;
                    entity.ItemStatBoost = existingItemStatBoost;
                }

                // Item
                var item = items.SingleOrDefault(i => i.Name.EqualsExact(entity.ItemStatBoost.Item.Name));

                if (item is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Item {entity.ItemStatBoost.Item.Name}, skipping item stat boost pokemon.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.ItemStatBoost.ItemId = item.Id;
                entity.ItemStatBoost.Item = item;
            }

            return entities;
        }
    }
}