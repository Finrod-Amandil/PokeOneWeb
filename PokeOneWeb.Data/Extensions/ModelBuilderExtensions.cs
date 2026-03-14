using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Adds Indexes and Unique Constraints on the Id-Hash and Content-Hash for a Hashed Entity.
        /// </summary>
        public static EntityTypeBuilder<T> HasIndexedHashes<T>(this EntityTypeBuilder<T> builder) where T : class, IHashedEntity
        {
            builder.HasIndex(x => x.Hash).IsUnique();
            builder.HasIndex(x => x.IdHash).IsUnique();

            return builder;
        }
    }
}