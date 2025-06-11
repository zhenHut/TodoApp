using System.Windows.Input;
using TodoApp.Commands;
using TodoApp.Helper;
using TodoApp.Infrastructure;
using TodoApp.Interfaces;
using TodoApp.Model;
using TodoApp.Services;

namespace TodoApp.ViewModel
{
    public class AddTaskViewModel : BaseViewModel, ICloseable
    {
        #region Constructor

        public AddTaskViewModel()
        {
            _notificationService = ServiceLocator.GET<INotificationService>();
            ConfirmCommand = new RelayCommand(Confirm_Execute, Confirm_CanExecute);
            CancelCommand = new RelayCommand(Cancel_Execute);
        }

        #endregion

        #region Fields

        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime? _dueDate;
        private bool _isTaskUpdated;
        private TaskItem? _task;

        private INotificationService? _notificationService;
        private TasksViewModel? _tasksViewModel;
        #endregion

        #region Properties

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        public bool IsTaskUpdated
        {
            get => _isTaskUpdated;
            set => SetProperty(ref _isTaskUpdated, value);
        }
        #endregion

        #region Commands
        public ICommand ConfirmCommand { get; }

        public ICommand CancelCommand { get; }

        #endregion

        #region Events
        public event Action? RequestClose;
        #endregion

        #region Methods

        public void Init(TasksViewModel? TasksViewModel , TaskItem? taskItem, bool isTaskUpdated = false)
        {
            _tasksViewModel = TasksViewModel;
            
            _isTaskUpdated = isTaskUpdated;

            if (IsTaskUpdated && _task != null)
            {
                _task = taskItem;
                _title = taskItem?.Title ?? string.Empty;
                _description = taskItem?.Description ?? string.Empty;
                _dueDate = taskItem?.DueDate;
            }
           
        }

        private bool Confirm_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(_task?.Title);
        }

        private void Confirm_Execute()
        {
            if (!_isTaskUpdated && _task != null)
            {
                _task.Title = _title;
                _task.Description = _description;
                _task.DueDate = _dueDate;
            }else
            { 
                var newTask = new TaskItem
                {
                    Title = Title,
                    Description = Description,
                    DueDate = DueDate,
                };
                

                if (_tasksViewModel?.Tasks.Any(t => t.Title.Equals(newTask?.Title, StringComparison.OrdinalIgnoreCase)) ?? false)
                {
                    _notificationService?.Show("Aufgabe existiert bereits.", MessagePanelType.Error);
                    return;
                }

                _tasksViewModel?.AddTask(newTask?? new TaskItem()) ;
            }
            
                RequestClose?.Invoke();
        }

        private void Cancel_Execute()
        {
            RequestClose?.Invoke();
        }

        #endregion
    }
}
