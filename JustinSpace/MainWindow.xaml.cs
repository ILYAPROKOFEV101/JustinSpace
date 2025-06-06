using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace JustinSpace
{
    public partial class MainWindow : Window
    {
        private Storyboard storyboard;
        private TranslateTransform rocketTranslate;

        public MainWindow()
        {
            InitializeComponent();
            var transformGroup = Rocket.RenderTransform as TransformGroup;
            rocketTranslate = transformGroup.Children[0] as TranslateTransform;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(FuelInput.Text, out int fuel) || fuel <= 0)
            {
                MessageBox.Show("Введите корректное количество топлива > 0", "Ошибка");
                return;
            }

            // Остановить предыдущую анимацию
            storyboard?.Stop();

            // Рассчет высоты подъема
            double liftHeight = Math.Min(fuel * 2, 200);
            
            // Создание анимации подъема
            DoubleAnimation liftAnimation = new DoubleAnimation
            {
                From = 0,
                To = -liftHeight,
                Duration = TimeSpan.FromSeconds(2),
                AccelerationRatio = 0.3,
                DecelerationRatio = 0.3
            };

            // Создание анимации падения (с ускорением)
            DoubleAnimation fallAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            fallAnimation.BeginTime = TimeSpan.FromSeconds(2); // Начать после подъема

            // Настройка Storyboard
            storyboard = new Storyboard();
            storyboard.Children.Add(liftAnimation);
            storyboard.Children.Add(fallAnimation);

            // Привязка анимаций к трансформации ракеты
            Storyboard.SetTarget(liftAnimation, Rocket);
            Storyboard.SetTarget(fallAnimation, Rocket);
            Storyboard.SetTargetProperty(liftAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));
            Storyboard.SetTargetProperty(fallAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));

            storyboard.Begin();
        }
    }
}