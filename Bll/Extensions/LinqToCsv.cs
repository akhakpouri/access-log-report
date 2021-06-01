using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogReport.Bll.Extensions
{
    internal static class LinqToCSV
    {
        internal static string ToCsv<T>(this IEnumerable<T> items) where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();
            var index = 0;
            foreach (T item in items)
            {
                string line = string.Empty;
                if (index == 0)
                {
                    line += string.Join(",", properties.Select(p => p.Name.CreateHeader()).ToArray());
                    csvBuilder.AppendLine(line);
                }
                line = string.Join(",", properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());
                csvBuilder.AppendLine(line);
                index++;
            }
            return csvBuilder.ToString();
        }

        static string CreateHeader<T>(this T item)
        {
            if (item == null) return "\"\"";
            return string.Format("\"{0}\"", item);
        }

        static string ToCsvValue<T>(this T item)
        {
            if (item == null) return "\"\"";

            if (item is string)
            {
                return $"\"{item.ToString().Replace("\"", "\\\"")}\"";
            }
            if (double.TryParse(item.ToString(), out double dummy))
            {
                return $"{item}";
            }
            return $"\"{item}\"";
        }
    }
}
