using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Threading;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new();
        private int tenthsOfSecondsElapsed;
        private int matchesFound;
        private TextBlock lastPressTB = null;
        private bool hasPressPair = false;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🦁","🦁",
                "🐷","🐷",
                "🐭","🐭",
                "🐗","🐗",
                "🐸","🐸",
                "🐼","🐼",
                "🐔","🐔",
                "🐮","🐮"
            };

            Random random = new Random();
            foreach (var textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "timeTextBlock") continue;
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (!hasPressPair)
            {
                textBlock.Foreground = Brushes.Red;
                lastPressTB = textBlock;
                hasPressPair = true;
                return;
            }

            if (lastPressTB.Text == textBlock.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastPressTB.Visibility = Visibility.Hidden;
                hasPressPair = false;
            }
            else
            {
                lastPressTB.Foreground = Brushes.Black;
                hasPressPair = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}