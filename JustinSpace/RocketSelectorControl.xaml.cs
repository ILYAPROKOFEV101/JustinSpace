using System.Collections.Generic;
using System.Windows.Controls;

namespace JustinSpace.Controls
{
    public partial class RocketSelectorControl : UserControl
    {
        public delegate void RocketSelectedEventHandler(string rocketType);
        public event RocketSelectedEventHandler RocketSelected;
    
        public RocketSelectorControl()
        {
            InitializeComponent();

            var rockets = new List<RocketItem>()
            {
                new RocketItem() { Name = "Ракета 1", ImagePath = "/assets/photo1.png" },
                new RocketItem() { Name = "Ракета 2", ImagePath = "/assets/photo2.png" },
                new RocketItem() { Name = "Ракета 3", ImagePath = "/assets/photo3.png" }
            };

            
        }

        private void RocketComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RocketComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string rocketTag = selectedItem.Tag?.ToString();
                RocketSelected?.Invoke(rocketTag);
            }
        }
        public void SetRocket(string rocketName)
        {
            switch(rocketName)
            {
                case "rocket1":
                    // Логика смены изображения, цвета, видимых частей
                    break;
                case "rocket2":
                    // ...
                    break;
                case "rocket3":
                    // ...
                    break;
            }
        }


    }
    

    public class RocketItem
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}