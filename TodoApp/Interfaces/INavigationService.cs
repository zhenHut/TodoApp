using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Enums;
using TodoApp.Infrastructure;

namespace TodoApp.Interfaces
{
    public interface INavigationService
    {
        void Configure(AppPageKey page, Func<BaseViewModel> viewModelFactory);
        void Navigate (AppPageKey pageKey);
        event EventHandler<BaseViewModel> PageChanged;
    }
}
