using System.Windows.Controls; 
using System.Windows;         
using System.Windows.Media;    

namespace JustinSpace.Controls
{
    public partial class RocketControl : UserControl
    {
        public RocketControl()
        {
            InitializeComponent();
        }

        public void ShowFlame(bool visible)
        {
            RocketFlame.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}