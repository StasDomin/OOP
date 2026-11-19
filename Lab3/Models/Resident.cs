using System.Text.Json.Serialization;

namespace DormitoryJsonManager.Models;

public class Resident
{
    [JsonPropertyName("FullName")]
    public string FullName { get; set; } = string.Empty;

    [JsonPropertyName("Faculty")]
    public string Faculty { get; set; } = string.Empty;

    [JsonPropertyName("Department")]
    public string Department { get; set; } = string.Empty;

    [JsonPropertyName("Course")]
    public int Course { get; set; } = 1;

    [JsonPropertyName("Benefit")]
    public string? Benefit { get; set; }

    [JsonPropertyName("Phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("Accommodation")]
    public Accommodation Accommodation { get; set; } = new();

    [JsonIgnore]
    public string BenefitDisplay => string.IsNullOrWhiteSpace(Benefit) ? "Немає" : Benefit;

    [JsonIgnore]
    public string PhoneDisplay => string.IsNullOrWhiteSpace(Phone) ? "Не вказано" : Phone;
}
