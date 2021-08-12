using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using System.Collections.Generic;

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
            var natures = _dbContext.Natures;

            foreach (var nature in natures)
            {
                yield return new NatureReadModel
                {
                    Name = nature.Name,
                    Effect = CommonFormatHelper.GetNatureEffect(nature),
                    Atk = nature.Attack,
                    Spa = nature.SpecialAttack,
                    Def = nature.Defense,
                    Spd = nature.SpecialDefense,
                    Spe = nature.Speed,
                };
            }
        }
    }
}
