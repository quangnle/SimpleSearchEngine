using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.SearchEngine
{
    public interface IStringMetric
    {
        int GetDistance(string st1, string st2);
    }
}
