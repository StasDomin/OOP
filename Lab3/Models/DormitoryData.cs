using System.Text.Json.Serialization;

namespace DormitoryJsonManager.Models;

public class DormitoryData
{
    [JsonPropertyName("DormitoryName")]
    public string DormitoryName { get; set; } = "Студентський гуртожиток КНУ";

    [JsonPropertyName("Residents")]
    public List<Resident> Residents { get; set; } = new();
}
