using System;
using System.Collections.Generic;
using System.Linq;
using To_Do_List.Interfaces;

namespace To_Do_List.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    {

        private readonly List<T> _items = new();

        public void Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            
            _items.Add(item);
        }

        public IReadOnlyList<T> GetAll()
        {
            return _items.ToList();
        }

        public bool Remove(Func<T, bool> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            
            var item = _items.FirstOrDefault(predicate);
            if (item is null)
                return false;
            
            return _items.Remove(item);
        }

        public bool Update(Func<T, bool> predicate, T newItem)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            if (newItem is null)
                throw new ArgumentNullException(nameof(newItem));

            var index = _items.FindIndex(x => predicate(x));
            if (index < 0)
                return false;
            
            _items[index] = newItem;
                return true;
        }

        public T? Find(Func<T, bool> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return _items.FirstOrDefault(predicate);
        }
    }
}