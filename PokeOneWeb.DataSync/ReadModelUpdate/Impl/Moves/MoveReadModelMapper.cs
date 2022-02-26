using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.Moves
{
    public class MoveReadModelMapper : IReadModelMapper<MoveReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public MoveReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<MoveReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport report)
        {
            return _dbContext.Moves
                .Include(m => m.DamageClass)
                .Include(m => m.ElementalType)
                .AsNoTracking()
                .Where(m => m.DoInclude)
                .OrderBy(m => m.Name)
                .Select(move => new MoveReadModel
                {
                    ApplicationDbId = move.Id,
                    Name = move.Name,
                    ResourceName = move.ResourceName,
                    DamageClass = move.DamageClass.Name,
                    ElementalType = move.ElementalType.Name,
                    AttackPower = move.AttackPower,
                    Accuracy = move.Accuracy,
                    PowerPoints = move.PowerPoints,
                    Priority = move.Priority,
                    EffectDescription = move.Effect
                })
                .ToDictionary(x => x, _ => DbAction.Create);
        }
    }
}
