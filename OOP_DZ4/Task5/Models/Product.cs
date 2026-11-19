using System;

namespace Task5.Models
{
    public class Product
    {
        private decimal _price;
        private string _countryOfOrigin;
        private string _name;
        private DateTime _packagingDate;
        private string _description;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string CountryOfOrigin
        {
            get { return _countryOfOrigin; }
            set { _countryOfOrigin = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime PackagingDate
        {
            get { return _packagingDate; }
            set { _packagingDate = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual string Category
        {
            get { return "Товар"; }
        }

        public virtual string Details
        {
            get { return _description; }
        }

        public string FormattedPrice
        {
            get { return $"{_price:F2} грн"; }
        }

        public string FormattedPackagingDate
        {
            get { return _packagingDate.ToString("dd.MM.yyyy"); }
        }

        public Product(decimal price, string countryOfOrigin, string name, DateTime packagingDate, string description)
        {
            _price = price;
            _countryOfOrigin = countryOfOrigin;
            _name = name;
            _packagingDate = packagingDate;
            _description = description;
        }
    }
}