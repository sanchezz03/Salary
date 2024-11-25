namespace Salary.Strategies;

public interface IXmlProcessingStrategy
{
    List<Scientist> ProcessXml(string filePath, string searchKeyword);
}
