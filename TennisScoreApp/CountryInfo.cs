using System.Drawing;

namespace TennisScoreApp
{
    public class CountryInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Image Flag { get; set; }
        public override string ToString() => $"{Code}, {Name}";
    }
}
