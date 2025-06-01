using TodoApp.Enums;
using TodoApp.Infrastructure;
using TodoApp.Interfaces;

namespace TodoApp.Services
{
    public class NavigationService : INavigationService
    {

        #region Fields
        // Hier wird die Factory (Erzeugungsfunktion) für ViewModels gespeichert
        private readonly Dictionary<AppPageKey, Func<BaseViewModel>> _factories = new();

        // Hier werden ViewModels gespeichert, nachdem sie einmal erzeugt wurden
        private readonly Dictionary<AppPageKey, BaseViewModel> _viewModelCache = new();

        #endregion

        #region Events
        
        public event EventHandler<BaseViewModel>? PageChanged;

        #endregion

        #region Methods

        public void Configure(AppPageKey pageKey, Func<BaseViewModel> viewModelFactory)
        {
            _factories[pageKey] = viewModelFactory;
        }

        public void Navigate(AppPageKey pageKey)
        {
            if (!_factories.TryGetValue(pageKey, out Func<BaseViewModel> ?factory)) 
                throw new ArgumentException($"No view registered for key {pageKey}");


            // ViewModel aus dem Cache holen, oder neu erzeugen und speichern
            if (!_viewModelCache.TryGetValue(pageKey, out var vm))
            {
                vm = factory();
                _viewModelCache[pageKey] = vm;
            }
             

            PageChanged?.Invoke(this,vm);
           
        }

        #endregion
    }
}
