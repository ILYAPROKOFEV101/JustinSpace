using System;
using System.Windows;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using System;
using System.Diagnostics; // Для Debug.WriteLine
using System.Windows;
using System.Windows.Media.Animation;

using System;
using System.Diagnostics; // Для Debug.WriteLine
using System.Windows;
using System.Windows.Media.Animation;

using System;
using System.Diagnostics; // Для Debug.WriteLine
using System.Windows;
using System.Windows.Media.Animation;
namespace JustinSpace
{
    public partial class MainWindow : Window
    {
        private Storyboard storyboard;
        private TranslateTransform rocketTranslate;
        private RotateTransform rocketRotate;

        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("MainWindow: Инициализация окна...");

            // Получаем трансформации из TransformGroup
            var transformGroup = Rocket.RenderTransform as TransformGroup;
            if (transformGroup == null)
            {
                throw new InvalidOperationException("RenderTransform должен быть типа TransformGroup.");
            }

            rocketTranslate = transformGroup.Children[0] as TranslateTransform;
            rocketRotate = transformGroup.Children[1] as RotateTransform;

            if (rocketTranslate == null || rocketRotate == null)
            {
                throw new InvalidOperationException("Одна из трансформаций не была найдена.");
            }

            Console.WriteLine("MainWindow: Трансформации успешно загружены.");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Console.WriteLine("StartButton_Click: Начало обработки клика.");

                if (storyboard != null)
                {
                    Console.WriteLine("StartButton_Click: Остановка предыдущей анимации.");
                    storyboard.Stop();
                }

                // Анимация подъёма ракеты
                DoubleAnimation liftAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = -100,
                    Duration = TimeSpan.FromSeconds(2),
                    AccelerationRatio = 0.3,
                    DecelerationRatio = 0.3,
                    FillBehavior = FillBehavior.HoldEnd
                };
                Console.WriteLine("StartButton_Click: Создана анимация подъема.");

                // Анимация вращения вокруг центра шарика
                DoubleAnimation orbitAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(3),
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromSeconds(2)
                };
                Console.WriteLine("StartButton_Click: Создана анимация вращения.");

                storyboard = new Storyboard();
                storyboard.Children.Add(liftAnimation);
                storyboard.Children.Add(orbitAnimation);
                Console.WriteLine("StartButton_Click: Добавлены анимации в Storyboard.");

                // Привязка анимации подъёма
                Storyboard.SetTarget(liftAnimation, Rocket);
                Storyboard.SetTargetProperty(liftAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));
                Console.WriteLine("StartButton_Click: Привязана анимация подъема к TranslateTransform.");

                // Привязка анимации орбиты
                Storyboard.SetTarget(orbitAnimation, Rocket);
                Storyboard.SetTargetProperty(orbitAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
                Console.WriteLine("StartButton_Click: Привязана анимация вращения к RotateTransform.");

                // Запуск анимации
                storyboard.Begin(this);
                Console.WriteLine("StartButton_Click: Анимация запущена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StartButton_Click: Ошибка: {ex.Message}");
            }
        }
    }
}