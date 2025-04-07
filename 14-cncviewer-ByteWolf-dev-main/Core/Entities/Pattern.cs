namespace Core.Entities
{
    public partial class Pattern
    {
        public string Name { get; set; } = "";
        public List<CutLine> Lines { get; set; } = [];
        public double Width => throw new NotImplementedException();
        public double Height => throw new NotImplementedException();

        public double Left => throw new NotImplementedException();
        public double Top => throw new NotImplementedException();
    }
}
