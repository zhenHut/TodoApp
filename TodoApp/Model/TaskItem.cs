using TodoApp.Infrastructure;

namespace TodoApp.Model
{
    public class TaskItem : BaseNotifyPropertyChanged
    {
        #region Constructor
        public TaskItem()
        {
            _dueDate = DateTime.Today;
        }

        #endregion

        #region Fields
        private string _title = string.Empty;
        private string _description = string.Empty;
        private bool _isCompleted;
        private DateTime ?_dueDate;
       
        #endregion

        #region Properties

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if(SetProperty(ref _isCompleted, value))
                    OnPropertyChanged(nameof(IsOverDue));
            }
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                if (SetProperty(ref _dueDate, value))
                    OnPropertyChanged(nameof(IsOverDue));
            }
        }

        public bool IsOverDue => DueDate.HasValue && DueDate.Value.Date < DateTime.Today;
        
         
        #endregion

        #region Methods
      
        #endregion

        #region 

        #endregion
    }
}
