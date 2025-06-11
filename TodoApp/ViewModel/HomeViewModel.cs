using System.Collections.ObjectModel;
using TodoApp.Infrastructure;
using TodoApp.Interfaces;
using TodoApp.Model;
using TodoApp.Services;

namespace TodoApp.ViewModel
{
    public class HomeViewModel :BaseViewModel
    {
        #region Constructor

        public HomeViewModel() 
        {
            _taskService = ServiceLocator.GET<ITaskService>();
            LoadOpenTasks();
        }
        #endregion

        #region Fields

        private readonly ITaskService _taskService;

        #endregion

        #region Properties
        public ObservableCollection<TaskItem> OpenTasks { get; } = new();
        #endregion

        #region Methods

        private void LoadOpenTasks() 
        {
            var tasks = _taskService.LoadAllTasks();
            var openTasks = tasks.Where(t => !t.IsCompleted)
                .OrderBy(t => t.DueDate ?? DateTime.MaxValue);

            foreach (var task in openTasks)
                OpenTasks.Add(task);
        }

        #endregion
    }
}
