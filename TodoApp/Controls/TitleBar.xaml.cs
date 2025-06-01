using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TodoApp.Controls
{
    /// <summary>
    /// Interaktionslogik für TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        #region Constructor
        public TitleBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Dependency Properties

        #region Border

        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register(nameof(BorderBackground), typeof(Brush), typeof(TitleBar), new PropertyMetadata(Brushes.LightGray));

        public static readonly DependencyProperty BorderStyleProperty =
            DependencyProperty.Register(nameof(BorderStyle), typeof(Style), typeof(TitleBar));

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register(nameof(BorderCornerRadius), typeof(CornerRadius), typeof(TitleBar), new PropertyMetadata(new CornerRadius(0)));

        #endregion

        #region Icon
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(TitleBar));

        public static readonly DependencyProperty IconStyleProperty =
            DependencyProperty.Register(nameof(IconStyle), typeof(Style), typeof(TitleBar));

        #endregion


        #region Title

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(TitleBar), new PropertyMetadata("Title"));

        public static readonly DependencyProperty TitleStyleProperty =
            DependencyProperty.Register(nameof(TitleStyle), typeof(Style), typeof(TitleBar));

        public static readonly DependencyProperty TitleHorizontalAligmentProperty =
            DependencyProperty.Register(nameof(TitleHorizontalAligment), typeof(HorizontalAlignment), typeof(TitleBar), new PropertyMetadata(HorizontalAlignment.Center));

        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register(nameof(TitleForeground), typeof(Brush), typeof(TitleBar), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty TitleFontsizeProperty =
            DependencyProperty.Register(nameof(TitleFontsize), typeof(double), typeof(TitleBar), new PropertyMetadata(15.0));
        #endregion

        #region Minimize Button

        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register(nameof(ButtonStyle), typeof(Style), typeof(TitleBar));

        public static readonly DependencyProperty MinimizeCommandProperty =
            DependencyProperty.Register(nameof(MinimizeCommand), typeof(ICommand), typeof(TitleBar));

        public static readonly DependencyProperty ShowMinimizeButtonProperty =
            DependencyProperty.Register(nameof(ShowMinimizeButton), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));

        public static readonly DependencyProperty MinimizeIconUseProperty =
            DependencyProperty.Register(nameof(UseMinimizeIcon), typeof(bool), typeof(TitleBar), new PropertyMetadata(false));

        public static readonly DependencyProperty MinimizeIconSourceProperty =
            DependencyProperty.Register(nameof(MinimizeIconSource), typeof(ImageSource), typeof(TitleBar));

        #endregion

        #region Maximize Button
        public static readonly DependencyProperty MaximizeButtonStyleProperty =
            DependencyProperty.Register(nameof(MaximizeButtonStyle), typeof(Style), typeof(TitleBar));

        public static readonly DependencyProperty MaximizeCommandProperty =
            DependencyProperty.Register(nameof(MaximizeCommand), typeof(ICommand), typeof(TitleBar));

        public static readonly DependencyProperty ShowMaximizeButtonProperty =
            DependencyProperty.Register(nameof(ShowMaximizeButton), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));

        public static readonly DependencyProperty MaximizeIconUseProperty =
           DependencyProperty.Register(nameof(UseMaximizeIcon), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));

        public static readonly DependencyProperty MaximizeIconSourceProperty =
            DependencyProperty.Register(nameof(MaximizeIconSource), typeof(ImageSource), typeof(TitleBar));

        public static readonly DependencyProperty ButtonBackgroundProperty =
    DependencyProperty.Register(nameof(ButtonBackground), typeof(Brush), typeof(TitleBar), new PropertyMetadata(Brushes.Transparent));


        #endregion

        #region Close Button

        public static readonly DependencyProperty CloseButtonStyleProperty =
            DependencyProperty.Register(nameof(CloseButtonStyle), typeof(Style), typeof(TitleBar));

        public static readonly DependencyProperty CloseButtonCommandProperty =
            DependencyProperty.Register(nameof(CloseButtonCommand), typeof(ICommand), typeof(TitleBar));

        public static readonly DependencyProperty CloseIconUseProperty =
           DependencyProperty.Register(nameof(UseCloseIcon), typeof(bool), typeof(TitleBar), new PropertyMetadata(true));

        public static readonly DependencyProperty CloseIconSourceProperty =
          DependencyProperty.Register(nameof(CloseIconSource), typeof(ImageSource), typeof(TitleBar));

        public static readonly DependencyProperty CloseBorderBrushProperty =
            DependencyProperty.Register(nameof(CloseBorderBrush), typeof(Brush), typeof(TitleBar), new PropertyMetadata(Brushes.Transparent));
        #endregion
        #endregion


        #region properties
        #region Border
        public Brush BorderBackground
        {
            get => (Brush)GetValue(BorderBackgroundProperty);
            set => SetValue(BorderBackgroundProperty, value);
        }

        public Style BorderStyle
        {
            get => (Style)GetValue(BorderStyleProperty);
            set => SetValue(BorderStyleProperty, value);
        }

        public CornerRadius BorderCornerRadius
        {
            get => (CornerRadius)GetValue(BorderCornerRadiusProperty);
            set => SetValue(BorderCornerRadiusProperty, value);
        }
        #endregion

        #region Icon

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public Style IconStyle
        {
            get => (Style)GetValue(IconStyleProperty);
            set => SetValue(IconStyleProperty, value);
        }
        #endregion

        #region Title

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Style TitleStyle
        {
            get => (Style)GetValue(TitleStyleProperty);
            set => SetValue(TitleStyleProperty, value);
        }

        public Brush TitleForeground
        {
            get => (Brush)GetValue(TitleForegroundProperty);
            set => SetValue(TitleForegroundProperty, value);
        }

        public HorizontalAlignment TitleHorizontalAligment
        {
            get => (HorizontalAlignment)GetValue(TitleHorizontalAligmentProperty);
            set => SetValue(TitleHorizontalAligmentProperty, value);
        }

        public double TitleFontsize
        {
            get => (double)GetValue(TitleFontsizeProperty);
            set => SetValue(TitleFontsizeProperty, value);
        }
        #endregion

        #region Buttons

        public Brush ButtonBackground
        {
            get => (Brush)GetValue(ButtonBackgroundProperty);
            set => SetValue(ButtonBackgroundProperty, value);
        }

        #region Minimize Button

        public Style ButtonStyle
        {
            get => (Style)GetValue(ButtonStyleProperty);
            set => SetValue(ButtonStyleProperty, value);
        }

        public ICommand MinimizeCommand
        {
            get => (ICommand)GetValue(MinimizeCommandProperty);
            set => SetValue(MinimizeCommandProperty, value);
        }

        public bool ShowMinimizeButton
        {
            get => (bool)GetValue(ShowMinimizeButtonProperty);
            set => SetValue(ShowMinimizeButtonProperty, value);
        }

        public bool UseMinimizeIcon
        {
            get => (bool)GetValue(MinimizeIconUseProperty);
            set => SetValue(MinimizeIconUseProperty, value);
        }

        public ImageSource MinimizeIconSource
        {
            get => (ImageSource)GetValue(MinimizeIconSourceProperty);
            set => SetValue(MinimizeIconSourceProperty, value);
        }


        #endregion

        #region Maximize Button

        public Style MaximizeButtonStyle
        {
            get => (Style)GetValue(MaximizeButtonStyleProperty);
            set => SetValue(MaximizeButtonStyleProperty, value);
        }

        public ICommand MaximizeCommand
        {
            get => (ICommand)GetValue(MaximizeCommandProperty);
            set => SetValue(MaximizeCommandProperty, value);
        }

        public bool ShowMaximizeButton
        {
            get => (bool)GetValue(ShowMaximizeButtonProperty);
            set => SetValue(ShowMaximizeButtonProperty, value);
        }

        public bool UseMaximizeIcon
        {
            get => (bool)GetValue(MaximizeIconUseProperty);
            set => SetValue(MaximizeIconUseProperty, value);
        }

        public ImageSource MaximizeIconSource
        {
            get => (ImageSource)GetValue(MaximizeIconSourceProperty);
            set => SetValue(MaximizeIconSourceProperty, value);
        }
        #endregion

        #region Close Button

        public Style CloseButtonStyle
        {
            get => (Style)GetValue(CloseButtonStyleProperty);
            set => SetValue(CloseButtonStyleProperty, value);
        }

        public ICommand CloseButtonCommand
        {
            get => (ICommand)GetValue(CloseButtonCommandProperty);
            set => SetValue(CloseButtonCommandProperty, value);
        }

        public bool UseCloseIcon
        {
            get => (bool)GetValue(CloseIconUseProperty);
            set => SetValue(CloseIconUseProperty, value);
        }

        public ImageSource CloseIconSource
        {
            get => (ImageSource)GetValue(CloseIconSourceProperty);
            set => SetValue(CloseIconSourceProperty, value);
        }

        public Brush CloseBorderBrush
        {
            get => (Brush)GetValue(CloseBorderBrushProperty);
            set => SetValue(CloseBorderBrushProperty, value);
        }
        #endregion
        #endregion
        #endregion

    }
}
