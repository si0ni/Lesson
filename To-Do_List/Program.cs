using System;
using To_Do_List.Commands;
using System.Collections.Generic;
using To_Do_List.Repositories;
using To_Do_List.Services;
using To_Do_List.Interfaces;

namespace To_Do_List
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Репозиторий. Создаём через специализированный TaskRepository,
            //    который наследует дженерик InMemoryRepository<ModelTodoTask>.
            //    Объявляем как ITaskRepository — работаем через контракт.
            ITaskRepository repository = new TaskRepository();

            // 2. Сервис. Внедрение зависимости через конструктор.
            ITaskService service = new TaskService(repository);

            // 3. Подписка на события сервиса через лямбда-выражения.
            //    Эти обработчики будут вызываться при поднятии событий в TaskService.
            service.TaskAdded += task =>
                Console.WriteLine($"[event] Добавлена: {task.TaskName}");

            service.TaskCompleted += task =>
                Console.WriteLine($"[event] Завершена: {task.TaskName}");

            service.TaskNameChanged += task =>
                Console.WriteLine($"[event] Переименована: {task.TaskName}");

            service.TaskDescriptionChanged += task =>
                Console.WriteLine($"[event] Изменено описание: {task.TaskName}");

            // 4. Команды. Создаются один раз, получают сервис через конструктор.
            var addCommand = new AddCommand(service);
            var listCommand = new ListCommand(service);
            var completeCommand = new CompleteCommand(service);
            var exitCommand = new ExitCommand();

            // Храним команды как Command — полиморфизм в действии.
            var commands = new Dictionary<string, Command>
            {
                { "1", addCommand },
                { "2", listCommand },
                { "3", completeCommand },
                { "4", exitCommand }
            };

            // 5. Цикл обработки ввода.
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 — добавить, 2 — список, 3 — завершить, 4 — выход");
                Console.Write("> ");

                var choice = Console.ReadLine();

                // Null-безопасность: пустой ввод — сообщаем и идём дальше.
                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.WriteLine("Неверный выбор.");
                    continue;
                }

                // Ищем команду. Если нет такой — сообщаем.
                if (!commands.TryGetValue(choice, out var command))
                {
                    Console.WriteLine("Неизвестная команда.");
                    continue;
                }

                command.Execute();

                // После выполнения проверяем: не Exit ли это?
                if (command.IsExit)
                    break;
            }
        }
    }
}