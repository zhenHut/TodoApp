using System.Windows.Media;
using TodoApp.Controls;
using TodoApp.Helper;
using TodoApp.Interfaces;

namespace TodoApp.Services
{
    public class NotificationService : INotificationService
    {
        #region Constructor
        public NotificationService(MessagePanel messagePanel)
        {
            _messagePanel = messagePanel;
        }

        #endregion

        #region Fields

        private readonly MessagePanel _messagePanel;

        #endregion

        #region Properties

        #endregion

        #region Methods

        public void Show(string message, MessagePanelType type, int durationMs = 3000)
        {
            _messagePanel.Show(message, GetBrush(type), durationMs);
        }

        private static Brush GetBrush(MessagePanelType type)
        {
            return type switch
            {
                MessagePanelType.Success => Brushes.Olive,
                MessagePanelType.Error => Brushes.IndianRed,
                MessagePanelType.Warning => Brushes.Goldenrod,
                _ => Brushes.Gray,
            };
        }

        #endregion
    }
}
