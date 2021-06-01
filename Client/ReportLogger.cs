using LogReport.Bll;
using System;

namespace LogReport.Client
{
    public class ReportLogger
    {
        readonly IReportManager _reportManager;

        public ReportLogger(IReportManager reportManager)
        {
            _reportManager = reportManager;
        }

        public void LogReport(string path)
        {
            var csvPath = _reportManager.Process(path);            
            Console.WriteLine($"You can find the result here: {csvPath}");
            Console.WriteLine("Finished");
        }        
    }
}
