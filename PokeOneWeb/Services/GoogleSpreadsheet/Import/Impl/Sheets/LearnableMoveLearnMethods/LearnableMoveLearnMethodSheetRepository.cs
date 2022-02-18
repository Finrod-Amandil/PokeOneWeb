using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Sheets.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodSheetRepository 
        : SheetRepository<LearnableMoveLearnMethodSheetDto, LearnableMoveLearnMethod>
    {
        public LearnableMoveLearnMethodSheetRepository(
            ApplicationDbContext dbContext,
            ISheetRowParser<LearnableMoveLearnMethodSheetDto> parser,
            ISpreadsheetEntityMapper<LearnableMoveLearnMethodSheetDto, LearnableMoveLearnMethod> mapper,
            ISpreadsheetImportReporter reporter) : base(dbContext, parser, mapper, reporter) { }

        protected override DbSet<LearnableMoveLearnMethod> DbSet => DbContext.LearnableMoveLearnMethods;

        protected override Entity Entity => Entity.LearnableMoveLearnMethod;

        protected override List<LearnableMoveLearnMethod> AttachRelatedEntities(
            List<LearnableMoveLearnMethod> entities)
        {
            var varieties = DbContext.PokemonVarieties.ToList();
            var moves = DbContext.Moves.ToList();
            var moveTutorMoves = DbContext.MoveTutorMoves
                .IncludeOptimized(m => m.MoveTutor)
                .ToList();
            var items = DbContext.Items.ToList();
            var learnMethods = DbContext.MoveLearnMethods.ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];

                var variety = varieties.SingleOrDefault(v => v.Name.EqualsExact(entity.LearnableMove.PokemonVariety.Name));
                if (variety is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching PokemonVariety {entity.LearnableMove.PokemonVariety.Name}, " +
                        $"skipping learnable move learn method.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LearnableMove.PokemonVarietyId = variety.Id;
                entity.LearnableMove.PokemonVariety = variety;

                var move = moves.SingleOrDefault(m => m.Name.EqualsExact(entity.LearnableMove.Move.Name));
                if (move is null)
                {
                    Reporter.ReportError(Entity, entity.IdHash,
                        $"Could not find matching Move {entity.LearnableMove.Move.Name}, " +
                        $"skipping learnable move learn method.");

                    entities.Remove(entity);
                    i--;
                    continue;
                }

                entity.LearnableMove.MoveId = move.Id;
                entity.LearnableMove.Move = move;

                var learnMethod = learnMethods.SingleOrDefault(l => l.Name.EqualsExact(entity.MoveLearnMethod.Name));
                if (learnMethod != null)
                {
                    entity.MoveLearnMethodId = learnMethod.Id;
                    entity.MoveLearnMethod = learnMethod;
                }

                if (entity.RequiredItem != null)
                {
                    var item = items.SingleOrDefault(i => i.Name.EqualsExact(entity.RequiredItem.Name));
                    if (item is null)
                    {
                        Reporter.ReportError(Entity, entity.IdHash,
                            $"Could not find matching Item {entity.RequiredItem.Name}, " +
                            $"skipping learnable move learn method.");

                        entities.Remove(entity);
                        i--;
                        continue;
                    }

                    entity.RequiredItemId = item.Id;
                    entity.RequiredItem = item;
                }

                if (entity.MoveTutorMove != null)
                {
                    var tutorMove = moveTutorMoves.SingleOrDefault(m => 
                        m.MoveTutor.Name.EqualsExact(entity.MoveTutorMove.MoveTutor.Name) &&
                        m.Move.Name.EqualsExact(entity.MoveTutorMove.Move.Name));
                    if (tutorMove is null)
                    {
                        Reporter.ReportError(Entity, entity.IdHash,
                            $"Could not find matching move tutor move {entity.MoveTutorMove.MoveTutor.Name}/" +
                            $"{entity.MoveTutorMove.Move.Name}, skipping learnable move learn method.");

                        entities.Remove(entity);
                        i--;
                        continue;
                    }

                    entity.MoveTutorMoveId = tutorMove.Id;
                    entity.MoveTutorMove = tutorMove;
                }
            }

            return entities;
        }
    }
}
