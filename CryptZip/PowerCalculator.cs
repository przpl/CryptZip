using System;

namespace CryptZip
{
    public static class PowerCalculator
    {
        /// <summary>
        /// Calculates two to the power X.
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public static uint Two(int power)
        {
            switch (power)
            {
                case 0:
                    return 1;
                case 8:
                    return 256;
                case 16:
                    return 65536;
                case 24:
                    return 16777216;
                default:
                    throw new ArgumentOutOfRangeException("Cannot calculate power of two to the " + power + ".");
            }
        }

        public static long Two64Bit(int power)
        {
            if (power == 32)
                return 4294967296;

            throw new ArgumentOutOfRangeException("Cannot calculate power of two to the " + power + ".");
        }
    }
}
