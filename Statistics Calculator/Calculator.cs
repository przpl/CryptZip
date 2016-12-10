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

            long totalCount = inputStream.Length;
            inputStream.Close();

            int uniqueCount = occurences.Where(i => i > 0).Count();
            double entropy = GetEntropy(probabilities);
            double maxEntropy = Math.Log(uniqueCount, 2);

            return new Result
            {
                TotalCount = totalCount,
                UniqueCount = uniqueCount,
                Entropy = entropy,
                MaxEntropy = maxEntropy,
                Redundancy = GetRedundancy(entropy, maxEntropy),
                Probabilities = probabilities
            };
        }

        private double GetRedundancy(double entropy, double maxEntropy)
        {
            return (1 - entropy/maxEntropy) * 100;
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
}
