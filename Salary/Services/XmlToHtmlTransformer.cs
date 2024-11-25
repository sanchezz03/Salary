using System.Xml.Xsl;
using System.Xml;
using System.Text;

namespace Salary.Services;

public class XmlToHtmlTransformer
{
    public void TransformXmlToHtml(List<Scientist> scientists, string xslFilePath, string outputHtmlPath)
    {
        var xmlString = GenerateXmlFromList(scientists);

        using (var reader = new StringReader(xmlString))
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);

            var xslt = new XslCompiledTransform();
            xslt.Load(xslFilePath);

            using (var writer = XmlWriter.Create(outputHtmlPath, xslt.OutputSettings))
            {
                xslt.Transform(xmlDoc, writer);
            }
        }
    }

    private string GenerateXmlFromList(List<Scientist> scientists)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<Scientists>");

        foreach (var scientist in scientists)
        {
            sb.AppendLine("<Scientist>");
            sb.AppendLine($"<ID>{scientist.Id}</ID>");
            sb.AppendLine($"<Type>{scientist.Type}</Type>");
            sb.AppendLine($"<FullName>{scientist.FullName}</FullName>");
            sb.AppendLine($"<Faculty>{scientist.Faculty}</Faculty>");
            sb.AppendLine($"<Department>{scientist.Department}</Department>");
            sb.AppendLine($"<Position>{scientist.Position}</Position>");
            sb.AppendLine($"<Salary>{scientist.Salary}</Salary>");
            sb.AppendLine($"<YearsOnPosition>{scientist.YearsOnPosition}</YearsOnPosition>");
            sb.AppendLine("</Scientist>");
        }

        sb.AppendLine("</Scientists>");
        return sb.ToString();
    }
}
