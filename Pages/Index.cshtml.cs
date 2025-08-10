using ITasks.Models;
using ITasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITasks.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private ICurrentUser _currentUser;
        private ITaskService _taskService;
        private AppDbContext _appDbContext;
        public IndexViewModel View = new();

        public IndexModel(ILogger<IndexModel> logger,ICurrentUser currentUser, ITaskService taskService,AppDbContext appDbContext)
        {
            _logger = logger;
            _currentUser = currentUser;
            _taskService = taskService;
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> OnGet()
        {
            if (_currentUser.GetCurrentUser() != null) {
                List<ITask> tl = await _taskService.GetUserTasks(_currentUser.GetCurrentUser()!.UID);
                foreach (var task in tl) {
                    View.Tasks.Add(new TaskView() {
                        TaskID = task.TaskID,
                        TaskName = task.TaskName,
                        DeadLine = task.DeadLine,
                        Description = task.Description,
                        isChecked = _appDbContext.UserTasks.Where(ut=>ut.TaskID==task.TaskID&&ut.UID==_currentUser.GetCurrentUser()!.UID).First().isChecked
                    });
                }
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }

    public class IndexViewModel
    {
        public List<TaskView> Tasks { get; set; } = new List<TaskView>();
    }

    public class TaskView:ITask
    {
        public bool isChecked {  get; set; }
    }
}

