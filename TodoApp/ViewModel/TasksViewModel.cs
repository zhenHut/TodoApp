using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using TodoApp.Commands;
using TodoApp.Helper;
using TodoApp.Infrastructure;
using TodoApp.Interfaces;
using TodoApp.Model;
using TodoApp.Services;
using TodoApp.View;

namespace TodoApp.ViewModel
{
    public class TasksViewModel : BaseViewModel
    {
        #region Constructor

        public TasksViewModel()
        {
            _dialogService = ServiceLocator.GET<IDialogService>();
            _notificationService = ServiceLocator.GET<INotificationService>();
            _historyLogService = ServiceLocator.GET<IHistoryLogService>();
            _taskService = ServiceLocator.GET<ITaskService>();

            _ = LoadTaskAsync_Execute();

            DeleteTaskCommand = new RelayCommand<TaskItem>(DeleteTask_Execute, DeleteTask_CanExecute);
            OpenAddTaskCommand = new RelayCommand(OpenAddTaskWindow_Execute);
            SaveTaskCommand = new AsyncRelayCommand(SaveTasksAsync_Execute);
            LoadTaskCommand = new AsyncRelayCommand(LoadTaskAsync_Execute);
            UpdateTaskCommand = new RelayCommand(UpdateTask_Execute, UpdateTask_CanExecute);
        }

        #endregion

        #region Fields

        private readonly IDialogService _dialogService;
        private readonly INotificationService _notificationService;
        private readonly IHistoryLogService _historyLogService;
        private readonly ITaskService _taskService;

        private string _dialogTitle;
        private string _dialogButtonName;
        public ObservableCollection<TaskItem> Tasks => _taskService.Tasks;

        private TaskItem? _selectedTaskItem;
        #endregion

        #region  Properties

        public ICommand DeleteTaskCommand { get; }
        public ICommand OpenAddTaskCommand { get; }
        public ICommand SaveTaskCommand { get; }
        public ICommand LoadTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }


        public TaskItem? SelectedTaskItem
        {
            get => _selectedTaskItem;
            set
            {
                if (SetProperty(ref _selectedTaskItem, value))
                {
                    // Überprüft alle CanExecute Zustände
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }


        #endregion

        #region Methods

        private void OpenAddTaskWindow_Execute()
        {
            OpenTaskWindow();
        }

        private void OpenTaskWindow(TaskItem? taskItem = null, bool isUpdated = false)
        {
            _dialogService.ShowDialog<AddTaskView, AddTaskViewModel>(vm =>
            {
                vm.Init(this, taskItem, isUpdated);
            });

        }

        public void AddTask(TaskItem taskItem)
        {
            _taskService.AddTask(taskItem);
            _historyLogService.Created($"Neue Aufgabe '{taskItem.Title}' erstellt.");
        }

        private bool DeleteTask_CanExecute(TaskItem taskItem)
        {
            return taskItem != null;
        }

        private void DeleteTask_Execute(TaskItem taskItem)
        {
            _taskService.DeleteTask(taskItem);
            _historyLogService.Deleted($"Aufgabe '{taskItem.Title}' gelöscht.");
        }

        public async Task SaveTasksAsync_Execute()
        {
            try
            {
                if (Tasks != null && Tasks.Count != 0)
                {
                    await _taskService.SaveTaskAsync();

                    _notificationService.Show("Aufgaben gespeichert", MessagePanelType.Success);
                }
                else
                {
                    _notificationService?.Show("Keine Aufgaben zum Speichern", MessagePanelType.Warning);
                }
            }
            catch (Exception ex)
            {
                _notificationService?.Show($"Fehler beim Speichern: {ex.Message}", MessagePanelType.Error);

                //MessageBox.Show($"Fehler beim Speichern: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async Task LoadTaskAsync_Execute()
        {
            try
            {
                await _taskService.LoadTasksAsync();

                _notificationService.Show("Aufgaben geladen", MessagePanelType.Success);
            }
            catch (Exception ex)
            {
                _notificationService.Show($"Fehler beim Laden: {ex.Message}", MessagePanelType.Error);
            }
        }

        private bool UpdateTask_CanExecute()
        {

            return _selectedTaskItem != null;
        }

        private void UpdateTask_Execute()
        {
            OpenTaskWindow(SelectedTaskItem, true);
        }
        #endregion
    }
}
