using System.Globalization;
using System.Xml.Linq;

namespace Salary.Strategies;
public class LinqToXmlProcessing : IXmlProcessingStrategy
{
    public List<Scientist> ProcessXml(string filePath, string searchKeyword)
    {
        var document = XDocument.Load(filePath);

        var scientists = document.Descendants("Scientist")
            .Select(x => new Scientist
            {
                Id = (string)x.Attribute("id"),
                Type = (string)x.Attribute("type"),
                FullName = (string)x.Element("FullName"),
                Faculty = (string)x.Element("Faculty"),
                Department = (string)x.Element("Department"),
                Position = (string)x.Element("Position"),
                Salary = decimal.Parse(x.Element("Salary")?.Value, CultureInfo.InvariantCulture),
                YearsOnPosition = (int?)x.Element("YearsOnPosition") ?? 0
            })
            .Where(scientist => ContainsKeyword(scientist, searchKeyword))
            .ToList();

        return scientists;
    }

    private bool ContainsKeyword(Scientist scientist, string keyword)
    {
        return scientist.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
               scientist.Faculty.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
               scientist.Department.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
               scientist.Position.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }
}
