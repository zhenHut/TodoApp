using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace TodoApp.Controls
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TodoApp.Controls"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TodoApp.Controls;assembly=TodoApp.Controls"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Navigieren Sie zu diesem Projekt, und wählen Sie es aus.]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:MessagePanel/>
    ///
    /// </summary>
    public class MessagePanel : Border
    {
        #region Constructor

        public MessagePanel()
        {
            CornerRadius = new CornerRadius(10.0);
            Padding = new Thickness(15);
            Margin = new Thickness(15);
            Background = Brushes.LightGray;
            Visibility = Visibility.Collapsed;
            Opacity = 0.8;

            _messageText = new TextBlock
            {
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                FontSize = 15.0
            };

            this.Child = _messageText;
           
        }

        #endregion

        #region Fields

        private readonly TextBlock _messageText;
        private DispatcherTimer _timer;

        #endregion

        #region Methods


        public void Show(string message, Brush background, int durationInMilliseconds = 3000)
        {
            _messageText.Text = message;
            Background = background;
            Visibility = Visibility.Visible;
            Opacity = 0.8;


            // Timer zurücksetzen
            if (_timer != null && _timer.IsEnabled)
            {
                _timer.Stop();
                _timer.Tick -= Timer_Tick;
            }


            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(durationInMilliseconds)
            };

            _timer.Tick += Timer_Tick;
         

            _timer.Start();

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Tick -= Timer_Tick;

            // z. B. ausblenden
            var fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromMilliseconds(500),
                FillBehavior = FillBehavior.Stop
            };

            fadeOut.Completed += (s, _) =>
            {
                this.Visibility = Visibility.Collapsed;
                this.Opacity = 0.8; // zurücksetzen für nächsten Aufruf
            };

            this.BeginAnimation(OpacityProperty, fadeOut);
        }
        #endregion
    }
}
