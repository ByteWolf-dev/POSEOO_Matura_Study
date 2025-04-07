namespace Core.Entities
{

    public class CutLine
    {
        public List<LinePoint> Points { get; set; } = [];
        public double Width => Points.Max(x => x.X) - Points.Min(x => x.X);
        public double Height => Points.Max(x => x.Y) - Points.Min(x => x.Y);
    }
}
