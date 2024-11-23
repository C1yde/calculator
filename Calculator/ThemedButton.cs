using System.Windows.Controls;
using System.Windows.Media;

namespace Calculator;

public class ThemedButton : Button
{
    public ThemedButton()
    {
        Margin = new System.Windows.Thickness(5);

        SetResourceReference(BackgroundProperty, "ButtonBackgroundBrush");
        SetResourceReference(ForegroundProperty, "ButtonForegroundBrush");
        FontSize = 18;

        BorderThickness = new System.Windows.Thickness(1);
        BorderBrush = new SolidColorBrush(Colors.Gray);
    }
}