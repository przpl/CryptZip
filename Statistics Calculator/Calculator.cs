using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Statistics_Calculator
{
    public class Calculator
    {
        private long _totalCount;

        public Result Calculate(FileStream inputStream)
        {
            int[] occurences = new int[256];
            while (inputStream.Position < inputStream.Length)
                occurences[inputStream.ReadByte()]++;

            _totalCount = inputStream.Length;
            IEnumerable<double> probabilities = occurences.Select(GetProbability);

            return new Result
            {
                TotalCount = inputStream.Length,
                UniqueCount = occurences.Where(i => i > 0).Count(),
                Entropy = GetEntropy(probabilities),
                Probabilities = probabilities
            };
        }

        private double GetProbability(int value)
        {
            return (double)value/_totalCount;
        }

        private double GetEntropy(IEnumerable<double> probabilities)
        {
            double entropy = 0;

            foreach (var probability in probabilities)
                if (probability > 0)
                    entropy += probability*Math.Log(1/probability, 2);

            return entropy;
        }
    }

    public class Result
    {
        public long TotalCount { get; set; }

        public long UniqueCount { get; set; }

        public double Entropy { get; set; }

        public IEnumerable<double> Probabilities { get; set; }
    }
}
