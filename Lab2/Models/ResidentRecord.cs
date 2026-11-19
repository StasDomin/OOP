namespace DormitoryXmlLab2.Models;

public class ResidentRecord
{
    public string FullName { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
    public string Benefit { get; set; } = "Немає";
    public string Phone { get; set; } = "Не вказано";
    public string Building { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string CheckInDate { get; set; } = string.Empty;
    public string CheckOutDate { get; set; } = string.Empty;

    public string AccommodationSummary => $"{Building}, кімн. {Room} ({CheckInDate} — {CheckOutDate})";
    public string ExtraAttributesSummary => $"Пільга: {Benefit} | Тел.: {Phone}";
}
