using System.Collections.Generic;
using System.Drawing;

namespace TennisScoreApp
{
    public static class CountryData
    {
        public static List<CountryInfo> GetCountries()
        {
            return new List<CountryInfo>
            {
                new CountryInfo { Code = "us", Name = "United States", Flag = Properties.Resources.us },
                new CountryInfo { Code = "gb", Name = "United Kingdom", Flag = Properties.Resources.gb },
                new CountryInfo { Code = "fr", Name = "France", Flag = Properties.Resources.fr },
                new CountryInfo { Code = "de", Name = "Germany", Flag = Properties.Resources.de },
                new CountryInfo { Code = "bg", Name = "Bulgaria", Flag = Properties.Resources.bg }
                // More Icons coming soon
            };
        }
    }
}
