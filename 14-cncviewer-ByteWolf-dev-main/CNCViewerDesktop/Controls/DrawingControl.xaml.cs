using Core.Entities;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CNCViewerDesktop.Controls
{
    /// <summary>
    /// Interaction logic for DrawingControl.xaml
    /// </summary>
    public partial class DrawingControl : UserControl
    {

        public DrawingControl()
        {
            InitializeComponent();
        }
        public static DependencyProperty PatternProperty = DependencyProperty.Register(
            nameof(Pattern),
            typeof(Pattern),
            typeof(DrawingControl),
            new PropertyMetadata(Pattern.Demo,
                OnScaleXChanged));

        public Pattern Pattern
        {
            get => (Pattern)GetValue(PatternProperty);
            set => SetValue(PatternProperty, value);
        }

        private static void OnScaleXChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (DrawingControl)dependencyObject;
            ctrl.InvalidateVisual();
        }

        private static readonly Pen RedPen = new Pen(new SolidColorBrush(Colors.DarkRed), 2.0d);

        protected override void OnRender(DrawingContext drawingContext)
        {
            // TODO: Render the pattern
        }

    }
}
