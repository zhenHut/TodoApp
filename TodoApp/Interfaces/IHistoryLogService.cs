using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Interfaces
{
    public interface IHistoryLogService
    {
        void Created(string message);
        void Updated(string message);
        void Deleted(string message);
        void Completed(string message);
        void Restored(string message);

    }
}
