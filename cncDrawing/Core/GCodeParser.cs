using Core.Entities;
using System.Runtime.CompilerServices;

namespace Core
{
    public class GCodeParser
    {
        public static async Task<Pattern> ParsePatternFromGcodeAsync(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);

            var pattern = new Pattern();

            pattern.Name = fileName;
            LinePoint prevpoint = null;

            foreach(var line in lines)
            {
                if(prevpoint != null && !line.Contains("Z") && line.Split(' ')[0] == "G01")
                {
                    CutLine cl = new CutLine();

                    cl.Points.Add(prevpoint);
                    cl.Points.Add(GetLinePointFromString(line));

                    pattern.Lines.Add(cl);
                }

                if (!line.Contains("Z"))
                {
                    prevpoint = GetLinePointFromString(line);
                }
            }

            return pattern;
        }

        private static LinePoint GetLinePointFromString(string line)
        {
            LinePoint lp = new LinePoint();

            lp.X = Convert.ToDouble(line.Split('X')[1].Split(' ')[0].Trim().Replace('.', ','));
            lp.Y = Convert.ToDouble(line.Split('Y')[1].Trim().Replace('.', ','));

            return lp;
        }
    }
}
