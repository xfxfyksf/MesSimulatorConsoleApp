using System.Reflection;

namespace MesSimulatorConsoleApp.Helpers;

internal class FileLoader
{
    public static async Task<string> LoadText(string relativeFilePath)
    {
        var directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
        if (directoryName != null)
        {
            var filePath = Path.Combine(directoryName, relativeFilePath);
            var sourcePath = Path.GetFullPath(filePath);
            if (File.Exists(sourcePath))
            {
                return await File.ReadAllTextAsync(sourcePath);
            }
        }

        throw new Exception($"File is not exist, relativeFilePath: {relativeFilePath}");
    }
}
