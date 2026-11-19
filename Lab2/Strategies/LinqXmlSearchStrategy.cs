using System.Xml.Linq;
using DormitoryXmlLab2.Models;

namespace DormitoryXmlLab2.Strategies;

public class LinqXmlSearchStrategy : IXmlSearchStrategy
{
    public string StrategyName => "LINQ to XML (XDocument)";

    public List<ResidentRecord> Search(string xmlFilePath, SearchCriteria criteria)
    {
        XDocument doc = XDocument.Load(xmlFilePath);

        var query = from resident in doc.Descendants("Resident")
                    let acc = resident.Element("Accommodation")
                    select new ResidentRecord
                    {
                        FullName = (string?)resident.Attribute("FullName") ?? string.Empty,
                        Faculty = (string?)resident.Attribute("Faculty") ?? string.Empty,
                        Department = (string?)resident.Attribute("Department") ?? string.Empty,
                        Course = (string?)resident.Attribute("Course") ?? string.Empty,
                        Benefit = (string?)resident.Attribute("Benefit") ?? "Немає",
                        Phone = (string?)resident.Attribute("Phone") ?? "Не вказано",
                        Building = (string?)acc?.Attribute("Building") ?? string.Empty,
                        Room = (string?)acc?.Attribute("Room") ?? string.Empty,
                        CheckInDate = (string?)acc?.Attribute("CheckInDate") ?? string.Empty,
                        CheckOutDate = (string?)acc?.Attribute("CheckOutDate") ?? string.Empty
                    };

        return query.Where(criteria.Matches).ToList();
    }
}
