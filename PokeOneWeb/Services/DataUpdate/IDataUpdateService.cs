using System.Collections.Generic;

namespace PokeOneWeb.Services.DataUpdate
{
    public interface IDataUpdateService<T> where T : class
    {
        /// <summary>
        /// Compares the given set of entries, including related entries where applicable, to the data currently stored in the database.
        /// The database entries will be updated to match the given new data. Entries for which no match could be found in the database
        /// will be newly created.
        /// </summary>
        /// <param name="newData">The (non-tracked) updated set of data, which should be applied to the database.</param>
        /// <param name="removeDatabaseExclusiveEntries">Whether entries which are not present in the given set of updated
        /// data, but are present in the database, should be deleted from the database or left unchanged.</param>
        /// <returns>Returns the updated set of data, as is present in the database after updating.</returns>
        List<T> Update(List<T> newData, bool removeDatabaseExclusiveEntries = true);
    }
}
