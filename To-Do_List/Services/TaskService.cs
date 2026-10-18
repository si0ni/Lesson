using System;
using System.Linq;
using System.Collections.Generic;
using To_Do_List.Events;
using To_Do_List.Interfaces;
using To_Do_List.Models;

namespace To_Do_List.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public event TaskChangedDelegate? TaskAdded;
        public event TaskChangedDelegate? TaskCompleted;
        public event TaskChangedDelegate? TaskNameChanged;
        public event TaskChangedDelegate? TaskDescriptionChanged;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void AddTask(string title, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название задачи не может быть пустым.", nameof(title));

            var task = new ModelTodoTask(title, description);
            _repository.Add(task);
            TaskAdded?.Invoke(task);
        }

        public IReadOnlyList<ModelTodoTask> GetAllTasks()
        {
            return _repository.GetAll();
        }

        public ModelTodoTask? GetTaskById(int id)
        {
            return _repository.GetById(id);
        }

        // Теперь возвращает bool: вызывающий код узнаёт, получилось или нет.
        public bool CompleteTask(int id)
        {
            var task = _repository.GetById(id);

            // Задача не найдена — сообщаем неудачу.
            if (task is null)
                return false;

            // Уже завершена — повторно событие не поднимаем, сообщаем неудачу.
            if (task.Status == Models.TaskStatus.Completed)
                return false;

            task.Status = Models.TaskStatus.Completed;
            task.TaskCompletedAt = DateTime.UtcNow;
            _repository.Update(task);

            TaskCompleted?.Invoke(task);
            return true;
        }

        // Поиск задачи по имени, без учёта регистра.
        public ModelTodoTask? FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return _repository.GetAll()
                .FirstOrDefault(t =>
                    t.TaskName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void ChangeName(int id, string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Введите корректное название.", nameof(newTitle));

            var task = _repository.GetById(id);
            if (task is null) return;

            task.TaskName = newTitle;
            _repository.Update(task);
            TaskNameChanged?.Invoke(task);
        }

        public void ChangeDescription(int id, string? newDescription)
        {
            var task = _repository.GetById(id);
            if (task is null) return;

            task.TaskDescription = newDescription;
            _repository.Update(task);
            TaskDescriptionChanged?.Invoke(task);
        }

        public IReadOnlyList<ModelTodoTask> Filter(TaskFilter filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));

            return _repository.GetAll().Where(t => filter(t)).ToList();
        }
    }
}