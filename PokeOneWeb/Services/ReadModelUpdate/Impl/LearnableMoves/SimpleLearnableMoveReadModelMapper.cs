using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.LearnableMoves
{
    public class SimpleLearnableMoveReadModelMapper : IReadModelMapper<SimpleLearnableMoveReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public SimpleLearnableMoveReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SimpleLearnableMoveReadModel> MapFromDatabase()
        {
            var learnableMoves = _dbContext.LearnableMoves
                .Include(lm => lm.PokemonVariety)
                .Include(lm => lm.Move)
                .Include(lm => lm.LearnMethods)
                .AsNoTracking();

            foreach (var learnableMove in learnableMoves)
            {
                if (learnableMove.LearnMethods.Any(learnMethod => learnMethod.IsAvailable))
                {
                    yield return new SimpleLearnableMoveReadModel
                    {
                        ApplicationDbId = learnableMove.Id,
                        PokemonName = learnableMove.PokemonVariety.Name,
                        MoveName = learnableMove.Move.Name
                    };
                }
            }
        }
    }
}
