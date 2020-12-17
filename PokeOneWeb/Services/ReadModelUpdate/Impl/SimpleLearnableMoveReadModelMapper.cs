using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
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
                .Include(lm => lm.LearnMethods);

            foreach (var learnableMove in learnableMoves)
            {
                if (learnableMove.LearnMethods.Any(learnMethod => learnMethod.IsAvailable))
                {
                    yield return new SimpleLearnableMoveReadModel
                    {
                        PokemonName = learnableMove.PokemonVariety.Name,
                        MoveName = learnableMove.Move.Name
                    };
                }
            }
        }
    }
}
