namespace Salary.Services;

public class FileLoaderService
    {
        public string LoadFile()
        {
            var fileResult = @"D:\Alex\Work\Freelance\MAUI\Salary\Salary\Salary\Resources\Data\data.xml";

            return fileResult;
        }

        public void SaveFile(string content, string filePath)
        {
            File.WriteAllText(filePath, content);
        }
    }
