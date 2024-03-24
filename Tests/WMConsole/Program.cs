using System.Globalization;

namespace WMConsole
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            //нам интересны в первую очередь заголовки
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while(!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,","Korea - ");
            }
        }
        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)//Провинция, страна, долгота, широта
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))//культура даты - формат, независимый от системы
            .ToArray();

        //Плюс типа IEnumerable - ленивые методы, которые не тянут все данные
        //Тут используем кортеж данных
        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)//заголовок
                .Select(line => line.Split(','));
            foreach(var row in lines)
            {
                var province = row[0].Trim();
                var country = row[1].Trim(' ','"');
                var counts = row.Skip((row[4].Equals("-68.2385")) ? 5 : 4)//Очень странный баг, что заходило в 4й столбец с координатой в одном месте
                    .Select(int.Parse)
                    .ToArray();
                yield return (country, province, counts);
            }
        }
        static void Main(string[] args)
        {
            var russia_data = GetData().First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count) => $"{date:dd-MM}: {count}")));

            Console.ReadLine();
        }
    }
}