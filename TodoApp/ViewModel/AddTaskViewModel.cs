using System.Windows.Input;
using TodoApp.Commands;
using TodoApp.Helper;
using TodoApp.Infrastructure;
using TodoApp.Interfaces;
using TodoApp.Model;

namespace TodoApp.ViewModel
{
    public class AddTaskViewModel : BaseViewModel, ICloseable
    {
        #region Constructor

        public AddTaskViewModel()
        {

            ConfirmCommand = new RelayCommand(Confirm_Execute, Confirm_CanExecute);
            CancelCommand = new RelayCommand(Cancel_Execute);

        }

        #endregion

        #region Fields

        private string _title = string.Empty;
        private string _description = string.Empty;
        private INotificationService? _notificationService;

        #endregion

        #region Properties

        private TasksViewModel? _tasksViewModel;


        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set { SetProperty(ref _description, value); }
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

        public void Init(TasksViewModel? TasksViewModel, INotificationService? notificationService)
        {
            _tasksViewModel = TasksViewModel;
            _notificationService = notificationService;
        }



        public bool Confirm_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        private void Confirm_Execute()
        {

            var newTask = new TaskItem { Title = Title, Description = Description };

            if (_tasksViewModel?.Tasks.Any(t => t.Title.Equals(newTask.Title, StringComparison.OrdinalIgnoreCase))?? false)
            {
                _notificationService?.Show("Aufgabe existiert bereits.", MessagePanelType.Error);
                return;
            }

            _tasksViewModel?.AddTask(newTask);
            RequestClose?.Invoke();

        }

        private void Cancel_Execute()
        {
            RequestClose?.Invoke();
        }

        #endregion
    }
}
