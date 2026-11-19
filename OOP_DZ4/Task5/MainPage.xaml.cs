using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Task5.Models;

namespace Task5
{
    public partial class MainPage : ContentPage
    {
        private readonly ObservableCollection<Product> _products;
        private int _itemCounter;

        public MainPage()
        {
            InitializeComponent();

            _products = new ObservableCollection<Product>
            {
                new FoodProduct(
                    42.50m,
                    "Україна",
                    "Молоко фермерське 2.5%",
                    new DateTime(2026, 11, 10),
                    "Пастеризоване коров'яче молоко",
                    new DateTime(2026, 11, 20),
                    1.0,
                    "л"),
                new FoodProduct(
                    135.00m,
                    "Італія",
                    "Оливкова олія Extra Virgin",
                    new DateTime(2026, 10, 1),
                    "Перший холодний віджим",
                    new DateTime(2027, 10, 1),
                    0.5,
                    "л"),
                new Book(
                    580.00m,
                    "Україна",
                    "CLR via C#",
                    new DateTime(2026, 9, 15),
                    "Поглиблене вивчення платформи .NET",
                    896,
                    "Фабула",
                    new List<string> { "Джеффрі Ріхтер" }),
                new Book(
                    450.00m,
                    "Велика Британія",
                    "Чистий код",
                    new DateTime(2026, 8, 20),
                    "Створення та рефакторинг програмного забезпечення",
                    464,
                    "Prentice Hall",
                    new List<string> { "Роберт Мартін" })
            };

            _itemCounter = 1;
            ProductsCollectionView.ItemsSource = _products;
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet(
                "Оберіть тип товару для додавання:",
                "Скасувати",
                null,
                "Продукти",
                "Книги");

            if (action == "Продукти")
            {
                _products.Add(new FoodProduct(
                    68.00m + _itemCounter * 5,
                    "Україна",
                    $"Сир кисломолочний #{_itemCounter}",
                    DateTime.Today,
                    "Натуральний фермерський продукт",
                    DateTime.Today.AddDays(14),
                    0.4,
                    "кг"));
                _itemCounter++;
            }
            else if (action == "Книги")
            {
                _products.Add(new Book(
                    490.00m + _itemCounter * 20,
                    "Україна",
                    $"Алгоритми та структури даних #{_itemCounter}",
                    DateTime.Today,
                    "Навчальний посібник з програмування",
                    520,
                    "Віват",
                    new List<string> { "Томас Кормен", "Чарльз Лейзерсон" }));
                _itemCounter++;
            }
        }

        private async void OnRemoveProductClicked(object sender, EventArgs e)
        {
            if (_products.Count == 0)
            {
                await DisplayAlert("Інформація", "Список товарів уже порожній.", "OK");
                return;
            }

            if (ProductsCollectionView.SelectedItem is Product selectedProduct)
            {
                _products.Remove(selectedProduct);
                ProductsCollectionView.SelectedItem = null;
            }
            else
            {
                _products.RemoveAt(_products.Count - 1);
            }
        }
    }
}