using Salary.Strategies;

namespace Salary.Services;

public class XmlProcessor
{
    private IXmlProcessingStrategy _strategy;

    public void SetStrategy(IXmlProcessingStrategy strategy)
    {
        _strategy = strategy;
    }

    public List<Scientist> ProcessXml(string filePath, string searchValue)
    {
        if (_strategy == null)
            throw new InvalidOperationException("Processing strategy is not set.");

        return _strategy.ProcessXml(filePath, searchValue);
    }
}
