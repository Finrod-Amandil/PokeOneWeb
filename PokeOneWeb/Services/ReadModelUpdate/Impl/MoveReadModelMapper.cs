using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
{
    public class MoveReadModelMapper : IReadModelMapper<MoveReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public MoveReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MoveReadModel> MapFromDatabase()
        {
            var moves = _dbContext.Moves
                .Include(m => m.DamageClass)
                .Include(m => m.ElementalType)
                .Where(m => m.DoInclude)
                .OrderBy(m => m.Name)
                .ToList();

            var index = 0;

            foreach (var move in moves)
            {
                index++;

                yield return new MoveReadModel
                {
                    Id = index,
                    Name = move.Name,
                    DamageClass = move.DamageClass.Name,
                    Type = move.ElementalType.Name,
                    AttackPower = move.AttackPower,
                    Accuracy = move.Accuracy,
                    PowerPoints = move.PowerPoints
                };
            }
        }
    }
}
