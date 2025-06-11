using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Tools.Properties.MainTools;

namespace JustinSpace
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer animationTimer;
        private TranslateTransform rocketTranslate;
        private Calculator currentCalculator;
        private int animationStepIndex = 0;
        private double zoomFactor = 1.0;
        private readonly double minZoom = 0.5;
        private readonly double maxZoom = 3.0;

        public MainWindow()
        {
            InitializeComponent();
            
            // Настройка полноэкранного режима
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            
            var transformGroup = Rocket.RenderTransform as TransformGroup;
            rocketTranslate = transformGroup.Children[0] as TranslateTransform;
            
            // Центрирование сцены при загрузке
            Loaded += (s, e) => CenterRocketInView();
        }

        private void CenterRocketInView()
        {
            // Рассчитываем центр сцены
            double centerX = SceneCanvas.Width / 2;
            double centerY = SceneCanvas.Height / 2;
    
            // Автоматический подбор масштаба
            zoomFactor = Math.Min(
                GameScrollViewer.ViewportWidth / SceneCanvas.Width,
                GameScrollViewer.ViewportHeight / SceneCanvas.Height
            ) * 0.9; // 90% от максимального возможного масштаба
    
            // Замена Math.Clamp для старых версий .NET
            zoomFactor = (zoomFactor < minZoom) ? minZoom : (zoomFactor > maxZoom) ? maxZoom : zoomFactor;
    
            ApplyZoom();
    
            // Прокрутка к ракете
            GameScrollViewer.ScrollToHorizontalOffset(centerX - GameScrollViewer.ViewportWidth / 2);
            GameScrollViewer.ScrollToVerticalOffset(centerY - GameScrollViewer.ViewportHeight / 2);
        }
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.SingleBorderWindow;
            }
            base.OnKeyDown(e);
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка ввода
            if (!ValidateInput())
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения для всех параметров", "Ошибка ввода");
                return;
            }

            // Остановить предыдущую анимацию
            animationTimer?.Stop();
            animationStepIndex = 0;

            // Создание калькулятора
            currentCalculator = CreateCalculator();

            // Настройка таймера для анимации
            animationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
            animationTimer.Tick += AnimateRocket;
            animationTimer.Start();
        }

        private bool ValidateInput()
        {
            return double.TryParse(Stage1DryMass.Text, out _) &&
                   double.TryParse(Stage1FuelMass.Text, out _) &&
                   double.TryParse(Stage1FuelConsumption.Text, out _) &&
                   double.TryParse(Stage2DryMass.Text, out _) &&
                   double.TryParse(Stage2FuelMass.Text, out _) &&
                   double.TryParse(Stage2FuelConsumption.Text, out _) &&
                   double.TryParse(Stage3DryMass.Text, out _) &&
                   double.TryParse(Stage3FuelMass.Text, out _) &&
                   double.TryParse(Stage3FuelConsumption.Text, out _);
        }

        private Calculator CreateCalculator()
        {
            return new Calculator(
                double.Parse(Stage1DryMass.Text),
                double.Parse(Stage1FuelMass.Text),
                double.Parse(Stage1FuelConsumption.Text),
                double.Parse(Stage2DryMass.Text),
                double.Parse(Stage2FuelMass.Text),
                double.Parse(Stage2FuelConsumption.Text),
                double.Parse(Stage3DryMass.Text),
                double.Parse(Stage3FuelMass.Text),
                double.Parse(Stage3FuelConsumption.Text));
        }

        private void AnimateRocket(object sender, EventArgs e)
        {
            if (currentCalculator == null || animationStepIndex >= currentCalculator.YAxisValues.Count)
            {
                animationTimer?.Stop();
                return;
            }

            double scale = 0.01;
            double x = currentCalculator.XAxisValues[animationStepIndex] * scale;
            double y = -currentCalculator.YAxisValues[animationStepIndex] * scale;

            rocketTranslate.X = x;
            rocketTranslate.Y = y;

            

            animationStepIndex++;
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (zoomFactor < maxZoom)
            {
                zoomFactor = Math.Min(zoomFactor + 0.1, maxZoom);
                ApplyZoom();
            }
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (zoomFactor > minZoom)
            {
                zoomFactor = Math.Max(zoomFactor - 0.1, minZoom);
                ApplyZoom();
            }
        }

        private void ApplyZoom()
        {
            SceneCanvasScaleTransform.ScaleX = zoomFactor;
            SceneCanvasScaleTransform.ScaleY = zoomFactor;
            var animation = new DoubleAnimation(zoomFactor, TimeSpan.FromMilliseconds(300))
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            SceneCanvasScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            SceneCanvasScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }
}