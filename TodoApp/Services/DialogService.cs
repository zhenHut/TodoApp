using System.Windows;
using TodoApp.Interfaces;

namespace TodoApp.Services
{
    public class DialogService : IDialogService
    {
        public bool? ShowDialog<TView, TViewModel>(Action<TViewModel>? init = null)
         where TView : Window, new()
         where TViewModel : class, new()
        {
            var view = new TView();
            var viewModel = new TViewModel();

            init?.Invoke(viewModel);

            if (view is Window window)
            {
                if (viewModel is ICloseable closeable)
                {
                    closeable.RequestClose += () => window.Close();
                }

                window.DataContext = viewModel;
                window.Owner = Application.Current.MainWindow;

                return window.ShowDialog();
            }

            return false;
        }
    }
}
