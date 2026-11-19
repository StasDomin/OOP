using System.Xml;
using DormitoryXmlLab2.Models;

namespace DormitoryXmlLab2.Strategies;

public class DomXmlSearchStrategy : IXmlSearchStrategy
{
    public string StrategyName => "DOM API (XmlDocument)";

    public List<ResidentRecord> Search(string xmlFilePath, SearchCriteria criteria)
    {
        var results = new List<ResidentRecord>();
        var document = new XmlDocument();
        document.Load(xmlFilePath);

        XmlNodeList? residentNodes = document.SelectNodes("//Resident");
        if (residentNodes is null)
        {
            return results;
        }

        foreach (XmlNode node in residentNodes)
        {
            XmlAttributeCollection? attrs = node.Attributes;
            if (attrs is null)
            {
                continue;
            }

            var record = new ResidentRecord
            {
                FullName = attrs["FullName"]?.Value ?? string.Empty,
                Faculty = attrs["Faculty"]?.Value ?? string.Empty,
                Department = attrs["Department"]?.Value ?? string.Empty,
                Course = attrs["Course"]?.Value ?? string.Empty,
                Benefit = attrs["Benefit"]?.Value ?? "Немає",
                Phone = attrs["Phone"]?.Value ?? "Не вказано"
            };

            XmlNode? accNode = node.SelectSingleNode("Accommodation");
            if (accNode?.Attributes is not null)
            {
                record.Building = accNode.Attributes["Building"]?.Value ?? string.Empty;
                record.Room = accNode.Attributes["Room"]?.Value ?? string.Empty;
                record.CheckInDate = accNode.Attributes["CheckInDate"]?.Value ?? string.Empty;
                record.CheckOutDate = accNode.Attributes["CheckOutDate"]?.Value ?? string.Empty;
            }

            if (criteria.Matches(record))
            {
                results.Add(record);
            }
        }

        return results;
    }
}
