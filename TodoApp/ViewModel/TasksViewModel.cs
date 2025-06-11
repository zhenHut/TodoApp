using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
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

        private readonly string _storagePath = Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoApp", "tasks.json");


        private TaskItem _selectedTaskItem;
        private bool _isTaskLoaded;
        #endregion

        #region  Properties

        public ObservableCollection<TaskItem> Tasks { get; } = new();



        public ICommand DeleteTaskCommand { get; }
        public ICommand OpenAddTaskCommand { get; }
        public ICommand SaveTaskCommand { get; }
        public ICommand LoadTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }


        public TaskItem SelectedTaskItem
        {
            get => _selectedTaskItem;
            set { 
                if(SetProperty(ref _selectedTaskItem, value))
                {
                    // Überprüft alle CanExecute Zustände
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }


        public bool IsTaskloaded
        {
            get => _isTaskLoaded;
            set => SetProperty(ref _isTaskLoaded, value);
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
            Tasks.Add(taskItem);
            _historyLogService.Created($"Neue Aufgabe '{taskItem.Title}' erstellt.");
        }

        private bool DeleteTask_CanExecute(TaskItem taskItem)
        {
            return taskItem != null;
        }

        private void DeleteTask_Execute(TaskItem taskItem)
        {
            Tasks.Remove(taskItem);
            _historyLogService.Deleted($"Aufgabe '{taskItem.Title}' gelöscht.");
        }

        public async Task SaveTasksAsync_Execute()
        {
            try
            {
                var directory = Path.GetDirectoryName(_storagePath);

                if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (Tasks != null && Tasks.Count != 0)
                {
                    var json = JsonSerializer.Serialize(Tasks);
                    await File.WriteAllTextAsync(_storagePath, json);

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
                if (!File.Exists(_storagePath))
                    return;

                var json = await File.ReadAllTextAsync(_storagePath);
                var loaded = JsonSerializer.Deserialize<List<TaskItem>>(json);

                if (loaded != null)
                {
                    Tasks.Clear();
                    foreach (var item in loaded)
                        Tasks.Add(item);
                }

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


        //private void UpdateTask(TaskItem taskItem)
        //{
        //    Tasks.
        //}
        #endregion
    }
}
