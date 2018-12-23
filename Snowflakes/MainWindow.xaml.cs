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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snowflakes
{
    public partial class MainWindow : Window
    {
        static int count = 200;
        Image[] snow = new Image[count];
        Random random = new Random();
        Grid grid = new Grid();

        int screenH;
        int screenW;

        int limitH_Top;
        int limitH_Bottom;
        int limitW_Left;
        int limitW_Right;

        public MainWindow()
        {
            InitializeComponent();

            screenH = (int)SystemParameters.PrimaryScreenHeight;
            screenW = (int)SystemParameters.PrimaryScreenWidth;
            limitH_Top = 0 - screenH - 20;
            limitH_Bottom = screenH + 20;
            limitW_Left = 0 - screenW + 20;
            limitW_Right = screenW - 20;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Content = grid;

            for (int i = 0; i < count; i++)
            {
                snow[i] = new Image();

                snow[i].Source = new BitmapImage(new Uri("snowflake.png", UriKind.Relative));
                snow[i].Width = 20;

                grid.Children.Add(snow[i]);
            }
            await Move();
        }

        public async Task Move()
        {
            for (int i = 0; i < count; i++)
            {
                int left = random.Next(limitW_Left, limitW_Right);
                int top = random.Next(limitH_Top + limitH_Top + limitH_Top, limitH_Bottom + limitH_Top + limitH_Top);
                snow[i].Margin = new Thickness(left, top, 0, 0);
            }

            while (true)
            {
                for (int i = 0; i < count; i++)
                {
                    var left = snow[i].Margin.Left;
                    var top = snow[i].Margin.Top;
                    top += 2;
                    if (top > limitH_Bottom)
                    {
                        top = limitH_Top;
                        left = random.Next(limitW_Left, limitW_Right);
                    }
                    snow[i].Margin = new Thickness(left, top, 0, 0);
                }
                await Task.Delay(16);
            }
        }
    }
}
