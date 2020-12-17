using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.Services.DataUpdate.Impl
{
    public class TutorMoveUpdateService : IDataUpdateService<TutorMove>
    {
        private readonly ApplicationDbContext _dbContext;

        public TutorMoveUpdateService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IEnumerable<TutorMove> newTutorMoves, bool deleteExisting = true)
        {
            if (deleteExisting)
            {
                //Delete all existing placed items
                _dbContext.TutorMoves.RemoveRange(_dbContext.TutorMoves);
                _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.TutorMoves',RESEED, 0)");

                _dbContext.SaveChanges();
            }
            _dbContext.AddRange(newTutorMoves);

            _dbContext.SaveChanges();
        }
    }
}
