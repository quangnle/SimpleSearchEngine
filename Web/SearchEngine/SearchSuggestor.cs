using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.SearchEngine
{
    public class SearchSuggestor
    {
        public IStringMetric Metric { get; set; }
        public List<string> Suggest(string input, int minDistance)
        {
            var result = new List<string>();
            var repo = new SEArticleRepository();
            var keywords = repo.GetAllKeywordsSingle();

            foreach (var keyword in keywords)
            {
                if (Metric.GetDistance(input, keyword) <= minDistance)
                    result.Add(keyword);
            }

            return result;
        }
    }
}
