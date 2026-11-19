namespace DormitoryXmlLab2.Models;

public class SearchCriteria
{
    public string Keyword { get; set; } = string.Empty;
    public string Faculty { get; set; } = "Усі";
    public string Department { get; set; } = "Усі";
    public string Course { get; set; } = "Усі";

    public bool Matches(ResidentRecord record)
    {
        if (!string.IsNullOrWhiteSpace(Faculty) && Faculty != "Усі" &&
            !record.Faculty.Equals(Faculty, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(Department) && Department != "Усі" &&
            !record.Department.Equals(Department, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(Course) && Course != "Усі" &&
            !record.Course.Equals(Course, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(Keyword))
        {
            string kw = Keyword.Trim();
            bool found =
                record.FullName.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                record.Faculty.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                record.Department.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                record.Course.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                record.Benefit.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                record.Phone.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                record.Building.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                record.Room.Contains(kw, StringComparison.OrdinalIgnoreCase);

            if (!found)
            {
                return false;
            }
        }

        return true;
    }
}
