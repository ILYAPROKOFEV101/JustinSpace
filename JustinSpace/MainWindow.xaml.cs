using System;
using System.Windows;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace JustinSpace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Анимация подъёма
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = -300, // Высота подъёма
                Duration = TimeSpan.FromSeconds(3),
                AccelerationRatio = 0.5 // Эффект ускорения  
            };

            Rocket.RenderTransform.BeginAnimation(
                TranslateTransform.YProperty, 
                animation
            );
        }
    }
}