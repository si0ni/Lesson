using System;
using System.Collections.Generic;
using To_Do_List.Models;
using To_Do_List.Interfaces;

namespace To_Do_List.Commands
{
    public class ListCommand : Command
    {
        public ListCommand(ITaskService service) : base(service) { }
    
        public override void Execute()
        {
            Console.WriteLine("1 — все, 2 — завершённые, 3 — незавершённые, 4 — поиск");
            var choice = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("Неверный выбор.");
                return;
            }
            
            IReadOnlyList<ModelTodoTask> result;
            switch (choice)
            {
                case "1":
                    result = _service!.GetAllTasks();
                    break;
                case "2":
                    result = _service!.Filter(t => t.Status == Models.TaskStatus.Completed);
                    break;
                case "3":
                    result = _service!.Filter(t => t.Status != Models.TaskStatus.Completed);
                    break;
                case "4":
                    Console.Write("Поиск - введите название задачи: ");
                    var search = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(search))
                    {
                        Console.WriteLine("Пустой запрос.");
                        return;
                    }
                    result = _service!.Filter(t =>
                        t.TaskName.Contains(search, StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    return;
            }

            // Вывод результата
            if (result.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
                return;
            }

            foreach (var task in result)
                PrintTask(task);
        }
    }
}

