namespace Core.Entities
{

    public class CutLine
    {
        public List<LinePoint> Points { get; set; } = [];
        public double Width => throw new NotImplementedException();
        public double Height => throw new NotImplementedException();
    }
}
