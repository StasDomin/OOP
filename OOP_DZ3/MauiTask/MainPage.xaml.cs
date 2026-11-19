namespace MauiTask;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnShowNumberButtonClicked(object? sender, EventArgs e)
    {
        try
        {
            string input = numberEntry.Text ?? string.Empty;

            if (input.Contains('.') || input.Contains(','))
            {
                throw new FormatException("Введено дробове число.");
            }

            int number = int.Parse(input);
            resultLabel.Text = $"Ви ввели число {number}";
        }
        catch (FormatException)
        {
            resultLabel.Text = "Помилка: потрібно ввести ціле число.";
        }
        catch (OverflowException)
        {
            resultLabel.Text = "Помилка: введене число занадто велике або занадто мале.";
        }
    }
}