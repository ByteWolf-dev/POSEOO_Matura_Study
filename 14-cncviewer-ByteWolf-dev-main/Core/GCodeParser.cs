using Core.Entities;

namespace Core
{
    public class GCodeParser
    {
        public static async Task<Pattern> ParsePatternFromGcodeAsync(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);
            throw new NotImplementedException();
        }
    }
}
