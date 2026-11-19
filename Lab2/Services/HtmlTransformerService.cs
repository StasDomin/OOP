using System.Xml.Xsl;

namespace DormitoryXmlLab2.Services;

public class HtmlTransformerService
{
    public string TransformToHtml(string xmlFilePath, string xslFilePath, string outputHtmlPath)
    {
        var transform = new XslCompiledTransform();
        transform.Load(xslFilePath);
        transform.Transform(xmlFilePath, outputHtmlPath);
        return outputHtmlPath;
    }
}
