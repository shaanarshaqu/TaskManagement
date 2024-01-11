using TaskManagement.Dependancies;

namespace TaskManagement.Models
{
    public interface ITasks
    {
        List<TaskDto> DisplayTasks();
        TaskDto DisplayTaskById(int id);
        TaskDto UpdateTasks(int id, TaskDto task);
        bool DeleteTasks(int id);
        int AddTask(TaskDto task);
    }

}
