using System.Collections.Generic;
using To_Do_List.Models;
using To_Do_List.Events;

namespace To_Do_List.Interfaces
{
    public interface ITaskService
    {
        event TaskChangedDelegate? TaskAdded;
        event TaskChangedDelegate? TaskCompleted;
        event TaskChangedDelegate? TaskNameChanged;
        event TaskChangedDelegate? TaskDescriptionChanged;

        void AddTask(string title, string? description = null);
        IReadOnlyList<ModelTodoTask> GetAllTasks();
        ModelTodoTask? GetTaskById(int id);

        bool CompleteTask(int id);

        ModelTodoTask? FindByName(string name);

        void ChangeName(int id, string newTitle);
        void ChangeDescription(int id, string? newDescription);

        IReadOnlyList<ModelTodoTask> Filter(TaskFilter filter);
    }
}