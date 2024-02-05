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
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        TextBlock lastClicked;
        bool findingMatch;
        int tenthsOfSecondsElapsed = 0; 
        int matchesFound = 0;

        public MainWindow()
        {
            SetUpTimer();
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            timer.Start();
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
                "🦒", "🦒",
                "🦍", "🦍",
                "🐅", "🐅",
                "🐎", "🐎",
                "🦬", "🦬",
                "🐖", "🐖",
                "🐫", "🐫",
                "🦣", "🦣"
            };

            Random random = new Random();

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmoji.Count);
                    string emoji = animalEmoji[index];
                    textBlock.Text = emoji;
                    animalEmoji.RemoveAt(index);

                }                
            }

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock current = sender as TextBlock;
            if (!findingMatch)
            {
                current.Visibility = Visibility.Hidden;
                lastClicked = current;
                findingMatch = true;
            }
            else if (current.Text == lastClicked.Text)
            {
                current.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound++;
            }
            else
            {
                lastClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                matchesFound = 0;   
                SetUpGame();
            }

        }
    }
}