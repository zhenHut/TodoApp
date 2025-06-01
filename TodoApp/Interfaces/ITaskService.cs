using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Model;

namespace TodoApp.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> LoadAllTasks();
    }
}
