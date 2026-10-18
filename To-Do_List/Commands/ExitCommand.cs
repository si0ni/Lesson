using System;

namespace To_Do_List.Commands
{
    public class ExitCommand : Command
    {
         // Сервис не нужен — передаём null в базовый конструктор.
        public ExitCommand() : base(null) { }

        public override void Execute()
        {
            Console.WriteLine("Выход.");
            IsExit = true; // Main увидит и завершит цикл.
        }
    }
}