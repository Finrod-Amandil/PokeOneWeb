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

        public override void Insert(ICollection<LearnableMoveLearnMethod> entities)
        {
            // Update/Insert learnable moves
            var distinctLearnableMoves = entities
                .Select(x => x.LearnableMove)
                .DistinctBy(x => x.PokemonVarietyName + x.MoveName)
                .ToList();

            var existingLearnableMoves = DbContext.LearnableMoves
                .Include(x => x.PokemonVariety)
                .Include(x => x.Move)
                .AsNoTracking()
                .ToDictionary(x => (x.PokemonVariety.Name, x.Move.Name), x => x.Id);

            foreach (var learnableMove in distinctLearnableMoves)
            {
                learnableMove.PokemonVarietyId = GetRequiredIdForName<PokemonVariety>(learnableMove.PokemonVarietyName);
                learnableMove.MoveId = GetRequiredIdForName<Move>(learnableMove.MoveName);

                // If entity exists, find Id so that EF updates the corresponding entry.
                // Else set Id to zero, which tells EF to treat it as new entry.
                learnableMove.Id = existingLearnableMoves.TryGetValue(
                    (learnableMove.PokemonVarietyName, learnableMove.MoveName),
                    out var id) ? id : 0;
            }

            DbContext.LearnableMoves.UpdateRange(distinctLearnableMoves);
            DbContext.SaveChanges();

            AddOrUpdateRelatedEntitiesByName(entities.Select(x => x.MoveLearnMethod));

            var learnableMoves = DbContext.LearnableMoves
                .Include(x => x.PokemonVariety)
                .Include(x => x.Move)
                .AsNoTracking()
                .ToDictionary(x => (x.PokemonVariety.Name, x.Move.Name), x => x.Id);

            foreach (var entity in entities)
            {
                entity.MoveLearnMethodId = GetRequiredIdForName<MoveLearnMethod>(entity.MoveLearnMethod.Name);
                entity.MoveLearnMethod = null;

                entity.LearnableMoveId = learnableMoves.TryGetValue(
                    (entity.LearnableMove.PokemonVarietyName, entity.LearnableMove.MoveName),
                    out var id)
                    ? id
                    : throw new RelatedEntityNotFoundException(
                        nameof(LearnableMoveLearnMethod),
                        nameof(LearnableMove),
                        entity.LearnableMove.PokemonVarietyName + entity.LearnableMove.MoveName);

                entity.LearnableMove = null;
            }

            base.Insert(entities);
        }
    }
}