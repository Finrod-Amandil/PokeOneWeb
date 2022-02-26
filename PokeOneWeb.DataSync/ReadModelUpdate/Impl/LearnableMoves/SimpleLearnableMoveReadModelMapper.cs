using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.LearnableMoves
{
    public class SimpleLearnableMoveReadModelMapper : IReadModelMapper<SimpleLearnableMoveReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public SimpleLearnableMoveReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<SimpleLearnableMoveReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport report)
        {
            return _dbContext.LearnableMoves
                .Include(lm => lm.PokemonVariety)
                .Include(lm => lm.Move)
                .Include(lm => lm.LearnMethods)
                .AsNoTracking()
                .ToList()
                .Where(learnableMove => learnableMove.LearnMethods.Any(learnMethod => learnMethod.IsAvailable))
                .Select(learnableMove => new SimpleLearnableMoveReadModel
                {
                    ApplicationDbId = learnableMove.Id,
                    PokemonVarietyApplicationDbId = learnableMove.PokemonVariety.Id,
                    MoveResourceName = learnableMove.Move.ResourceName
                })
                .ToDictionary(x => x, _ => DbAction.Create);
        }
    }
}
