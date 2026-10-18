using System;

namespace To_Do_List.Models
{
    public enum TaskStatus //enum необходимо создать это не просто переменная 
    {
        New, InProgress, Deferred, Completed 
    }
    
    public class ModelTodoTask
    {
        public int TaskId { get; init; } //Id задачи
        public string TaskName { get; set; } //Имя задачи
        public string? TaskDescription { get; set; } //Описание задачи
        public TaskStatus Status { get; set; } //Статус задачи
        public DateTime TaskCreatedAt { get; init; } //Время создания задачи
        public DateTime? TaskCompletedAt { get; set; } //Время завершения задачи

        //init позволяет создать переменную которую можно задать только при создании объекта
        //set значение может меняться


        public ModelTodoTask(string taskName, string? taskDescription = null)
        {
            TaskId = Random.Shared.Next();
            TaskName = taskName;
            TaskDescription = taskDescription;
            Status = TaskStatus.New;
            TaskCreatedAt = DateTime.UtcNow;            
        }
    }
}