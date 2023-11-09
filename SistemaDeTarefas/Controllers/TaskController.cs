using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("v1/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRespository _taskRespository;
        public TaskController(ITaskRespository taskRespository)
        {
            _taskRespository = taskRespository;
        }


        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetAllTasks()
        {
            List<TaskModel> tasks = await _taskRespository.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetById(int id)
        {
            TaskModel task = await _taskRespository.GetTaskById(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTask([FromBody] TaskModel taskModel)
        {
            TaskModel task = await _taskRespository.Add(taskModel);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTask(int id)
        {
            await _taskRespository.Delete(id);
            return Ok(true);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTask([FromBody] TaskModel task, int id)
        {
            TaskModel updatedTask = await _taskRespository.Update(task, id);
            return Ok(updatedTask);
        }
    }
}
