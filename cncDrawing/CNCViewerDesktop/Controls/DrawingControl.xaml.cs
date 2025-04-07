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
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, ActualWidth, ActualHeight));
            LinePoint? prevpoint = null;

            double scale = Math.Min((ActualWidth * 0.9) / Pattern.Width, (ActualHeight * 0.9) / Pattern.Height);
            Point offset = new Point((ActualWidth - Pattern.Width * scale) / 2, (ActualHeight - Pattern.Height * scale) / 2);

            foreach (var line in Pattern.Lines)
            {
                prevpoint = null;
                foreach(var poin in line.Points)
                {
                    if (prevpoint != null)
                    {
                        drawingContext.DrawLine(RedPen, 
                            new Point(prevpoint.X * scale + offset.X, ActualHeight - (prevpoint.Y * scale + offset.Y)), 
                            new Point(poin.X * scale + offset.X, ActualHeight - (poin.Y * scale + offset.Y)));
                    }
                    prevpoint = poin;
                }
            }
        }
    }
}
