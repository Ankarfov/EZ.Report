using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EZ.Report
{
    public partial class Settings : Window
    {
        public MainWindow General { get; set; }

        public bool ShotSound { get; set; } = false;

        public Settings()
        {
            InitializeComponent();

            RefreshSoundButton(ShotSound);

            foreach (UIElement element in SettingsGrid.Children)
            {
                if (element is Button)
                {
                    ((Button)element).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Button = (string)((Button)e.OriginalSource).Name;

            switch (Button)
            {
                case "StateSoundBTN":

                    ShotSound = !ShotSound;

                    General.ShotSound = ShotSound;

                    RefreshSoundButton(ShotSound);

                    break;
            }
        }

        public void RefreshSoundButton(bool State)
        {
            if (State)
            {
                StateSoundBTN.Content = "Вкл";

                StateSoundBTN.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x00, 0xAA, 0x21));
            }
            else
            {
                StateSoundBTN.Content = "Выкл";

                StateSoundBTN.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0x30, 0x30));
            }
        }

        private void UpperBorder(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            General.Win_Settings_Status = false;

            Close();
        }
    }
}
