using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Builds
{
    public class BuildXiSheetRepository : XSheetRepository<BuildSheetDto, Build>
    {
        public BuildXiSheetRepository(
            ApplicationDbContext dbContext,
            XISheetRowParser<BuildSheetDto> parser,
            XISpreadsheetEntityMapper<BuildSheetDto, Build> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter)
        {
        }

        protected override DbSet<Build> DbSet => DbContext.Builds;

        protected override Entity Entity => Entity.Build;

        protected override List<Build> AttachRelatedEntities(List<Build> entities)
        {
            var varieties = DbContext.PokemonVarieties.ToList();
            var abilities = DbContext.Abilities.ToList();
            var moves = DbContext.Moves.ToList();
            var items = DbContext.Items.ToList();
            var natures = DbContext.Natures.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var variety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.PokemonVariety.Name));
                if (variety is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching PokemonVariety {entity.PokemonVariety.Name}, skipping build.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.PokemonVarietyId = variety.Id;
                entity.PokemonVariety = variety;

                var ability = abilities.SingleOrDefault(a => a.Name.EqualsExact(entity.Ability.Name));
                if (ability is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Ability {entity.Ability.Name}, skipping build.");
                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.AbilityId = ability.Id;
                entity.Ability = ability;

                AttachMoves(entity, moves);
                AttachItems(entity, items);
                AttachNatures(entity, natures);
            }

            return entities;
        }

        private void AttachMoves(Build build, List<Move> moves)
        {
            foreach (var moveOption in new List<MoveOption>(build.MoveOptions))
            {
                var move = moves.SingleOrDefault(m => m.Name.Equals(moveOption.Move.Name));

                if (move is null)
                {
                    Reporter.ReportError(Entity, build.IdHash,
                        $"No unique matching move could be found for move option {moveOption.Move.Name}. Skipping.");
                    build.MoveOptions.Remove(moveOption);
                    continue;
                }

                moveOption.Move = move;
                moveOption.MoveId = move.Id;
            }
        }

        private void AttachItems(Build build, List<Item> items)
        {
            foreach (var itemOption in new List<ItemOption>(build.ItemOptions))
            {
                var item = items.SingleOrDefault(i => i.Name.Equals(itemOption.Item.Name));

                if (item is null)
                {
                    Reporter.ReportError(Entity, build.IdHash,
                        $"No unique matching item could be found for item option {itemOption.Item.Name}. Skipping.");
                    build.ItemOptions.Remove(itemOption);
                    continue;
                }

                itemOption.Item = item;
                itemOption.ItemId = item.Id;
            }
        }

        private void AttachNatures(Build build, List<Nature> natures)
        {
            foreach (var natureOption in new List<NatureOption>(build.NatureOptions))
            {
                var nature = natures.SingleOrDefault(n => n.Name.Equals(natureOption.Nature.Name));

                if (nature is null)
                {
                    Reporter.ReportError(Entity, build.IdHash,
                        $"No unique matching nature could be found for nature option {natureOption.Nature.Name}. Skipping.");
                    build.NatureOptions.Remove(natureOption);
                    continue;
                }

                natureOption.Nature = nature;
                natureOption.NatureId = nature.Id;
            }
        }
    }
}