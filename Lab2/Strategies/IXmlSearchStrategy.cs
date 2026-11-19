using DormitoryXmlLab2.Models;

namespace DormitoryXmlLab2.Strategies;

public interface IXmlSearchStrategy
{
    string StrategyName { get; }
    List<ResidentRecord> Search(string xmlFilePath, SearchCriteria criteria);
}
