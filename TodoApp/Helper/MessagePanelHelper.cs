using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Controls;
using System.Windows.Media;
using System.Windows;

namespace TodoApp.Helper
{
    public static class MessagePanelHelper
    {

        public static MessagePanel Create(string message, MessagePanelType type, int durationMs= 3000)
        {
            var control = new MessagePanel();
            control.Show(message, GetBrush(type),durationMs);
            return control;
        }

        private static Brush GetBrush(MessagePanelType type)
        {
            return type switch
            {
                MessagePanelType.Success => Brushes.LightGreen,
                MessagePanelType.Error => Brushes.IndianRed,
                MessagePanelType.Warning => Brushes.Goldenrod,
                _ => Brushes.LightGray
            };
        }

    }
}
