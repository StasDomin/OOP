using System;
using System.Collections.Generic;

namespace Task5.Models
{
    public class Book : Product
    {
        private int _pageCount;
        private string _publisher;
        private List<string> _authors;

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        public List<string> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        public override string Category
        {
            get { return "Книги"; }
        }

        public override string Details
        {
            get
            {
                string authorsList = _authors != null && _authors.Count > 0
                    ? string.Join(", ", _authors)
                    : "Не вказано";

                return $"{Description} | Вид-во: {_publisher}, {_pageCount} стор. | Автори: {authorsList}";
            }
        }

        public Book(
            decimal price,
            string countryOfOrigin,
            string name,
            DateTime packagingDate,
            string description,
            int pageCount,
            string publisher,
            List<string> authors)
            : base(price, countryOfOrigin, name, packagingDate, description)
        {
            _pageCount = pageCount;
            _publisher = publisher;
            _authors = authors;
        }
    }
}