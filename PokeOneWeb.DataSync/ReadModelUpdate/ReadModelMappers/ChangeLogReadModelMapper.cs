using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers
{
    public class ChangeLogReadModelMapper : IReadModelMapper<ChangeLogReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public ChangeLogReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ChangeLogReadModel> MapFromDatabase()
        {
            return _dbContext.ChangeLogs
                .AsNoTracking()
                .Select(changeLog => new ChangeLogReadModel
                {
                    ChangeLogId = changeLog.ChangeLogId,
                    Date = changeLog.Date,
                    Category = changeLog.Category,
                    Description = changeLog.Description,
                })
                .ToList();
        }
    }
}
