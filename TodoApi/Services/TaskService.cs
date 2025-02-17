using Microsoft.EntityFrameworkCore;
using TodoApi.Context;
using TodoApi.DTO.Tasks;
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

        // Get all tasks
        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _context.Tasks.Include(t => t.User).ToListAsync();
        }

        public async Task<TaskModel> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found");

            return task;
        }

        public async Task<TaskModel> CreateTaskAsync(int userId, CreateTaskDTO DTO)
        {
            if (DTO == null)
                throw new ArgumentNullException(nameof(DTO));

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var task = new TaskModel
            {
                Name = DTO.Name,
                Description = DTO.Description,
                IsCompleted = DTO.IsCompleted,
                CreatedDate = DateTime.UtcNow,
                UserId = userId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> UpdateTaskAsync(int id, int userId, UpdateTaskDTO DTO)
        {
            if (DTO == null)
                throw new ArgumentNullException(nameof(DTO));

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {id} not found");

            if (_context.Users.Find(userId) == null)
            {
                throw new InvalidOperationException("User associated with this task not found");
            }

            task.Name = DTO.Name;
            task.Description = DTO.Description;
            task.IsCompleted = DTO.IsCompleted;
            task.UserId = userId;

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
