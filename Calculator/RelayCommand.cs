using System.Windows.Input;

namespace Calculator;

public class RelayCommand(
    Action<object?> execute,
    Func<object?, bool>? canExecute = null)
    : ICommand
{
    public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

    public void Execute(object? parameter) => execute(parameter);

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}