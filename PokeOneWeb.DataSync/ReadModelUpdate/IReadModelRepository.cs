using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels.Interfaces;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.ReadModelUpdate
{
    public interface IReadModelRepository<T> where T : IReadModel
    {
        void Update(IDictionary<T, DbAction> entities);
    }
}
