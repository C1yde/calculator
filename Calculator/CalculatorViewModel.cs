using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Calculator;

public class CalculatorViewModel : INotifyPropertyChanged
{
    private string _display = "0";
    private string? _currentOperation;
    private double _firstOperand;
    private bool _isNewInput = true;

    public string Display
    {
        get => _display;
        set { _display = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand ButtonCommand { get; }

    public ICommand OpenSettingsCommand { get; }

    public CalculatorViewModel()
    {
        ButtonCommand = new RelayCommand(ExecuteButton);
        OpenSettingsCommand = new RelayCommand(ToggleTheme);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ExecuteButton(object? parameter)
    {
        var buttonTag = parameter as string;

        if (double.TryParse(buttonTag, out var number))
        {
            HandleNumberInput(buttonTag);
        }
        else
        {
            HandleOperation(buttonTag);
        }
    }

    private void HandleNumberInput(string input)
    {
        if (_isNewInput)
        {
            Display = input;
            _isNewInput = false;
        }
        else
        {
            Display += input;
        }
    }

    private void HandleOperation(string? operation)
    {
        switch (operation)
        {
            case "C":
                Display = "0";
                _isNewInput = true;
                _currentOperation = null;
                _firstOperand = 0;
                break;
            case "=":
                ExecuteCalculation();
                break;
            default:
                _firstOperand = double.Parse(Display);
                _currentOperation = operation;
                _isNewInput = true;
                break;
        }
    }

    private void ExecuteCalculation()
    {
        if (!double.TryParse(Display, out var secondOperand) || _currentOperation == null)
            return;

        double result = 0;
        switch (_currentOperation)
        {
            case "+":
                result = _firstOperand + secondOperand;
                break;
            case "-":
                result = _firstOperand - secondOperand;
                break;
            case "*":
                result = _firstOperand * secondOperand;
                break;
            case "/":
                {
                    if (secondOperand != 0)
                    {
                        Display = "Error";
                    }
                    else
                    {
                        result = _firstOperand / secondOperand;
                    }
                }
                break;
        }

        Display = result.ToString(CultureInfo.InvariantCulture);
    }

    private static void ToggleTheme(object? parameter)
    {
        var currentTheme = Application.Current.Resources.MergedDictionaries[0];
        if (currentTheme.Source.ToString().Contains("LightTheme.xaml"))
        {
            Application.Current.Resources.MergedDictionaries[0] =
                new ResourceDictionary { Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative) };
        }
        else
        {
            Application.Current.Resources.MergedDictionaries[0] =
                new ResourceDictionary { Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative) };
        }
    }
}