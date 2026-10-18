using System;
using System.Collections.Generic;

namespace To_Do_List.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);
        IReadOnlyList<T> GetAll();
        bool Update(Func<T, bool> predicate, T newItem);
        bool Remove(Func<T, bool> predicate);
        T? Find(Func<T, bool> predicate);
    }
}