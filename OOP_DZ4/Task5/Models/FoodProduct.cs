using System;

namespace Task5.Models
{
    public class FoodProduct : Product
    {
        private DateTime _expirationDate;
        private double _quantity;
        private string _unit;

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }

        public double Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public override string Category
        {
            get { return "Продукти"; }
        }

        public override string Details
        {
            get
            {
                return $"{Description} | К-ть: {_quantity} {_unit} | Придатний до: {_expirationDate:dd.MM.yyyy}";
            }
        }

        public FoodProduct(
            decimal price,
            string countryOfOrigin,
            string name,
            DateTime packagingDate,
            string description,
            DateTime expirationDate,
            double quantity,
            string unit)
            : base(price, countryOfOrigin, name, packagingDate, description)
        {
            _expirationDate = expirationDate;
            _quantity = quantity;
            _unit = unit;
        }
    }
}