using System;
using System.IO;

namespace Statistics_Calculator
{
    public class RoundUpCalculator : Calculator
    {
        public override Result Calculate(FileStream inputStream)
        {
            Result result = base.Calculate(inputStream);
            result.Entropy = Math.Round(result.Entropy, 2);
            result.MaxEntropy = Math.Round(result.MaxEntropy, 2);
            result.Redundancy = Math.Round(result.Redundancy, 1);
            return result;
        }
    }
}