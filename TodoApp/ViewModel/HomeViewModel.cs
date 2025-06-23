using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
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
            OpenTasksView = CollectionViewSource.GetDefaultView(_taskService.Tasks);

            LoadOpenTasks();
        }

     
        #endregion

        #region Fields

        private readonly ITaskService _taskService;

        #endregion

        #region Properties
        
        public ICollectionView OpenTasksView { get; init; }
        #endregion

        #region Methods


        public void LoadOpenTasks()
        {
            
            foreach (var task in _taskService.Tasks)
            {
                task.PropertyChanged += Task_PropertyChanged;
            }

            _taskService.Tasks.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (TaskItem newTask in e.NewItems)
                        newTask.PropertyChanged += Task_PropertyChanged;
                }

                if (e.OldItems != null)
                {
                    foreach (TaskItem oldTask in e.OldItems)
                        oldTask.PropertyChanged -= Task_PropertyChanged;
                }

                OpenTasksView.Refresh();
            };
        }
        private void Task_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaskItem.IsCompleted))
            {
                OpenTasksView?.Refresh();
            }
        }
        #endregion
    }
}
