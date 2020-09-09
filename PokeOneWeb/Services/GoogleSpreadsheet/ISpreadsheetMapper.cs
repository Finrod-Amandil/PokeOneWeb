using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet
{
    public interface ISpreadsheetMapper<S, T> where S : ISpreadsheetDto where T : class
    {
        IEnumerable<T> Map(IEnumerable<S> dtos);
    }
}
