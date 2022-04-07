using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate
{
    public interface IReadModelRepository<T> where T : IReadModel
    {
        void Update(IDictionary<T, DbAction> entities);
    }
}