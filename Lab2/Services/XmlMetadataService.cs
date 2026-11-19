using System.Xml.Linq;

namespace DormitoryXmlLab2.Services;

public class XmlMetadataService
{
    public (List<string> Faculties, List<string> Departments, List<string> Courses) LoadFilterOptions(string xmlFilePath)
    {
        XDocument doc = XDocument.Load(xmlFilePath);
        var residents = doc.Descendants("Resident").ToList();

        var faculties = new List<string> { "Усі" };
        faculties.AddRange(residents
            .Select(r => (string?)r.Attribute("Faculty"))
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Distinct()
            .OrderBy(v => v)!);

        var departments = new List<string> { "Усі" };
        departments.AddRange(residents
            .Select(r => (string?)r.Attribute("Department"))
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Distinct()
            .OrderBy(v => v)!);

        var courses = new List<string> { "Усі" };
        courses.AddRange(residents
            .Select(r => (string?)r.Attribute("Course"))
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Distinct()
            .OrderBy(v => v)!);

        return (faculties, departments, courses);
    }
}
