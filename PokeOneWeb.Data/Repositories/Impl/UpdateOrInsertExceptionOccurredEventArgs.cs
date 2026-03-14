using System;

namespace PokeOneWeb.Data.Repositories.Impl
{
    public class UpdateOrInsertExceptionOccurredEventArgs : EventArgs
    {
        public UpdateOrInsertExceptionOccurredEventArgs(Type entityType, Exception exception)
        {
            EntityType = entityType;
            Exception = exception;
        }

        public Type EntityType { get; }

        public Exception Exception { get; }
    }
}