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

        private string _dialogTitle = "Aufgabe hinzufügen";
        private string _dialogButtonName = "Hinzufügen";
        private string _title = string.Empty;
        private string _description = string.Empty;
        private DateTime? _dueDate;
        private bool _isTaskUpdated;
        private TaskItem? _task;

        private INotificationService? _notificationService;
        private TasksViewModel? _tasksViewModel;
        #endregion

        #region Properties

        public string DialogTitle
        {
            get => _dialogTitle;
            set => SetProperty(ref _dialogTitle, value);
        }

        public string DialogButtonName
        {
            get => _dialogButtonName;
            set => SetProperty(ref _dialogButtonName, value);
        }

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

        public void Init(TasksViewModel? TasksViewModel, TaskItem? taskItem = null, bool isTaskUpdated = false)
        {
            _tasksViewModel = TasksViewModel;

            _isTaskUpdated = isTaskUpdated;

            if (IsTaskUpdated && taskItem != null)
            {
                _dialogTitle = "Aufgabe bearbeiten";
                _dialogButtonName =  "Speichern";
                _task = taskItem;
                _title = taskItem?.Title ?? string.Empty;
                _description = taskItem?.Description ?? string.Empty;
                _dueDate = taskItem?.DueDate;
            }

        }

        private bool Confirm_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(_title); 
        }

        private void Confirm_Execute()
        {
            if (_isTaskUpdated && _task != null)
            {
                _task.Title = _title;
                _task.Description = _description;
                _task.DueDate = _dueDate;
            }
            else
            {
                var newTask = new TaskItem
                {
                    Title = Title,
                    Description = Description,
                    DueDate = DueDate,
                };


                if (_tasksViewModel?.Tasks.Any(t => t.Title.Trim().Equals(newTask?.Title, StringComparison.OrdinalIgnoreCase)) ?? false)
                {
                    _notificationService?.Show("Aufgabe existiert bereits.", MessagePanelType.Error);
                    return;
                }

                _tasksViewModel?.AddTask(newTask);
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
