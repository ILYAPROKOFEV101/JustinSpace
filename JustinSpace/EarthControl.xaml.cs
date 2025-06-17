using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace JustinSpace
{
    public partial class EarthControl : UserControl
    {
        public EarthControl()
        {
            InitializeComponent();
            Loaded += EarthControl_Loaded;
        }

        private void EarthControl_Loaded(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)FindResource("EarthRotationStoryboard");
            storyboard.Begin();
        }
    }
}