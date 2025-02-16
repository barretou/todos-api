using Microsoft.EntityFrameworkCore;
using TodoApi.Context;
using TodoApi.DTO;
using TodoApi.Models.Entities;

namespace TodoApi.Services
{
    public class TaskService
    {
        private readonly ApplicationContext _context;

        public TaskService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskModel> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? throw new KeyNotFoundException($"Task with ID {id} not found") : task;
        }

        public async Task<TaskModel> CreateTaskAsync(TaskCreateDTO taskDto) 
        {
            if (taskDto == null)
                throw new ArgumentNullException(nameof(taskDto));

            var task = new TaskModel
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                IsCompleted = taskDto.IsCompleted,
                CreatedDate = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> UpdateTaskAsync(int id, TaskModel updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (updatedTask == null)
                throw new ArgumentNullException(nameof(updatedTask));

            task.Name = updatedTask.Name;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
