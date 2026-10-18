using System;
using System.Collections.Generic;
using To_Do_List.Interfaces;
using To_Do_List.Models;

namespace To_Do_List.Repositories
{
    public class TaskRepository : InMemoryRepository<ModelTodoTask>, ITaskRepository
    {
        public ModelTodoTask? GetById(int id)
        {
        return Find(t => t.TaskId == id);
        }

        public void  Update(ModelTodoTask task)
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));
                
            Update(t => t.TaskId == task.TaskId, task);
        }

        public bool Remove(int id)
        {
            return Remove(t => t.TaskId == id);
        }
        
    }
}