using System;
using To_Do_List.Interfaces;

namespace To_Do_List.Commands
{
    public class CompleteCommand : Command
    {
        public CompleteCommand(ITaskService service) : base(service) { }

        public override void Execute()
        {
            // Вводим имя, а не id.
            var name = ReadNonEmptyLine("Введите название задачи: ");
            if (name is null)
            {
                Console.WriteLine("Название не может быть пустым.");
                return;
            }

            // Ищем задачу по имени.
            var task = _service!.FindByName(name);
            if (task is null)
            {
                Console.WriteLine("Задача не найдена.");
                return;
            }

            // Завершаем по найденному TaskId и проверяем результат.
            if (_service!.CompleteTask(task.TaskId))
                Console.WriteLine("Задача отмечена выполненной.");
            else
                Console.WriteLine("Не удалось завершить задачу (возможно, уже завершена).");
        }
    }
}