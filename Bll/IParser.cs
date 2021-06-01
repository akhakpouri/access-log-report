using System.Collections.Generic;
using Tx.Windows;

namespace LogReport.Bll
{
    public interface IParser
    {
        IEnumerable<W3CEvent> Parse(string path);
    }
}
