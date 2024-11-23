using System.Windows;
using System.Windows.Media.Animation;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isMenuOpen;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isMenuOpen)
            {
                var animation = new DoubleAnimation
                {
                    From = 200,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(300))
                };
                MenuPanel.BeginAnimation(WidthProperty, animation);
            }
            else
            {
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 200,
                    Duration = new Duration(TimeSpan.FromMilliseconds(300))
                };
                MenuPanel.BeginAnimation(WidthProperty, animation);
            }

            _isMenuOpen = !_isMenuOpen;
        }
    }
}
