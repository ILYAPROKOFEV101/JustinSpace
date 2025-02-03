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
        private Point orbitCenter = new Point(400, 200); // Центр орбиты
        private double orbitRadius = 150; // Радиус орбиты

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

                // Создаем анимацию взлета по параболе
                DoubleAnimation liftXAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = orbitCenter.X - orbitRadius, // Движение вправо до начала орбиты
                    Duration = TimeSpan.FromSeconds(2),
                    AccelerationRatio = 0.3,
                    DecelerationRatio = 0.3
                };

                DoubleAnimation liftYAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = orbitCenter.Y - orbitRadius, // Движение вверх до начала орбиты
                    Duration = TimeSpan.FromSeconds(2),
                    AccelerationRatio = 0.3,
                    DecelerationRatio = 0.3
                };

                Console.WriteLine("StartButton_Click: Создана анимация взлёта.");

                // Создаем анимацию вращения по орбите
                DoubleAnimation orbitAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(5),
                    RepeatBehavior = RepeatBehavior.Forever, // Бесконечное повторение
                    AutoReverse = false // Без обратного движения
                };

                Console.WriteLine("StartButton_Click: Создана анимация орбитального движения.");

                // Создаем Storyboard
                storyboard = new Storyboard();

                // Добавляем анимацию взлета
                storyboard.Children.Add(liftXAnimation);
                storyboard.Children.Add(liftYAnimation);

                // Привязываем анимацию взлета к свойствам TranslateTransform
                Storyboard.SetTarget(liftXAnimation, Rocket);
                Storyboard.SetTargetProperty(liftXAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)"));

                Storyboard.SetTarget(liftYAnimation, Rocket);
                Storyboard.SetTargetProperty(liftYAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));

                // Устанавливаем центр поворота для орбитального движения
                rocketRotate.CenterX = orbitCenter.X;
                rocketRotate.CenterY = orbitCenter.Y;

                // Добавляем анимацию вращения по орбите
                storyboard.Children.Add(orbitAnimation);

                // Привязываем анимацию вращения к свойству Angle RotateTransform
                Storyboard.SetTarget(orbitAnimation, Rocket);
                Storyboard.SetTargetProperty(orbitAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));

                // Устанавливаем начало орбитальной анимации после завершения анимации взлета
                orbitAnimation.BeginTime = TimeSpan.FromSeconds(2);

                Console.WriteLine("StartButton_Click: Все анимации добавлены в Storyboard.");

                // Запускаем анимацию
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