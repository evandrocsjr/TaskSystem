using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
    public class TaskRepository : ITaskRespository
    {
        private readonly SistemaDeTarefasDBContext _dbContext;

        public TaskRepository(SistemaDeTarefasDBContext sistemaDeTarefasDBContext)
        {
            _dbContext = sistemaDeTarefasDBContext;
        }

        public async Task<TaskModel> Add(TaskModel task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel task = await GetTaskById(id);
            _dbContext.Remove(task);
            return true;
        }

        public async Task<List<TaskModel>> GetAllTasks()
        {
            List<TaskModel> task = await _dbContext.Tasks
                .Include(t => t.User)
                .ToListAsync();
            return task;
        }

        public async Task<TaskModel> GetTaskById(int id)
        {
            return await _dbContext.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception($"Task {id} inexistente.");
        }

        public async Task<TaskModel> Update(TaskModel taskModel, int id)
        {
            TaskModel taskDb = await GetTaskById(id);
            taskDb.Description = taskModel.Description;
            taskDb.Status= taskModel.Status;
            taskDb.UserId = taskModel.UserId;
            taskDb.Name = taskModel.Name;

            _dbContext.Tasks.Update(taskDb);
            await _dbContext.SaveChangesAsync();

            return taskDb;
        }
    }
}
