using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Xml.Serialization;
using WPFMVVM.Models;

namespace WPFMVVM.Services
{
    internal class DataService
    {
        private const string _DataSourceAdress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
       
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            //нам интересны в первую очередь заголовки
            var response = await client.GetAsync(_DataSourceAdress, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line
                    .Replace("Korea,", "Korea - ")
                    .Replace("Bonaire", "Bonaire -");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)//Провинция, страна, долгота, широта
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))//культура даты - формат, независимый от системы
            .ToArray();

        private static IEnumerable<(string Province, string Country, (double Lat, double Lon) Place, int[] Counts)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)//заголовок
                .Select(line => line.Split(','));
            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country = row[1].Trim(' ', '"');
                var latitude = double.Parse(row[2]);
                var longtitude = double.Parse(row[3]);
                var counts = row.Skip((row[4].Equals("-68.2385")) ? 5 : 4)//Очень странный баг, что заходило в 4й столбец, где 4е поле равно -68.2385
                    .Select(int.Parse)
                    .ToArray();
                yield return (province, country, (latitude, longtitude), counts);
            }
        }

        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();
            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach(var counry_info in data)
            {
                var country = new CountryInfo
                {
                    Name = counry_info.Key,
                    Provinces = counry_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts = dates.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
                    })
                };
                yield return country;
            }
        }
    }
}
