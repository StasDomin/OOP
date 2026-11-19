using DormitoryXmlLab2.Models;

namespace DormitoryXmlLab2.Strategies;

public class XmlSearchContext
{
    private IXmlSearchStrategy _strategy;

    public XmlSearchContext(IXmlSearchStrategy initialStrategy)
    {
        _strategy = initialStrategy;
    }

    public void SetStrategy(IXmlSearchStrategy strategy)
    {
        _strategy = strategy;
    }

    public string CurrentStrategyName => _strategy.StrategyName;

    public List<ResidentRecord> ExecuteSearch(string xmlFilePath, SearchCriteria criteria)
    {
        return _strategy.Search(xmlFilePath, criteria);
    }
}
