using DormitoryJsonManager.Models;

namespace DormitoryJsonManager.Services;

public class DormitorySearchService
{
    public List<Resident> Search(
        IEnumerable<Resident> residents,
        string? faculty,
        string? department,
        string? course,
        string? keyword)
    {
        IEnumerable<Resident> query = residents;

        if (!string.IsNullOrWhiteSpace(faculty) && faculty != "Усі")
        {
            query = query.Where(r => string.Equals(r.Faculty, faculty, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(department) && department != "Усі")
        {
            query = query.Where(r => string.Equals(r.Department, department, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(course) && course != "Усі" && int.TryParse(course, out int parsedCourse))
        {
            query = query.Where(r => r.Course == parsedCourse);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            string term = keyword.Trim();
            query = query.Where(r =>
                r.FullName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                r.Accommodation.Building.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                r.Accommodation.Room.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                r.BenefitDisplay.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                r.PhoneDisplay.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        return query.OrderBy(r => r.FullName).ToList();
    }

    public List<string> GetDistinctFaculties(IEnumerable<Resident> residents)
    {
        List<string> items = new() { "Усі" };
        items.AddRange(residents
            .Select(r => r.Faculty)
            .Where(f => !string.IsNullOrWhiteSpace(f))
            .Distinct()
            .OrderBy(f => f));
        return items;
    }

    public List<string> GetDistinctDepartments(IEnumerable<Resident> residents)
    {
        List<string> items = new() { "Усі" };
        items.AddRange(residents
            .Select(r => r.Department)
            .Where(d => !string.IsNullOrWhiteSpace(d))
            .Distinct()
            .OrderBy(d => d));
        return items;
    }

    public List<string> GetDistinctCourses(IEnumerable<Resident> residents)
    {
        List<string> items = new() { "Усі" };
        items.AddRange(residents
            .Select(r => r.Course.ToString())
            .Distinct()
            .OrderBy(c => c));
        return items;
    }
}
