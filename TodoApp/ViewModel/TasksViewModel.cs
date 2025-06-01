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
       
        public TasksViewModel(IDialogService dialogService, INotificationService notificationService)
        {
            _dialogService = dialogService;
            _notificationService = notificationService;

            _ = LoadTaskAsync_Execute();

            DeleteTaskCommand = new RelayCommand<TaskItem>(DeleteTask_Execute, DeleteTask_CanExecute);
            OpenAddTaskCommand = new RelayCommand(OpenAddTaskWindow_Execute);
            SaveTaskCommand = new AsyncRelayCommand(SaveTasksAsync_Execute);
            LoadTaskCommand = new AsyncRelayCommand(LoadTaskAsync_Execute);

            //_notificationService.Show("Laden erfolgreich bitch", MessagePanelType.Warning);

            Tasks.Add(new TaskItem { Title = "skdasdas" });
            Tasks.Add(new TaskItem { Title = "sdasgdg" });
            Tasks.Add(new TaskItem { Title = "sdasg" });
            Tasks.Add(new TaskItem { Title = "sdasggddg" });
            Tasks.Add(new TaskItem { Title = "sdasjdg" });
            Tasks.Add(new TaskItem { Title = "sdaqsgdg" });
            Tasks.Add(new TaskItem { Title = "sdasxgdg" });
            Tasks.Add(new TaskItem { Title = "sdkasgdg" });
            Tasks.Add(new TaskItem { Title = "sdaysgdg" });
            Tasks.Add(new TaskItem { Title = "sdasbgdg" });
            Tasks.Add(new TaskItem { Title = "sdaösgdg" });
            Tasks.Add(new TaskItem { Title = "sdasgdsg" });
            Tasks.Add(new TaskItem { Title = "sdnasgdg" });
            Tasks.Add(new TaskItem { Title = "sdaäsgdg" }


            );
        }


        #endregion

        #region Fields

        private IDialogService _dialogService;
        private INotificationService _notificationService;

        private readonly string _storagePath = Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoApp", "tasks.json");

        private bool _isTaskLoaded;
        #endregion

        #region  Properties

        public ObservableCollection<TaskItem> Tasks { get; } = new();

        public ICommand DeleteTaskCommand { get; }
        public ICommand OpenAddTaskCommand { get; }
        public ICommand SaveTaskCommand { get; }

        public ICommand LoadTaskCommand { get; }

        public bool IsTaskloaded
        {
            get => _isTaskLoaded;
            set =>SetProperty(ref _isTaskLoaded, value);
        }
        #endregion

        #region Methods

        private void OpenAddTaskWindow_Execute()
        {
            _dialogService.ShowDialog<AddTaskView, AddTaskViewModel>(vm =>
            {
                vm.Init(this, _notificationService);
            });
        }


        public void AddTask(TaskItem taskItem)
        {
            Tasks.Add(taskItem);
        }

        private bool DeleteTask_CanExecute(TaskItem taskItem)
        {
            return taskItem != null;
        }

        private void DeleteTask_Execute(TaskItem taskItem)
        {
            Tasks.Remove(taskItem);
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
                //MessageBox.Show($"Fehler beim Laden: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        #endregion
    }
}
