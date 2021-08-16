using System.Collections.Generic;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Services.ReadModelUpdate
{
    public interface IReadModelRepository<T> where T : IReadModel
    {
        void Update(IEnumerable<T> entities);
    }
}
