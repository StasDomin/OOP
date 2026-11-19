using System.Xml;
using DormitoryXmlLab2.Models;

namespace DormitoryXmlLab2.Strategies;

public class SaxXmlSearchStrategy : IXmlSearchStrategy
{
    public string StrategyName => "SAX API (XmlReader)";

    public List<ResidentRecord> Search(string xmlFilePath, SearchCriteria criteria)
    {
        var results = new List<ResidentRecord>();
        ResidentRecord? currentResident = null;

        using var reader = XmlReader.Create(xmlFilePath);
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                if (reader.Name == "Resident")
                {
                    currentResident = new ResidentRecord
                    {
                        FullName = reader.GetAttribute("FullName") ?? string.Empty,
                        Faculty = reader.GetAttribute("Faculty") ?? string.Empty,
                        Department = reader.GetAttribute("Department") ?? string.Empty,
                        Course = reader.GetAttribute("Course") ?? string.Empty,
                        Benefit = reader.GetAttribute("Benefit") ?? "Немає",
                        Phone = reader.GetAttribute("Phone") ?? "Не вказано"
                    };
                }
                else if (reader.Name == "Accommodation" && currentResident is not null)
                {
                    currentResident.Building = reader.GetAttribute("Building") ?? string.Empty;
                    currentResident.Room = reader.GetAttribute("Room") ?? string.Empty;
                    currentResident.CheckInDate = reader.GetAttribute("CheckInDate") ?? string.Empty;
                    currentResident.CheckOutDate = reader.GetAttribute("CheckOutDate") ?? string.Empty;
                }
            }
            else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Resident")
            {
                if (currentResident is not null && criteria.Matches(currentResident))
                {
                    results.Add(currentResident);
                }
                currentResident = null;
            }
        }

        return results;
    }
}
