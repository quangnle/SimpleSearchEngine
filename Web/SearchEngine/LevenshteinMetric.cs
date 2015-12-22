using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.SearchEngine
{
    public class LevenshteinMetric : IStringMetric
    {
        public int GetDistance(string st1, string st2)
        {
            // degenerate cases
            if (st1 == st2) return 0;
            if (st1.Length == 0) return st2.Length;
            if (st2.Length == 0) return st1.Length;

            // create two work vectors of integer distances
            int[] v0 = new int[st2.Length + 1];
            int[] v1 = new int[st2.Length + 1];

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < st1.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0

                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < st2.Length; j++)
                {
                    var cost = (st1[i] == st2[j]) ? 0 : 1;
                    v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[st2.Length];
        }
    }
}
