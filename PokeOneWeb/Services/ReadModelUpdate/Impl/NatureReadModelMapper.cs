using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using Z.EntityFramework.Plus;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
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
            var natures = _dbContext.Natures
                .IncludeOptimized(n => n.StatBoost);

            foreach (var nature in natures)
            {
                yield return new NatureReadModel
                {
                    Name = nature.Name,
                    Effect = CommonFormatHelper.GetNatureEffect(nature),
                    Atk = (int)nature.StatBoost.Attack,
                    Spa = (int)nature.StatBoost.SpecialAttack,
                    Def = (int)nature.StatBoost.Defense,
                    Spd = (int)nature.StatBoost.SpecialDefense,
                    Spe = (int)nature.StatBoost.Speed,
                    Hp = (int)nature.StatBoost.HitPoints
                };
            }
        }
    }
}
