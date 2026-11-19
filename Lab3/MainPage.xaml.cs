using System.Collections.ObjectModel;
using System.ComponentModel;
using DormitoryJsonManager.Models;
using DormitoryJsonManager.Services;
using DormitoryJsonManager.Views;

namespace DormitoryJsonManager;

public class ResidentRowItem : INotifyPropertyChanged
{
    private bool _isSelected;
    public Resident Item { get; }

    public ResidentRowItem(Resident item) => Item = item;

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RowBackground)));
            }
        }
    }

    public Color RowBackground => IsSelected ? Color.FromArgb("#1E4620") : Color.FromArgb("#252526");
    public event PropertyChangedEventHandler? PropertyChanged;
}

public partial class MainPage : ContentPage
{
    private readonly IJsonDormitoryService _jsonService = new JsonDormitoryService();
    private readonly DormitorySearchService _searchService = new DormitorySearchService();
    private DormitoryData _dormitoryData = new();
    private string? _currentFilePath;
    private bool _isUpdatingFilters;
    private ResidentRowItem? _selectedRow;

    public ObservableCollection<ResidentRowItem> DisplayedRows { get; } = new();

    public MainPage()
    {
        InitializeComponent();
        ResidentsCollectionView.ItemsSource = DisplayedRows;
        ResetFilters();
    }

    private async void OnOpenJsonClicked(object sender, EventArgs e)
    {
        try
        {
            FileResult? result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Оберіть JSON-файл гуртожитку"
            });

            if (result is null) return;

            if (!result.FileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                await DisplayAlert("Невірний формат", "Оберіть файл із розширенням .json", "ОК");
                return;
            }

            _dormitoryData = await _jsonService.LoadFromFileAsync(result.FullPath);
            _currentFilePath = result.FullPath;

            ActiveFileLabel.Text = $"Активний файл: {Path.GetFileName(_currentFilePath)}";
            DormitoryNameLabel.Text = $"({_dormitoryData.DormitoryName})";

            PopulateFilterPickers();
            ApplyLinqSearch();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка читання JSON", ex.Message, "ОК");
        }
    }

    private void PopulateFilterPickers()
    {
        _isUpdatingFilters = true;
        FacultyPicker.ItemsSource = _searchService.GetDistinctFaculties(_dormitoryData.Residents);
        FacultyPicker.SelectedIndex = 0;
        DepartmentPicker.ItemsSource = _searchService.GetDistinctDepartments(_dormitoryData.Residents);
        DepartmentPicker.SelectedIndex = 0;
        CoursePicker.ItemsSource = _searchService.GetDistinctCourses(_dormitoryData.Residents);
        CoursePicker.SelectedIndex = 0;
        _isUpdatingFilters = false;
    }

    private void ResetFilters()
    {
        _isUpdatingFilters = true;
        FacultyPicker.ItemsSource = new List<string> { "Усі" };
        FacultyPicker.SelectedIndex = 0;
        DepartmentPicker.ItemsSource = new List<string> { "Усі" };
        DepartmentPicker.SelectedIndex = 0;
        CoursePicker.ItemsSource = new List<string> { "Усі" };
        CoursePicker.SelectedIndex = 0;
        KeywordEntry.Text = string.Empty;
        _isUpdatingFilters = false;
    }

    private void ApplyLinqSearch()
    {
        List<Resident> filtered = _searchService.Search(
            _dormitoryData.Residents,
            FacultyPicker.SelectedItem?.ToString(),
            DepartmentPicker.SelectedItem?.ToString(),
            CoursePicker.SelectedItem?.ToString(),
            KeywordEntry.Text);

        DisplayedRows.Clear();
        _selectedRow = null;
        foreach (Resident r in filtered)
            DisplayedRows.Add(new ResidentRowItem(r));

        StatusCountLabel.Text = $"Знайдено записів: {DisplayedRows.Count} із {_dormitoryData.Residents.Count}";
    }

    private void OnFilterChanged(object sender, EventArgs e)
    {
        if (!_isUpdatingFilters) ApplyLinqSearch();
    }

    private void OnKeywordChanged(object sender, TextChangedEventArgs e)
    {
        if (!_isUpdatingFilters) ApplyLinqSearch();
    }

    private void OnSearchClicked(object sender, EventArgs e) => ApplyLinqSearch();

    private void OnRowTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is ResidentRowItem tappedRow)
        {
            if (_selectedRow is not null) _selectedRow.IsSelected = false;
            _selectedRow = tappedRow;
            _selectedRow.IsSelected = true;
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        ResidentFormPage formPage = new();
        await Navigation.PushModalAsync(formPage);
        Resident? newResident = await formPage.GetResultAsync();
        if (newResident is not null)
        {
            _dormitoryData.Residents.Add(newResident);
            PopulateFilterPickers();
            ApplyLinqSearch();
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (_selectedRow is null)
        {
            await DisplayAlert("Увага", "Спочатку натисніть на рядок у таблиці для редагування.", "ОК");
            return;
        }

        Resident target = _selectedRow.Item;
        ResidentFormPage formPage = new(target);
        await Navigation.PushModalAsync(formPage);
        Resident? updated = await formPage.GetResultAsync();
        if (updated is not null)
        {
            int idx = _dormitoryData.Residents.IndexOf(target);
            if (idx >= 0) _dormitoryData.Residents[idx] = updated;
            PopulateFilterPickers();
            ApplyLinqSearch();
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_selectedRow is null)
        {
            await DisplayAlert("Увага", "Оберіть рядок у таблиці для видалення.", "ОК");
            return;
        }

        bool confirm = await DisplayAlert("Підтвердження", $"Видалити мешканця «{_selectedRow.Item.FullName}»?", "Так", "Ні");
        if (confirm)
        {
            _dormitoryData.Residents.Remove(_selectedRow.Item);
            PopulateFilterPickers();
            ApplyLinqSearch();
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_currentFilePath))
        {
            await SaveAsNewFileAsync();
            return;
        }

        try
        {
            await _jsonService.SaveToFileAsync(_currentFilePath, _dormitoryData);
            await DisplayAlert("Збережено", $"Зміни записано у файл:\n{_currentFilePath}", "ОК");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка збереження", ex.Message, "ОК");
        }
    }

    private async void OnSaveAsClicked(object sender, EventArgs e) => await SaveAsNewFileAsync();

    private async Task SaveAsNewFileAsync()
    {
        string? fileName = await DisplayPromptAsync(
            "Зберегти як новий JSON",
            "Введіть назву нового файлу:",
            accept: "Зберегти",
            cancel: "Скасувати",
            initialValue: "dormitory_updated.json");

        if (string.IsNullOrWhiteSpace(fileName)) return;
        if (!fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) fileName += ".json";

        string dir = !string.IsNullOrWhiteSpace(_currentFilePath)
            ? Path.GetDirectoryName(_currentFilePath)!
            : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string newPath = Path.Combine(dir, fileName);

        try
        {
            await _jsonService.SaveToFileAsync(newPath, _dormitoryData);
            _currentFilePath = newPath;
            ActiveFileLabel.Text = $"Активний файл: {Path.GetFileName(_currentFilePath)}";
            await DisplayAlert("Успіх", $"Файл збережено:\n{newPath}", "ОК");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка збереження", ex.Message, "ОК");
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        _isUpdatingFilters = true;
        if (FacultyPicker.ItemsSource is not null) FacultyPicker.SelectedIndex = 0;
        if (DepartmentPicker.ItemsSource is not null) DepartmentPicker.SelectedIndex = 0;
        if (CoursePicker.ItemsSource is not null) CoursePicker.SelectedIndex = 0;
        KeywordEntry.Text = string.Empty;
        _isUpdatingFilters = false;
        ApplyLinqSearch();
    }

    private async void OnAboutClicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new AboutPage());

    private async void OnExitClicked(object sender, EventArgs e)
    {
        if (await DisplayAlert("Вихід", "Завершити роботу з програмою?", "Так", "Ні"))
            Application.Current?.Quit();
    }
}
