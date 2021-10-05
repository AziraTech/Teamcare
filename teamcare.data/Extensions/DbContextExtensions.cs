using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using teamcare.data.Entities;

namespace teamcare.data.Extensions
{
    public static class DbContextExtensions
    {
        public static void DetachLocal<T>(this DbContext context, T t, Guid entryId)
            where T : BaseEntity
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }

            context.Entry(t).State = EntityState.Modified;
        }

        public static void AttachLocal<T>(this DbContext context, T t, Guid entryId)
            where T : BaseEntity
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State=EntityState.Modified;
            }

            context.Entry(t).State = EntityState.Modified;
        }
    }
}
