using System.Collections.Generic;

namespace Statistics_Calculator
{
    public class Result
    {
        public long TotalCount { get; set; }

        public long UniqueCount { get; set; }

        public double Entropy { get; set; }

        public double MaxEntropy { get; set; }

        public double Redundancy{ get; set; }

        public IEnumerable<double> Probabilities { get; set; }
    }
}