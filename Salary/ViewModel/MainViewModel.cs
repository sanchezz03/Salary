using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Salary.Services;
using Salary.Strategies;
using System.Collections.ObjectModel;

namespace Salary.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly XmlProcessor _xmlProcessor;
    private readonly XmlToHtmlTransformer _xmlToHtmlTransformer;

    [ObservableProperty]
    private ObservableCollection<List<Scientist>> scientists;

    [ObservableProperty]
    private string searchValue;

    [ObservableProperty]
    string absolutePath;

    [ObservableProperty]
    string htmlOutput;

    public MainViewModel()
    {
        scientists = new ObservableCollection<List<Scientist>>();
        _xmlProcessor = new XmlProcessor();
        _xmlToHtmlTransformer = new XmlToHtmlTransformer();
    }

    [RelayCommand]
    private void LoadXmlFile()
    {
        absolutePath = new FileLoaderService().LoadFile();
    }

    [RelayCommand]
    private void ProcessWithSax()
    {
        if (absolutePath == null)
        {
            return;
        }

        try
        {
            _xmlProcessor.SetStrategy(new SaxProcessing());
            var scientists = _xmlProcessor.ProcessXml(absolutePath, SearchValue);
            Scientists.Clear();
            Scientists.Add(scientists);

            TransformXmlToHtmlAsync(scientists);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [RelayCommand]
    private void ProcessWithDom()
    {
        if (absolutePath == null)
        {
            return;
        }

        try
        {
            _xmlProcessor.SetStrategy(new DomProcessing());
            var scientists = _xmlProcessor.ProcessXml(absolutePath, SearchValue);
            Scientists.Clear();
            Scientists.Add(scientists);

            TransformXmlToHtmlAsync(scientists);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [RelayCommand]
    private void ProcessWithLinq()
    {
        if (absolutePath == null)
        {
            return;
        }

        try
        {
            _xmlProcessor.SetStrategy(new LinqToXmlProcessing());
            var scientists = _xmlProcessor.ProcessXml(absolutePath, SearchValue);
            Scientists.Clear();
            Scientists.Add(scientists);

            TransformXmlToHtmlAsync(scientists);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [RelayCommand]
    private void Clear()
    {
        Scientists.Clear();
        SearchValue = "";
    }

    private async Task TransformXmlToHtmlAsync(List<Scientist> scientists)
    {
        if (absolutePath == null)
        {
            return;
        }

        try
        {
            string xslFilePath = @"D:\Alex\Work\Freelance\MAUI\Salary\Salary\Salary\Resources\Xsl\html.xslt";  
            string outputHtmlPath = @"D:\Alex\Work\Freelance\MAUI\Salary\Salary\Salary\Resources\Data\salary.html"; 

            _xmlToHtmlTransformer.TransformXmlToHtml(scientists, xslFilePath, outputHtmlPath);

            try
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync(outputHtmlPath))
                {
                    using (var reader = new StreamReader(stream))
                    {
      
                        var htmlContent = await reader.ReadToEndAsync();

                        HtmlOutput = htmlContent;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading HTML content: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error transforming XML to HTML: {ex.Message}");
        }
    }
}
