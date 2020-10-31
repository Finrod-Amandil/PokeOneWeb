using System.Collections.Generic;

namespace PokeOneWeb.Services.DataUpdate
{
    public interface IDataUpdateService<T> where T : class
    {
        void Update(IEnumerable<T> newPlacedItems);
    }
}