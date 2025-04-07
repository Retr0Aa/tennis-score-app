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
                new CountryInfo { Code = "bg", Name = "Bulgaria", Flag = Properties.Resources.bg },
                new CountryInfo { Code = "fr", Name = "France", Flag = Properties.Resources.fr },
                new CountryInfo { Code = "es", Name = "Spain", Flag = Properties.Resources.es },
                new CountryInfo { Code = "cn", Name = "China", Flag = Properties.Resources.cn },
                new CountryInfo { Code = "it", Name = "Italy", Flag = Properties.Resources.it },
                new CountryInfo { Code = "tr", Name = "Turkey", Flag = Properties.Resources.tr },
                new CountryInfo { Code = "mx", Name = "Mexico", Flag = Properties.Resources.mx },
                new CountryInfo { Code = "th", Name = "Thailand", Flag = Properties.Resources.th },
                new CountryInfo { Code = "de", Name = "Germany", Flag = Properties.Resources.de },
                new CountryInfo { Code = "jp", Name = "Japan", Flag = Properties.Resources.jp },
                new CountryInfo { Code = "at", Name = "Austria", Flag = Properties.Resources.at },
                new CountryInfo { Code = "gr", Name = "Greece", Flag = Properties.Resources.gr },
                new CountryInfo { Code = "my", Name = "Malaysia", Flag = Properties.Resources.my },
                new CountryInfo { Code = "ru", Name = "Russia", Flag = Properties.Resources.ru },
                new CountryInfo { Code = "ca", Name = "Canada", Flag = Properties.Resources.ca },
                new CountryInfo { Code = "pl", Name = "Poland", Flag = Properties.Resources.pl },
                new CountryInfo { Code = "nl", Name = "Netherlands", Flag = Properties.Resources.nl }
                // More Icons coming soon
            };
        }
    }
}
