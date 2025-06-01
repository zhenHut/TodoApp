using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApp.Interfaces;
using TodoApp.Model;

namespace TodoApp.Services
{
    public class TaskService : ITaskService
    {

        #region Fields
        private readonly string _storagePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TodoApp", "tasks.json");

        #endregion

        #region Methods
        public IEnumerable<TaskItem> LoadAllTasks()
        {
            if (!File.Exists(_storagePath)) 
            {
                return Enumerable.Empty<TaskItem>();
            }

            var json = File.ReadAllText(_storagePath);
            var loaded = JsonSerializer.Deserialize<List<TaskItem>>(json);

            return loaded ?? Enumerable.Empty<TaskItem>();
        }
        #endregion
    }

}
