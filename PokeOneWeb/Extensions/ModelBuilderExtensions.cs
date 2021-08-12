using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static EntityTypeBuilder<T> HasIndexedHashes<T>(this EntityTypeBuilder<T> builder) where T : class, IHashedEntity
        {
            builder.HasIndex(x => x.Hash).IsUnique();
            builder.HasIndex(x => x.IdHash).IsUnique();

            return builder;
        }
    }
}
