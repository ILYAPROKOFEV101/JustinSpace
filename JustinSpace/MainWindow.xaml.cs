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
        private Storyboard storyboard;
        private TranslateTransform rocketTranslate;
        private Calculator currentCalculator;
        private int animationStepIndex = 0;
        private DispatcherTimer animationTimer;
        private double zoomFactor = 1.0;
        private readonly double minZoom = 0.5;
        private readonly double maxZoom = 3.0;
        

        public MainWindow()
        {
            InitializeComponent();
            var transformGroup = Rocket.RenderTransform as TransformGroup;
            rocketTranslate = transformGroup.Children[0] as TranslateTransform;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка ввода
            if (!double.TryParse(Stage1DryMass.Text, out double dryMass1) ||
                !double.TryParse(Stage1FuelMass.Text, out double fuelMass1) ||
                !double.TryParse(Stage1FuelConsumption.Text, out double fuelConsumption1) ||
                !double.TryParse(Stage2DryMass.Text, out double dryMass2) ||
                !double.TryParse(Stage2FuelMass.Text, out double fuelMass2) ||
                !double.TryParse(Stage2FuelConsumption.Text, out double fuelConsumption2) ||
                !double.TryParse(Stage3DryMass.Text, out double dryMass3) ||
                !double.TryParse(Stage3FuelMass.Text, out double fuelMass3) ||
                !double.TryParse(Stage3FuelConsumption.Text, out double fuelConsumption3))
            {
                MessageBox.Show("Введите корректные параметры всех ступеней", "Ошибка");
                return;
            }

            // Остановить предыдущую анимацию
            animationTimer?.Stop();
            animationStepIndex = 0;

            // Создание калькулятора
            currentCalculator = new Calculator(
                dryMass1, fuelMass1, fuelConsumption1,
                dryMass2, fuelMass2, fuelConsumption2,
                dryMass3, fuelMass3, fuelConsumption3);

            // Настройка таймера для анимации
            animationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
            animationTimer.Tick += AnimateRocket;
            animationTimer.Start();
        }
        
        private void AnimateRocket(object sender, EventArgs e)
        {
            if (animationStepIndex >= currentCalculator.YAxisValues.Count)
            {
                animationTimer.Stop();
                return;
            }

            // Масштабирование: 1 метр = 0.01 пикселя (можно настроить)
            double scale = 0.01;

            double x = currentCalculator.XAxisValues[animationStepIndex] * scale;
            double y = -currentCalculator.YAxisValues[animationStepIndex] * scale; // Отрицательное значение для движения вверх

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
        }
    }
}