using System;

namespace PokeOneWeb.Data.Exceptions
{
    public class RelatedEntityNotFoundException : Exception
    {
        public RelatedEntityNotFoundException(
            string entityType, string relatedEntityType, string relatedEntityName)
            : base($"Related entity of type {relatedEntityType} with name {relatedEntityName}" +
                   $"was not found when inserting/updating an entity of type {entityType}.")
        {
        }
    }
}