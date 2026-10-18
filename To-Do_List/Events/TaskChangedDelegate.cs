using To_Do_List.Models;

namespace To_Do_List.Events
{
    public delegate void TaskChangedDelegate (ModelTodoTask task);
}