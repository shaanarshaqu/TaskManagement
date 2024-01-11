using TaskManagement.Dependancies;

namespace TaskManagement.Models
{
    public class Tasks:ITasks
    {

        List<TaskDto> tasks_list = new List<TaskDto>
        {
            new TaskDto{Id=1,Title="HTML",Description="jsdgfksjh",Status="Not Completed"},
            new TaskDto{Id=2,Title="CSS",Description="gfngh",Status="Not Completed"},
            new TaskDto{Id=3,Title="JavaScript",Description="kyu",Status="Not Completed"},
            new TaskDto{Id=4,Title="React",Description="wew",Status="Not Completed"},
            new TaskDto{Id=5,Title=".NET",Description="cbc",Status="Not Completed"},
        };

        public List<TaskDto> DisplayTasks()
        {
            return tasks_list;
        }
        public TaskDto DisplayTaskById(int id)
        {
            var task = tasks_list.FirstOrDefault(x=>x.Id == id);
            if (task == null)
            {
                return null;
            }
            return task;
        }
        public TaskDto UpdateTasks(int id, TaskDto task)
        {
            var perticular_task = tasks_list.FirstOrDefault(x=>x.Id == id);
            if(perticular_task == null)
            {
                return null;
            }
            perticular_task.Title = task.Title;
            perticular_task.Description = task.Description;
            perticular_task.Status = task.Status;
            return task;
        }



        public bool DeleteTasks(int id)
        {
            var perticular_task = tasks_list.FirstOrDefault(x => x.Id == id);
            if (perticular_task == null) 
            { 
                return false; 
            }
            tasks_list.Remove(perticular_task);
            return true;
        }

        public int AddTask(TaskDto task)
        {
            var isExist = tasks_list.FirstOrDefault(x => x.Title.ToLower() == task.Title.ToLower());
            if(isExist != null) 
            {
                return 0;
            }
            task.Id = tasks_list.OrderByDescending(x => x.Id).FirstOrDefault().Id+1;
            tasks_list.Add(task);
            return task.Id;
        }

    }
}
