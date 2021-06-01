using LogReport.Bll.Dto;
using LogReport.Bll.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogReport.Bll.Managers
{
    public class ReportManager : IReportManager
    {
        readonly IParser _parser;

        public ReportManager(IParser parser)
        {
            _parser = parser;
        }

        public string Process(string path)
        {
            var events = _parser.Parse(path);
            var result = events.GroupBy(e => e.c_ip)
                .Select(group => new Result
                {
                    Quantity = group.Count(),
                    IpAddress = group.Key                    
                })
                .OrderByDescending(r => r.Quantity);
            return WriteToCsv(result);
        }

        static string WriteToCsv(IEnumerable<Result> result)
        {
            var csv = Encoding.Default.GetBytes(result.ToCsv());
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, $"Report-{Guid.NewGuid()}.csv");
            File.WriteAllBytes(path, csv);
            return path;
        }
    }
}
