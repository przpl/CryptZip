using System;
using System.Collections.Generic;

namespace CryptZip
{
    public static class CollectionExtensions
    {
        public static T[] LastElements<T>(this List<T> list, int count)
        {
            var bytes = new T[count];
            int index = 0;

            for (int i = list.Count - count; i < list.Count; i++)
                bytes[index++] = list[i];

            return bytes;
        }

        public static T[] ShiftToLeft<T>(this T[] array)
        {
            var shifted = new T[array.Length];

            for (int j = 0; j < array.Length - 1; j++)
                shifted[j] = array[j + 1];

            shifted[shifted.Length - 1] = array[0];

            return shifted;
        }

        public static T[] ShiftToLeft<T>(this T[] array, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count has to be greater than one.");

            var shifted = new T[array.Length];

            Array.Copy(array, shifted, array.Length);

            for (int i = 0; i < count; i++)
                shifted = shifted.ShiftToLeft();

            return shifted;
        }

        public static T[] ShiftToRight<T>(this T[] array)
        {
            var shifted = new T[array.Length];

            shifted[0] = array[array.Length - 1];

            for (int j = 1; j < array.Length; j++)
                shifted[j] = array[j - 1];

            return shifted;
        }

        public static T[] ShiftToRight<T>(this T[] array, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count has to be greater than one.");

            var shifted = new T[array.Length];

            Array.Copy(array, shifted, array.Length);

            for (int i = 0; i < count; i++)
                shifted = shifted.ShiftToRight();

            return shifted;
        }

        public static byte[] XOR(this byte[] first, IEnumerable<byte> second)
        {
            var result = new byte[first.Length];

            int i = 0;
            foreach (var b in second)
            {
                result[i] = Convert.ToByte(first[i] ^ b);
                i++;
            }

            if (i < result.Length)
                throw new ArgumentException("Arrays have to be equal lengths.");

            return result;
        }

        public static uint[] XOR(this uint[] first, IEnumerable<uint> second)
        {
            var result = new uint[first.Length];

            int i = 0;
            foreach (var b in second)
            {
                result[i] = first[i] ^ b;
                i++;
            }

            if (i < result.Length)
                throw new ArgumentException("Arrays have to be equal lengths.");

            return result;
        }

        public static byte[] SubArray(this byte[] array, int from, int toExclusive)
        {
            var subArray = new byte[toExclusive - from];
            int index = 0;

            for (int i = from; i < toExclusive; i++)
                subArray[index++] = array[i];

            return subArray;
        }
    }
}