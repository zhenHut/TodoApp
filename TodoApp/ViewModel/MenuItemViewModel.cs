using TodoApp.Enums;
using TodoApp.Infrastructure;

namespace TodoApp.ViewModel
{
    public class MenuItemViewModel : BaseViewModel
    {
        #region Fields
        private string _title = string.Empty;
        private string _icon = string.Empty;
        private AppPageKey _pageKey;

        #endregion

        #region Properties

        public string Title 
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public AppPageKey PageKey
        {
            get => _pageKey;
            set => SetProperty(ref _pageKey, value);
        }

        
        #endregion

    }
}
