using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Extensions;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers
{
    public class NatureReadModelMapper : IReadModelMapper<NatureReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public NatureReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<NatureReadModel> MapFromDatabase()
        {
            return _dbContext.Natures
                .AsNoTracking()
                .Select(nature => new NatureReadModel
                {
                    Name = nature.Name,
                    Effect = nature.GetDescription(),
                    Attack = nature.Attack,
                    SpecialAttack = nature.SpecialAttack,
                    Defense = nature.Defense,
                    SpecialDefense = nature.SpecialDefense,
                    Speed = nature.Speed,
                })
                .ToList();
        }
    }
}