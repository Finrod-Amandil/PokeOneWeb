using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels.Interfaces;
using PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.Reporting;

namespace PokeOneWeb.Services.ReadModelUpdate
{
    public interface IReadModelRepository<T> where T : IReadModel
    {
        void Update(IDictionary<T, DbAction> entities);
    }
}
