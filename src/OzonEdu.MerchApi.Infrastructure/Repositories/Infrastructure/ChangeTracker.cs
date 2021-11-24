using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;
using OzonEdu.MerchApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Infrastructure
{
    public class ChangeTracker : IChangeTracker
    {
        public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();

        // Можно заменить на любую другую имплементацию. Не только через ConcurrentBag
        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField;

        public ChangeTracker()
        {
            _usedEntitiesBackingField = new ConcurrentBag<Entity>();
        }
        
        public void Track(Entity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), $"Can't track null {nameof(entity)} in change tracker");
            _usedEntitiesBackingField.Add(entity);
        }
    }
}