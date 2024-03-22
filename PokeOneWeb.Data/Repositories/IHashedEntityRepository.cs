using System.Collections.Generic;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Repositories
{
    public interface IHashedEntityRepository<THashedEntity> : IRepository<THashedEntity> where THashedEntity : class, IHashedEntity
    {
        /// <summary>
        /// Loads the ID Hashes and Content Hashes for the entities that were imported from
        /// the given sheet.
        /// </summary>
        List<RowHash> GetHashes();

        /// <summary>
        /// Deletes hashed entities by looking up the corresponding ID Hashes.
        /// </summary>
        /// <param name="idHashes">The ID hashes calculated over the entities to be deleted.</param>
        /// <returns>How many entities were deleted.</returns>
        int DeleteByIdHashes(ICollection<string> idHashes);

        /// <summary>
        /// Updates those entities in the data store whose ID Hashes match with the ID Hashes of the
        /// given entities.
        /// </summary>
        /// <param name="entities">The hashed entities to update.</param>
        /// <returns>How many entities were successfully updated.</returns>
        int UpdateByIdHashes(ICollection<THashedEntity> entities);
    }
}