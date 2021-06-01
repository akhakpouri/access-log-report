using System.Collections.Generic;
using System.Linq;
using Tx.Windows;

namespace LogReport.Bll
{
    public class LogParser : IParser
    {
        public IEnumerable<W3CEvent> Parse(string path)
        {
            var log = W3CEnumerable.FromFile(path);
            return log.Where(l => l.c_ip is not null && !l.c_ip.StartsWith("207.114") &&
                l.cs_method is not null && l.cs_method == "GET" &&
                l.s_port is not null && l.s_port == "80").AsEnumerable();
        }
    }
}
