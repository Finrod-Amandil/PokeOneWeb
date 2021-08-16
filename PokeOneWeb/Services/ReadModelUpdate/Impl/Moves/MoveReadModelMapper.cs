using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.Moves
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
                .AsNoTracking()
                .Where(m => m.DoInclude)
                .OrderBy(m => m.Name)
                .ToList();

            foreach (var move in moves)
            {
                yield return new MoveReadModel
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
                };
            }
        }
    }
}
