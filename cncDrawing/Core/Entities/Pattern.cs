namespace Core.Entities
{
    public partial class Pattern
    {
        public string Name { get; set; } = "";
        public List<CutLine> Lines { get; set; } = new();

        public double Left => Lines.SelectMany(x => x.Points).Min(x => x.X);
        public double Top => Lines.SelectMany(x => x.Points).Min(x => x.Y);

        public double Width => Lines.SelectMany(x => x.Points).Max(x => x.X);
        public double Height => Lines.SelectMany(x => x.Points).Max(x => x.Y);
    }
}
