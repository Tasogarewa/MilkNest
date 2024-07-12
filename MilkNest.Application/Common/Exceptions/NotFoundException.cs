using System;
using System.Collections.Generic;

namespace MilkNest.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object key)
            : base($"Entity \"{entityName}\" with key \"{key}\" was not found.")
        {
        }
        public NotFoundException(string entityName)
           : base($"Entities \"{entityName}\" was not found.")
        {
        }
        public static void Throw(object entity, object key)
        {
            if (entity == null || (key is Guid && (Guid)key == Guid.Empty))
            {
                throw new NotFoundException(nameof(entity), key);
            }
        }

        public static void ThrowRange(IEnumerable<object> entities)
        {
            if (entities == null || !entities.GetEnumerator().MoveNext())
            {
                throw new NotFoundException(nameof(entities));
            }
        }
    }
}