using DormitoryXmlLab2.Models;
using DormitoryXmlLab2.Services;
using DormitoryXmlLab2.Strategies;

namespace DormitoryXmlLab2;

public partial class MainPage : ContentPage
{
    private readonly XmlSearchContext _searchContext;
    private readonly XmlMetadataService _metadataService;
    private readonly HtmlTransformerService _htmlTransformerService;

    private string _currentXmlPath = string.Empty;
    private string _defaultXslPath = string.Empty;

    public MainPage()
    {
        InitializeComponent();

        _searchContext = new XmlSearchContext(new SaxXmlSearchStrategy());
        _metadataService = new XmlMetadataService();
        _htmlTransformerService = new HtmlTransformerService();

        _ = InitializeDefaultFilesAsync();
    }

    private async Task InitializeDefaultFilesAsync()
    {
        try
        {
            string sampleXml = await ExtractPackageAssetAsync("dormitory.xml");
            _defaultXslPath = await ExtractPackageAssetAsync("dormitory.xsl");

            if (File.Exists(sampleXml))
            {
                LoadXmlFile(sampleXml);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка ініціалізації файлів", ex.Message, "ОК");
        }
    }

    private static async Task<string> ExtractPackageAssetAsync(string assetName)
    {
        string targetPath = Path.Combine(FileSystem.AppDataDirectory, assetName);
        using Stream sourceStream = await FileSystem.OpenAppPackageFileAsync(assetName);
        using FileStream targetStream = File.Create(targetPath);
        await sourceStream.CopyToAsync(targetStream);
        return targetPath;
    }

    private void LoadXmlFile(string filePath)
    {
        try
        {
            _currentXmlPath = filePath;
            CurrentFileLabel.Text = $"Активний файл: {Path.GetFileName(filePath)}";

            var (faculties, departments, courses) = _metadataService.LoadFilterOptions(filePath);

            FacultyPicker.ItemsSource = faculties;
            FacultyPicker.SelectedIndex = 0;

            DepartmentPicker.ItemsSource = departments;
            DepartmentPicker.SelectedIndex = 0;

            CoursePicker.ItemsSource = courses;
            CoursePicker.SelectedIndex = 0;

            StatusLabel.Text = "Файл успішно завантажено. Оберіть параметри та натисніть «Пошук».";
        }
        catch (Exception ex)
        {
            DisplayAlert("Помилка читання XML", ex.Message, "ОК");
        }
    }

    private void OnStrategyChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (!e.Value || _searchContext is null)
        {
            return;
        }

        if (sender == SaxRadio)
        {
            _searchContext.SetStrategy(new SaxXmlSearchStrategy());
        }
        else if (sender == DomRadio)
        {
            _searchContext.SetStrategy(new DomXmlSearchStrategy());
        }
        else if (sender == LinqRadio)
        {
            _searchContext.SetStrategy(new LinqXmlSearchStrategy());
        }
    }

    private async void OnOpenXmlClicked(object? sender, EventArgs e)
    {
        try
        {
            var xmlFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.MacCatalyst, new[] { "public.xml" } },
                { DevicePlatform.WinUI, new[] { ".xml" } }
            });

            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Оберіть XML-файл гуртожитку",
                FileTypes = xmlFileType
            });

            if (result is not null)
            {
                LoadXmlFile(result.FullPath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка вибору файлу", ex.Message, "ОК");
        }
    }

    private void OnSearchClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_currentXmlPath) || !File.Exists(_currentXmlPath))
        {
            DisplayAlert("Увага", "Спочатку відкрийте коректний XML-файл.", "ОК");
            return;
        }

        try
        {
            var criteria = new SearchCriteria
            {
                Faculty = FacultyPicker.SelectedItem?.ToString() ?? "Усі",
                Department = DepartmentPicker.SelectedItem?.ToString() ?? "Усі",
                Course = CoursePicker.SelectedItem?.ToString() ?? "Усі",
                Keyword = KeywordEntry.Text ?? string.Empty
            };

            List<ResidentRecord> results = _searchContext.ExecuteSearch(_currentXmlPath, criteria);
            ResultsCollectionView.ItemsSource = results;
            StatusLabel.Text = $"Знайдено записів: {results.Count} (метод: {_searchContext.CurrentStrategyName})";
        }
        catch (Exception ex)
        {
            DisplayAlert("Помилка пошуку", ex.Message, "ОК");
        }
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        KeywordEntry.Text = string.Empty;
        if (FacultyPicker.ItemsSource is not null) FacultyPicker.SelectedIndex = 0;
        if (DepartmentPicker.ItemsSource is not null) DepartmentPicker.SelectedIndex = 0;
        if (CoursePicker.ItemsSource is not null) CoursePicker.SelectedIndex = 0;

        ResultsCollectionView.ItemsSource = null;
        StatusLabel.Text = "Поле виводу та параметри пошуку очищено.";
    }

    private async void OnTransformHtmlClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_currentXmlPath) || !File.Exists(_currentXmlPath))
        {
            await DisplayAlert("Увага", "Спочатку відкрийте XML-файл.", "ОК");
            return;
        }

        try
        {
            if (string.IsNullOrWhiteSpace(_defaultXslPath) || !File.Exists(_defaultXslPath))
            {
                _defaultXslPath = await ExtractPackageAssetAsync("dormitory.xsl");
            }

            string outputHtml = Path.Combine(FileSystem.AppDataDirectory, "dormitory_report.html");
            _htmlTransformerService.TransformToHtml(_currentXmlPath, _defaultXslPath, outputHtml);

            string htmlContent = await File.ReadAllTextAsync(outputHtml);

            var closeButton = new Button
            {
                Text = "Закрити перегляд",
                BackgroundColor = Color.FromArgb("#b91c1c"),
                TextColor = Colors.White
            };
            closeButton.Clicked += async (_, _) => await Navigation.PopModalAsync();

            var shareButton = new Button
            {
                Text = "Зберегти / Поділитися HTML",
                BackgroundColor = Color.FromArgb("#1e40af"),
                TextColor = Colors.White
            };
            shareButton.Clicked += async (_, _) =>
                await Launcher.Default.OpenAsync(new OpenFileRequest("HTML-звіт", new ReadOnlyFile(outputHtml)));

            var topBar = new HorizontalStackLayout
            {
                Spacing = 12,
                Padding = new Thickness(16, 12),
                Children =
                {
                    closeButton,
                    shareButton,
                    new Label
                    {
                        Text = "Згенеровано через XslCompiledTransform",
                        VerticalOptions = LayoutOptions.Center,
                        FontAttributes = FontAttributes.Bold
                    }
                }
            };

            var webView = new WebView
            {
                Source = new HtmlWebViewSource { Html = htmlContent },
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };

            var previewGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star }
                }
            };
            previewGrid.Add(topBar, 0, 0);
            previewGrid.Add(webView, 0, 1);

            await Navigation.PushModalAsync(new ContentPage
            {
                Title = "HTML-звіт (XSLT)",
                Content = previewGrid
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка XSLT-трансформації", ex.Message, "ОК");
        }
    }

    private async void OnHelpClicked(object? sender, EventArgs e)
    {
        await DisplayAlert(
            "Допомога користувачу",
            "1. Натисніть «Відкрити XML» для завантаження файлу (або використовуйте автоматично завантажений приклад).\n" +
            "2. Оберіть стратегію аналізу: SAX, DOM або LINQ to XML.\n" +
            "3. Вкажіть фільтри (факультет, кафедру, курс чи ключове слово) та натисніть «Пошук».\n" +
            "4. Кнопка «Clear» очищає результати й параметри.\n" +
            "5. Кнопка «В HTML» генерує веб-звіт за допомогою XSL та відкриває його у браузері.",
            "Зрозуміло");
    }

    private async void OnAboutClicked(object? sender, EventArgs e)
    {
        await DisplayAlert(
            "Про програму",
            "Лабораторна робота №2 (Варіант 3: «Гуртожиток»)\n" +
            "Дисципліна: Об'єктно-орієнтоване програмування\n" +
            "Реалізовано шаблон «Стратегія» (SAX, DOM, LINQ to XML) та XSLT-трансформацію.",
            "ОК");
    }

    private async void OnExitClicked(object? sender, EventArgs e)
    {
        bool confirm = await DisplayAlert(
            "Підтвердження виходу",
            "Чи дійсно ви хочете завершити роботу з програмою?",
            "Так",
            "Ні");

        if (confirm)
        {
            Application.Current?.Quit();
        }
    }
}
