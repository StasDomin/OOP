using System.Text.Json.Serialization;

namespace JsonTask
{
    public class Book
    {
        private int _publishingHouseId;
        private string _title;
        private PublishingHouse _publishingHouse;

        [JsonIgnore]
        public int PublishingHouseId
        {
            get { return _publishingHouseId; }
            set { _publishingHouseId = value; }
        }

        [JsonPropertyName("Name")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public PublishingHouse PublishingHouse
        {
            get { return _publishingHouse; }
            set { _publishingHouse = value; }
        }

        public Book()
        {
            _title = string.Empty;
            _publishingHouse = new PublishingHouse();
        }
    }
}