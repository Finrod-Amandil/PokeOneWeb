using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.WebApi.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace PokeOneWeb.WebApi.Services.Api.Impl
{
    public class MoveApiService : IMoveApiService
    {
        private readonly ReadModelDbContext _dbContext;

        public MoveApiService(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MoveDto> GetAllMoves()
        {
            return _dbContext.MoveReadModels
                .AsNoTracking()
                .Select(m => new MoveDto
                {
                    Name = m.Name,
                    ResourceName = m.ResourceName,
                    ElementalType = m.ElementalType,
                    DamageClass = m.DamageClass,
                    AttackPower = m.AttackPower,
                    Accuracy = m.Accuracy,
                    PowerPoints = m.PowerPoints,
                    Priority = m.Priority,
                    EffectDescription = m.EffectDescription
                });
        }

        public IEnumerable<MoveNameDto> GetAllMoveNames()
        {
            return _dbContext.MoveReadModels
                .AsNoTracking()
                .Select(m => new MoveNameDto
                {
                    Name = m.Name,
                    ResourceName = m.ResourceName
                });
        }
    }
}
