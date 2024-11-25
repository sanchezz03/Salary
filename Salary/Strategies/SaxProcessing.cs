using System.Xml;

namespace Salary.Strategies;

public class SaxProcessing : IXmlProcessingStrategy
{
    public List<Scientist> ProcessXml(string filePath, string searchKeyword)
    {
        var scientists = new List<Scientist>();
        Scientist currentScientist = null;

        using var reader = XmlReader.Create(filePath);
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Scientist")
            {
                currentScientist = new Scientist
                {
                    Id = reader.GetAttribute("id"),
                    Type = reader.GetAttribute("type")
                };
            }

            if (currentScientist != null && reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "FullName":
                        currentScientist.FullName = reader.ReadElementContentAsString();
                        break;
                    case "Faculty":
                        currentScientist.Faculty = reader.ReadElementContentAsString();
                        break;
                    case "Department":
                        currentScientist.Department = reader.ReadElementContentAsString();
                        break;
                    case "Position":
                        currentScientist.Position = reader.ReadElementContentAsString();
                        break;
                    case "Salary":
                        currentScientist.Salary = reader.ReadElementContentAsDecimal();
                        break;
                    case "YearsOnPosition":
                        currentScientist.YearsOnPosition = reader.ReadElementContentAsInt();
                        break;
                }
            }

            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Scientist")
            {
                if (ContainsKeyword(currentScientist, searchKeyword))
                {
                    scientists.Add(currentScientist);
                }

                currentScientist = null;
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