using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import
{
    public interface ISpreadsheetEntityMapper<in S, out T> where S : ISpreadsheetEntityDto where T : class
    {
        IEnumerable<T> Map(IEnumerable<S> dtos);
    }
}
