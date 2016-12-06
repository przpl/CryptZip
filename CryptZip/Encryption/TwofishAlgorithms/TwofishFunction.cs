using System;

namespace CryptZip.Encryption.TwofishAlgorithms
{
    public static class TwofishFunction
    {
        public static uint h(ITwofishMDS mds, uint x, uint[] L)
        {
            int k = L.Length;
            uint[] y = Word32Bits.ToUintBytes(x);

            if (k == 4)
            {
                y[0] = (uint)q1(y[0]) ^ Word32Bits.GetByte(L[3], 0);
                y[1] = (uint)q0(y[1]) ^ Word32Bits.GetByte(L[3], 1);
                y[2] = (uint)q0(y[2]) ^ Word32Bits.GetByte(L[3], 2);
                y[3] = (uint)q1(y[3]) ^ Word32Bits.GetByte(L[3], 3);
            }
            if (k >= 3)
            {
                y[0] = (uint)q1(y[0]) ^ Word32Bits.GetByte(L[2], 0);
                y[1] = (uint)q1(y[1]) ^ Word32Bits.GetByte(L[2], 1);
                y[2] = (uint)q0(y[2]) ^ Word32Bits.GetByte(L[2], 2);
                y[3] = (uint)q0(y[3]) ^ Word32Bits.GetByte(L[2], 3);
            }
            y[0] = (uint)(q0(q0(y[0]) ^ Word32Bits.GetByte(L[1], 0)) ^ Word32Bits.GetByte(L[0], 0));
            y[1] = (uint)(q0(q1(y[1]) ^ Word32Bits.GetByte(L[1], 1)) ^ Word32Bits.GetByte(L[0], 1));
            y[2] = (uint)(q1(q0(y[2]) ^ Word32Bits.GetByte(L[1], 2)) ^ Word32Bits.GetByte(L[0], 2));
            y[3] = (uint)(q1(q1(y[3]) ^ Word32Bits.GetByte(L[1], 3)) ^ Word32Bits.GetByte(L[0], 3));

            return mds.Multiply(y);
        }

        public static byte q0(int x)
        {
            return q0((byte)x);
        }

        public static byte q0(uint x)
        {
            return q0((byte)x);
        }

        public static byte q0(byte x)
        {
            byte[,] t =
            {
                {0x8, 0x1, 0x7, 0xD, 0x6, 0xF, 0x3, 0x2, 0x0, 0xB, 0x5, 0x9, 0xE, 0xC, 0xA, 0x4},
                {0xE, 0xC, 0xB, 0x8, 0x1, 0x2, 0x3, 0x5, 0xF, 0x4, 0xA, 0x6, 0x7, 0x0, 0x9, 0xD},
                {0xB, 0xA, 0x5, 0xE, 0x6, 0xD, 0x9, 0x0, 0xC, 0x8, 0xF, 0x3, 0x2, 0x4, 0x7, 0x1},
                {0xD, 0x7, 0xF, 0x4, 0x1, 0x2, 0x6, 0xE, 0x9, 0xB, 0x3, 0x0, 0x8, 0x5, 0xC, 0xA}
            };
            return q(x, t);
        }

        public static byte q1(int x)
        {
            return q1((byte)x);
        }

        public static byte q1(uint x)
        {
            return q1((byte)x);
        }

        public static byte q1(byte x)
        {
            byte[,] t =
            {
                {0x2, 0x8, 0xB, 0xD, 0xF, 0x7, 0x6, 0xE, 0x3, 0x1, 0x9, 0x4, 0x0, 0xA, 0xC, 0x5},
                {0x1, 0xE, 0x2, 0xB, 0x4, 0xC, 0x3, 0x7, 0x6, 0xD, 0xA, 0x5, 0xF, 0x9, 0x0, 0x8},
                {0x4, 0xC, 0x7, 0x5, 0x1, 0x6, 0x9, 0xA, 0x0, 0xE, 0xD, 0x8, 0x2, 0xB, 0x3, 0xF},
                {0xB, 0x9, 0x5, 0x1, 0xC, 0x3, 0xD, 0xE, 0x6, 0x4, 0x7, 0xF, 0x2, 0x0, 0x8, 0xA}
            };
            return q(x, t);
        }

        private static byte q(byte x, byte[,] t)
        {
            int a0 = x / 16;
            int b0 = x % 16;
            int a1 = a0 ^ b0;
            int b1 = a0 ^ Word4Bits.RotateRight((byte)b0) ^ 8 * a0 % 16;
            int a2 = t[0, a1];
            int b2 = t[1, b1];
            int a3 = a2 ^ b2;
            int b3 = a2 ^ Word4Bits.RotateRight((byte)b2) ^ 8 * a2 % 16;
            int a4 = t[2, a3];
            int b4 = t[3, b3];
            return Convert.ToByte(16 * b4 + a4);
        }
    }
}
