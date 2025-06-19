using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace JustinSpace.Controls
{
    public partial class RocketControl : UserControl
    {
        public RocketControl()
        {
            InitializeComponent();
            SetRocket("rocket_photo"); // дефолтная ракета
        }

        public void SetRocket(string rocketName)
        {
            string imagePath = null;

            switch (rocketName)
            {
                case "rocket_photo":
                    imagePath = "pack://application:,,,/Assets/rocket_photo.png";
                    break;
                case "rocket1":
                    imagePath = "pack://application:,,,/Assets/photo1.png";
                    break;
                case "rocket2":
                    imagePath = "pack://application:,,,/Assets/photo2.png";
                    break;
                case "rocket3":
                    imagePath = "pack://application:,,,/Assets/photo3.png";
                    break;
            }

            if (imagePath == null)
            {
                RocketImage.Source = null;
                return;
            }

            try
            {
                RocketImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }
            catch
            {
                RocketImage.Source = null;
            }
        }
    }
}