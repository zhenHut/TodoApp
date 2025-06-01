using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TodoApp.Controls;
using TodoApp.Helper;

namespace TodoApp.Interfaces
{
    public interface INotificationService
    {
        public void Show(string message, MessagePanelType type, int durationMs = 3000);
    
    }
}
