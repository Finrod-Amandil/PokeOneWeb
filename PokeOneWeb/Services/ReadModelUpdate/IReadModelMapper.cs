using System.Collections.Generic;

namespace PokeOneWeb.Services.ReadModelUpdate
{
    public interface IReadModelMapper<T>
    {
        IEnumerable<T> MapFromDatabase();
    }
}
