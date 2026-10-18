using System;
using To_Do_List.Interfaces;
using To_Do_List.Models;

namespace To_Do_List.Commands
{
    public abstract class Command
    {
        // Общая зависимость. Может быть null — ExitCommand сервис не нужен.
        protected readonly ITaskService? _service;

        // Конструктор с внедрением зависимости.
        // Принимает nullable: не все команды используют сервис.
        protected Command(ITaskService? service)
        {
            _service = service;
        }

        // Флаг выхода. Ставит только ExitCommand.
        public bool IsExit { get; protected set; }

        // Наследники обязаны реализовать.
        public abstract void Execute();

        // Общий хелпер: читает строку, возвращает null если пусто.
        protected static string? ReadNonEmptyLine(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? null : input;
        }

        // Общий хелпер: читает int, возвращает null если не удалось.
        protected static int? ReadInt(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            return int.TryParse(input, out var value) ? value : null;
        }

        // Общий хелпер: печатает задачу в едином формате.
        protected static void PrintTask(ModelTodoTask task)
        {
            var description = task.TaskDescription ?? "(без описания)";
            Console.WriteLine($"[{task.TaskId}] {task.TaskName} — {task.Status} — {description}");
        }
    }
}