using System.Collections.Generic;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Interfaces
{
    public interface IReadModelMapper<TReadModel> where TReadModel : class, IReadModel
    {
        List<TReadModel> MapFromDatabase();
    }
}