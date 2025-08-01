using ITasks.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ITasks.Services
{
    public interface ITaskService
    {
        ValueTask<List<ITask>> GetUserTasks(int UID);

        ValueTask<ITask?> GetTask(int TaskID);

        Task<int> AddTask(ITask task);

        Task<ITask> UpdateTask(int TaskID, ITask task);

        Task<ITask?> RemoveTask(int UID,int TaskID);
    }

    public class TaskService : ITaskService
    {
        private AppDbContext _context;

        public TaskService(AppDbContext context) {  _context = context; }

        public async Task<int> AddTask(ITask task)
        {
            ITask t = _context.Tasks.AddAsync(task).Result.Entity;
            await _context.SaveChangesAsync();
            return t.TaskID;
        }

        public async ValueTask<ITask?> GetTask(int TaskID)
        {
            return await _context.Tasks.FindAsync(TaskID);
        }

        public async ValueTask<List<ITask>> GetUserTasks(int UID)
        {
            List<UserTask> ut = await _context.userTasks.Where(t => t.UID == UID).ToListAsync();
            List<ITask> tasks = new List<ITask>();
            foreach (UserTask item in ut)
            {
                tasks.Add(_context.Tasks.Find(item.TaskID));
            }
            return tasks;
        }

        public async Task<ITask?> RemoveTask(int UID,int TaskID)
        {
            ITask? res = _context.Tasks.FindAsync(TaskID).Result;
            UserTask? ut = _context.userTasks.Where(i => i.TaskID == TaskID && i.UID == UID).First();
            _context.userTasks.Remove(ut);
            await _context.SaveChangesAsync();
            if (await _context.userTasks.FindAsync(TaskID) == null)
            {
                
                if (res == null)
                {
                    return null;
                }
                else
                {
                    _context.Tasks.Remove(res);
                    await _context.SaveChangesAsync();
                }
            }
            return res;
        }

        public async Task<ITask> UpdateTask(int TaskID, ITask task)
        {
            task.TaskID = TaskID;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
