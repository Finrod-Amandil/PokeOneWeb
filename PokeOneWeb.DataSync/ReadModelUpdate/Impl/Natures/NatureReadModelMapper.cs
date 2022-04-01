using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Extensions;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.Natures
{
    public class NatureReadModelMapper : IReadModelMapper<NatureReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public NatureReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDictionary<NatureReadModel, DbAction> MapFromDatabase(SpreadsheetImportReport report)
        {
            return _dbContext.Natures
                .AsNoTracking()
                .Select(nature => new NatureReadModel
                {
                    Name = nature.Name,
                    ApplicationDbId = nature.Id,
                    Effect = nature.GetDescription(),
                    Attack = nature.Attack,
                    SpecialAttack = nature.SpecialAttack,
                    Defense = nature.Defense,
                    SpecialDefense = nature.SpecialDefense,
                    Speed = nature.Speed,
                })
                .ToDictionary(x => x, _ => DbAction.Create);
        }
    }
}