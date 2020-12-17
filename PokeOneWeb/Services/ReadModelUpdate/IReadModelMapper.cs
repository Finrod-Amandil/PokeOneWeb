using System.Collections.Generic;

namespace PokeOneWeb.Services.ReadModelUpdate
{
    public interface IReadModelMapper<out T>
    {
        IEnumerable<T> MapFromDatabase();
    }
}
