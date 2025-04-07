namespace Core.Entities
{
    public partial class Pattern
    {
        public static Pattern Demo = new()
        {
            Name = "Demo Pattern",
            Lines = [
        new CutLine
                {
                    Points =   [
                        new LinePoint { X = 0, Y = 0 },
                        new LinePoint { X = 10, Y = 0 },
                        new LinePoint { X = 10, Y = 10 },
                        new LinePoint { X = 0, Y = 10 },
                        new LinePoint { X = 0, Y = 0 }
                    ]
                },
                new CutLine
                {
                    Points =[
                        new() { X = 1, Y = 1 },
                        new() { X = 9, Y = 9 }
                    ]
                },
                new()
                {
                        Points =[
                        new LinePoint { X = 1, Y = 9 },
                        new LinePoint { X = 9, Y = 1 }
                    ]
                }
    ]
        };
    }
}
