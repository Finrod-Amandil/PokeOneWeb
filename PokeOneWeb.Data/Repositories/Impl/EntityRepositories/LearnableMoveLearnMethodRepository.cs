using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Data.Exceptions;

namespace PokeOneWeb.Data.Repositories.Impl.EntityRepositories
{
    public class LearnableMoveLearnMethodRepository : HashedEntityRepository<LearnableMoveLearnMethod>
    {
        public LearnableMoveLearnMethodRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override int Insert(ICollection<LearnableMoveLearnMethod> entities)
        {
            var insertedCount = base.Insert(entities);
            DeleteUnusedParentEntities();
            return insertedCount;
        }

        public override int Update(ICollection<LearnableMoveLearnMethod> entities)
        {
            var updatedCount = base.Update(entities);
            DeleteUnusedParentEntities();
            return updatedCount;
        }

        public override int DeleteByIdHashes(ICollection<string> idHashes)
        {
            var deletedCount = base.DeleteByIdHashes(idHashes);
            DeleteUnusedParentEntities();
            return deletedCount;
        }

        protected override ICollection<LearnableMoveLearnMethod> PrepareEntitiesForInsertOrUpdate(ICollection<LearnableMoveLearnMethod> entities)
        {
            AddOrUpdateLearnableMoves(entities);
            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.MoveLearnMethod));

            var learnableMoves = DbContext.LearnableMoves
                .Include(x => x.PokemonVariety)
                .Include(x => x.Move)
                .AsNoTracking()
                .ToDictionary(x => (x.PokemonVariety.Name, x.Move.Name), x => x.Id);

            var moveTutorMoves = DbContext.MoveTutorMoves
                .Include(x => x.Move)
                .Include(x => x.MoveTutor)
                .AsNoTracking()
                .ToDictionary(x => (x.MoveTutor.Name, x.Move.Name), x => x.Id);

            var verifiedEntities = new List<LearnableMoveLearnMethod>(entities);
            foreach (var entity in entities)
            {
                var canInsertOrUpdate = true;

                canInsertOrUpdate &= TrySetIdForName<LearnableMoveLearnMethodAvailability>(entity.AvailabilityName, id => entity.AvailabilityId = id);
                canInsertOrUpdate &= TryAddRequiredItem(entity);
                canInsertOrUpdate &= TryAddMoveTutorMove(entity, moveTutorMoves);
                canInsertOrUpdate &= TryAddMoveLearnMethod(entity);
                canInsertOrUpdate &= TryAddLearnableMove(entity, learnableMoves);

                if (!canInsertOrUpdate)
                {
                    verifiedEntities.Remove(entity);
                }
            }

            return base.PrepareEntitiesForInsertOrUpdate(verifiedEntities);
        }

        private void DeleteUnusedParentEntities()
        {
            DbContext.LearnableMoves
                .Where(x => x.LearnMethods.Count == 0)
                .DeleteFromQuery();
        }

        private void AddOrUpdateLearnableMoves(ICollection<LearnableMoveLearnMethod> entities)
        {
            var distinctLearnableMoves = entities
                .Select(x => x.LearnableMove)
                .DistinctBy(x => x.PokemonVarietyName + x.MoveName)
                .ToList();

            var existingLearnableMoves = DbContext.LearnableMoves
                .Include(x => x.PokemonVariety)
                .Include(x => x.Move)
                .AsNoTracking()
                .ToDictionary(x => (x.PokemonVariety.Name, x.Move.Name), x => x.Id);

            var verifiedLearnableMoves = new List<LearnableMove>(distinctLearnableMoves);
            foreach (var learnableMove in distinctLearnableMoves)
            {
                var canInsertOrUpdate = true;

                canInsertOrUpdate &= TrySetIdForName<PokemonVariety>(
                    learnableMove.PokemonVarietyName,
                    id => learnableMove.PokemonVarietyId = id);

                canInsertOrUpdate &= TrySetIdForName<Move>(
                    learnableMove.MoveName,
                    id => learnableMove.MoveId = id);

                // If entity exists, find Id so that EF updates the corresponding entry.
                // Else set Id to zero, which tells EF to treat it as new entry.
                learnableMove.Id = existingLearnableMoves.TryGetValue(
                    (learnableMove.PokemonVarietyName, learnableMove.MoveName),
                    out var learnableMoveId) ? learnableMoveId : 0;

                if (!canInsertOrUpdate)
                {
                    verifiedLearnableMoves.Remove(learnableMove);
                }
            }

            DbContext.LearnableMoves.UpdateRange(verifiedLearnableMoves);
            DbContext.SaveChanges();
        }

        private bool TryAddMoveLearnMethod(LearnableMoveLearnMethod entity)
        {
            var success = TrySetIdForName<MoveLearnMethod>(
                entity.MoveLearnMethod.Name,
                id => entity.MoveLearnMethodId = id);

            entity.MoveLearnMethod = null;

            return success;
        }

        private bool TryAddLearnableMove(LearnableMoveLearnMethod entity, Dictionary<(string, string), int> learnableMoves)
        {
            var success = learnableMoves.TryGetValue(
                (entity.LearnableMove.PokemonVarietyName, entity.LearnableMove.MoveName),
                out var learnableMoveId);

            if (!success)
            {
                var exception = new RelatedEntityNotFoundException(
                    nameof(LearnableMoveLearnMethod),
                    nameof(LearnableMove),
                    entity.LearnableMove.PokemonVarietyName + entity.LearnableMove.MoveName);

                ReportInsertOrUpdateException(typeof(LearnableMoveLearnMethod), exception);
            }
            else
            {
                entity.LearnableMoveId = learnableMoveId;
                entity.LearnableMove = null;
            }

            return success;
        }

        private bool TryAddRequiredItem(LearnableMoveLearnMethod entity)
        {
            if (string.IsNullOrWhiteSpace(entity.RequiredItemName))
            {
                return true;
            }

            var success = TrySetIdForName<Item>(
                entity.RequiredItemName,
                id => entity.RequiredItemId = id);

            entity.RequiredItem = null;

            return success;
        }

        private bool TryAddMoveTutorMove(LearnableMoveLearnMethod entity, Dictionary<(string, string), int> moveTutorMoves)
        {
            if (string.IsNullOrWhiteSpace(entity.MoveTutorName))
            {
                return true;
            }

            var success = moveTutorMoves.TryGetValue(
                (entity.MoveTutorName, entity.LearnableMove.MoveName),
                out var moveTutorMoveId);

            if (!success)
            {
                var exception = new RelatedEntityNotFoundException(
                    nameof(LearnableMoveLearnMethod),
                    nameof(MoveTutorMove),
                    entity.MoveTutorName + entity.LearnableMove.MoveName);

                ReportInsertOrUpdateException(typeof(LearnableMoveLearnMethod), exception);
            }
            else
            {
                entity.MoveTutorMoveId = moveTutorMoveId;
                entity.MoveTutorMove = null;
            }

            return success;
        }
    }
}