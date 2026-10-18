using System.Collections.Generic;
using To_Do_List.Models;

namespace To_Do_List.Interfaces
{
    public interface ITaskRepository 
    {
        void Add(ModelTodoTask task);
        ModelTodoTask? GetById(int id);
        IReadOnlyList<ModelTodoTask> GetAll();
        void Update(ModelTodoTask task);
        bool Remove(int id);
    }
}