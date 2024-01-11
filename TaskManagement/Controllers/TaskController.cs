using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Dependancies;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITasks _task_list;
        public TaskController(IConfiguration configuration, ITasks task_list) 
        {
            _configuration = configuration;
            _task_list = task_list;
        }


        
        [HttpGet(Name = "TaskList")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTasks()
        {
            return Ok(_task_list.DisplayTasks());
        }

        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTasks(int id)
        {
            var taskById = _task_list.DisplayTaskById(id);
            if(taskById == null)
            {
                return NotFound(id);
            }
            return Ok(taskById);
        }


        [HttpPost("AddTask")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTask([FromBody] TaskDto task)
        {
            if(task.Id > 0) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if(task  == null)
            {
                return BadRequest();
            }
            int  isAdded = _task_list.AddTask(task);
            if(isAdded == 0)
            {
                return BadRequest("Task already exist");
            }
            return CreatedAtRoute("TaskList", new { id = isAdded }, task);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTask(int id, [FromBody]TaskDto task)
        {
            if(id != task.Id)
            {
                return BadRequest($"{task.Id} is not maching to your request..");
            }
            if(task == null)
            {
                return BadRequest();
            }
            var isUpdated = _task_list.UpdateTasks(id, task);
            if (isUpdated == null)
            {
                return NotFound("user not found");
            }
            return Ok(isUpdated);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTask(int id)
        {
            bool isDeleted = _task_list.DeleteTasks(id);
            if (isDeleted == false)
            {
                return BadRequest("task not found");
            }
            return NoContent();
        }




        

    }
}
