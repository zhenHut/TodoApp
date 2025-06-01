using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoApp.Infrastructure;

namespace TodoApp.Services
{
    public interface IDialogService
    {
        bool? ShowDialog<TView, TViewModel>(Action<TViewModel>? init = null)
    where TView : Window, new()
    where TViewModel : class, new();
    }
}
