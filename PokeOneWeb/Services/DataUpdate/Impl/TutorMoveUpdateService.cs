using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.DataUpdate.Impl
{
    public class TutorMoveUpdateService : IDataUpdateService<TutorMove>
    {
        private readonly ApplicationDbContext _dbContext;

        public TutorMoveUpdateService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IEnumerable<TutorMove> newTutorMoves)
        {
            //Delete all existing placed items
            _dbContext.TutorMoves.RemoveRange(_dbContext.TutorMoves);
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.TutorMoves',RESEED, 0)");

            _dbContext.SaveChanges();

            _dbContext.AddRange(newTutorMoves);

            _dbContext.SaveChanges();
        }
    }
}
