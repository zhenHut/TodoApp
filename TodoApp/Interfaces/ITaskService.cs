using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Model;

namespace TodoApp.Interfaces
{
    public interface ITaskService
    {
        ObservableCollection<TaskItem> Tasks {  get; }
        //ICollectionView OpenTasksView { get; }

        Task LoadTasksAsync();
        Task SaveTaskAsync();

        void AddTask(TaskItem task);
        void DeleteTask(TaskItem task);

    }
}
