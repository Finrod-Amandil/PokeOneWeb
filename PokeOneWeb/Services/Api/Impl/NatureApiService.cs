using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.Services.Api.Impl
{
    public class NatureApiService : INatureApiService
    {
        private readonly ReadModelDbContext _dbContext;

        public NatureApiService(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<NatureDto> GetAllNatures()
        {
            return _dbContext.NatureReadModels
                .AsNoTracking()
                .Select(n => new NatureDto
                {
                    Name = n.Name,
                    Effect = n.Effect,
                    Attack = n.Attack,
                    SpecialAttack = n.SpecialAttack,
                    Defense = n.Defense,
                    SpecialDefense = n.SpecialDefense,
                    Speed = n.Speed
                });
        }
    }
}
