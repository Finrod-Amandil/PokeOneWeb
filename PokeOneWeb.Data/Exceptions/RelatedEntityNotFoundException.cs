using System;

namespace PokeOneWeb.Data.Exceptions
{
    /// <summary>
    /// Exception to communicate that an entity could not be inserted or updated to the data store
    /// because a related entity, whose existence is required for this entity to be modeled correctly,
    /// was not found in the data store. This may happen, if the related entity does not exist anymore
    /// (deleted), this entity was attempted to be inserted before the related entity, or the identifier
    /// for the related entity was mis-spelled.
    /// </summary>
    public class RelatedEntityNotFoundException : Exception
    {
        public RelatedEntityNotFoundException(
            string entityType, string relatedEntityType, string relatedEntityName)
            : base($"Related entity of type {relatedEntityType} with name {relatedEntityName} " +
                   $"was not found when inserting/updating an entity of type {entityType}.")
        {
        }
    }
}