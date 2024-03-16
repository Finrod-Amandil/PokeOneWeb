using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers
{
    public class SimpleLearnableMoveReadModelMapper : IReadModelMapper<SimpleLearnableMoveReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public SimpleLearnableMoveReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SimpleLearnableMoveReadModel> MapFromDatabase()
        {
            return _dbContext.LearnableMoves
                .Include(lm => lm.PokemonVariety)
                .Include(lm => lm.Move)
                .Include(lm => lm.LearnMethods)
                .AsNoTracking()
                .AsEnumerable()
                .Where(learnableMove => learnableMove.LearnMethods.Any(learnMethod => learnMethod.IsAvailable))
                .Select(learnableMove => new SimpleLearnableMoveReadModel
                {
                    ApplicationDbId = learnableMove.Id,
                    PokemonVarietyApplicationDbId = learnableMove.PokemonVariety.Id,
                    Name = learnableMove.PokemonVariety.Name,
                    ResourceName = learnableMove.PokemonVariety.ResourceName,
                    MoveResourceName = learnableMove.Move.ResourceName
                })
                .ToList();
        }
    }
}