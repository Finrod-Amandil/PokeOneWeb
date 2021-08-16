using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.Natures
{
    public class NatureReadModelMapper : IReadModelMapper<NatureReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public NatureReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<NatureReadModel> MapFromDatabase()
        {
            var natures = _dbContext.Natures.AsNoTracking();

            foreach (var nature in natures)
            { 
                yield return new NatureReadModel
                {
                    Name = nature.Name,
                    ApplicationDbId = nature.Id,
                    Effect = nature.GetDescription(),
                    Attack = nature.Attack,
                    SpecialAttack = nature.SpecialAttack,
                    Defense = nature.Defense,
                    SpecialDefense = nature.SpecialDefense,
                    Speed = nature.Speed,
                };
            }
        }
    }
}
