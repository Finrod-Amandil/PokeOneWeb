using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.LearnableMoves
{
    public class SimpleLearnableMoveReadModelMapper : IReadModelMapper<SimpleLearnableMoveReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public SimpleLearnableMoveReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<SimpleLearnableMoveReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport importReport)
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
                    MoveResourceName = learnableMove.Move.ResourceName
                })
                .ToDictionary(x => x, _ => DbAction.Create);
        }
    }
}