namespace StarApi.ModelView
{
    public class ReportView
    {
        public string Name { get; set; }
        public string Shape { get; set; }
        public double Size { get; set; }
        public string Stars { get; set; }
        public string Planets { get; set; }
        public string Composition { get; set; }

        public ReportView(string name, string shape, double size, string stars, string planets, string Compostion)
        {
            Name = name;
            Shape = shape;
            Size = size;
            Stars = stars;
            Planets = planets;
            Composition = Compostion;
        }
    }
}
