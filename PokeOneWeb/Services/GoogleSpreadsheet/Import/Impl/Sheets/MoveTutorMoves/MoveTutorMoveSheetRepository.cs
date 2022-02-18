using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves
{
    public class MoveTutorMoveSheetRepository : SheetRepository<MoveTutorMoveSheetDto, MoveTutorMove>
    {
        public MoveTutorMoveSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<MoveTutorMoveSheetDto> parser,
            ISpreadsheetEntityMapper<MoveTutorMoveSheetDto, MoveTutorMove> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<MoveTutorMove> DbSet => DbContext.MoveTutorMoves;

        protected override Entity Entity => Entity.MoveTutorMove;

        protected override List<MoveTutorMove> AttachRelatedEntities(List<MoveTutorMove> entities)
        {
            var moveTutors = DbContext.MoveTutors.ToList();
            var moves = DbContext.Moves.ToList();
            var currencies = DbContext.Currencies
                .IncludeOptimized(c => c.Item)
                .ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var moveTutor = moveTutors.SingleOrDefault(m => m.Name.EqualsExact(entity.MoveTutor.Name));
                if (moveTutor is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching MoveTutor {entity.MoveTutor.Name}, skipping move tutor move.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.MoveTutorId = moveTutor.Id;
                entity.MoveTutor = moveTutor;

                var move = moves.SingleOrDefault(m => m.Name.EqualsExact(entity.Move.Name));
                if (move is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Move {entity.MoveTutor.Name}, skipping move tutor move.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.MoveId = move.Id;
                entity.Move = move;

                for (var j = 0; j < entity.Price.Count; j++)
                {
                    var price = entity.Price[j];

                    var currency = currencies.SingleOrDefault(c => 
                        c.Item.Name.EqualsExact(price.CurrencyAmount.Currency.Item.Name));

                    if (currency is null)
                    {
                        Reporter.ReportError(Entity, entity.IdHash,
                            "Could not find matching Currency " +
                            $"{price.CurrencyAmount.Currency.Item.Name}, " +
                            "skipping move tutor move price.");

                        entity.Price.Remove(price);
                        j--;
                        continue;
                    }

                    price.CurrencyAmount.CurrencyId = currency.Id;
                    price.CurrencyAmount.Currency = currency;
                }
            }

            return entities;
        }
    }
}
