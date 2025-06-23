using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Data;
using TodoApp.Interfaces;
using TodoApp.Model;

namespace TodoApp.Services
{
    public class TaskService : ITaskService
    {
        #region Constructor

        public TaskService()
        { 
            _ = LoadTasksAsync();

        }

        #endregion

        #region Fields
        private readonly string _storagePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TodoApp", "tasks.json");

        #endregion

        #region Properties

        public ObservableCollection<TaskItem> Tasks { get; } = new();
        //public ICollectionView OpenTasksView { get; }

        #endregion

        #region Methods


        public async Task LoadTasksAsync()
        {
            if (!File.Exists(_storagePath))
                return;

            var json = await File.ReadAllTextAsync(_storagePath);
            var loaded = JsonSerializer.Deserialize<List<TaskItem>>(json);

            if (loaded != null)
            {
                Tasks.Clear();
                foreach (var task in loaded)
                {
                    Tasks.Add(task);
                }
            }
            
        }
        public async Task SaveTaskAsync()
        {
            var json = JsonSerializer.Serialize(Tasks);
            var directory = Path.GetDirectoryName(_storagePath);

            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllTextAsync(_storagePath, json);

        }

        public void AddTask(TaskItem task)
        {
            Tasks.Add(task);
        }

        public void DeleteTask(TaskItem task)
        {
            Tasks.Remove( task);
        }

    
        #endregion
    }

}
