using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers
{
    public class MoveReadModelMapper : IReadModelMapper<MoveReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public MoveReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MoveReadModel> MapFromDatabase()
        {
            return _dbContext.Moves
                .Include(m => m.DamageClass)
                .Include(m => m.ElementalType)
                .AsNoTracking()
                .Where(m => m.DoInclude)
                .OrderBy(m => m.Name)
                .Select(move => new MoveReadModel
                {
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
                .ToList();
        }
    }
}