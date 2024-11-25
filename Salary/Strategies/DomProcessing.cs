using System.Globalization;
using System.Xml;

namespace Salary.Strategies;

public class DomProcessing : IXmlProcessingStrategy
{
    public List<Scientist> ProcessXml(string filePath, string searchKeyword)
    {
        var scientists = new List<Scientist>();

        var document = new XmlDocument();
        document.Load(filePath);

        foreach (XmlNode node in document.SelectNodes("//Scientist"))
        {
            var scientist = new Scientist
            {
                Id = node.Attributes["id"]?.Value,
                Type = node.Attributes["type"]?.Value,
                FullName = node["FullName"]?.InnerText,
                Faculty = node["Faculty"]?.InnerText,
                Department = node["Department"]?.InnerText,
                Position = node["Position"]?.InnerText,
                Salary = decimal.Parse(node["Salary"]?.InnerText, CultureInfo.InvariantCulture),
                YearsOnPosition = int.TryParse(node["YearsOnPosition"]?.InnerText, out var years) ? years : 0
            };

            if (ContainsKeyword(scientist, searchKeyword))
            {
                scientists.Add(scientist);
            }
        }

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
