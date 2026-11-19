using System.Text.Json.Serialization;

namespace DormitoryJsonManager.Models;

public class Accommodation
{
    [JsonPropertyName("Building")]
    public string Building { get; set; } = string.Empty;

    [JsonPropertyName("Room")]
    public string Room { get; set; } = string.Empty;

    [JsonPropertyName("CheckInDate")]
    public string CheckInDate { get; set; } = string.Empty;

    [JsonPropertyName("CheckOutDate")]
    public string CheckOutDate { get; set; } = string.Empty;

    [JsonIgnore]
    public string LocationDisplay => $"{Building}, кімн. {Room}";

    [JsonIgnore]
    public string PeriodDisplay => $"{CheckInDate} — {CheckOutDate}";
}
