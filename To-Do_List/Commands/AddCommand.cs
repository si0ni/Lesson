using System;
using To_Do_List.Interfaces;

namespace To_Do_List.Commands
{
    public class AddCommand : Command
    {
        public AddCommand(ITaskService service) : base(service) { }
        

        public override void Execute()
        {
            // Хелпер из базового класса: читает строку, вернёт null если пусто.    
            var title = ReadNonEmptyLine("Введите название задачи: ");
            if (title is null)
            {
                Console.WriteLine("Название не может быть пустым.");
                return;
            }
    
            Console.Write("Введите описание (можно оставить пустым): ");
            var description = Console.ReadLine();
    
            // Пустое описание → null. Так семантически точнее, чем "".
            if (string.IsNullOrWhiteSpace(description))
                description = null;
    
             // _service! — null-forgiving: в этой команде сервис точно есть.
            _service!.AddTask(title, description);
            Console.WriteLine("Задача добавлена.");
        }
    }
}