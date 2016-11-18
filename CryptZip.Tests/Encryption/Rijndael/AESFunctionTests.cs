using CryptZip.Encryption.Rijndael;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption.Rijndael
{
    [TestClass]
    public class AESFunctionTests
    {
        [TestMethod]
        public void SubButes_GoesOverSubBytesStep_Replaced()
        {
            byte[][] array =
            {
                new byte[] {0x19, 0xa0, 0x9a, 0xe9},
                new byte[] {0x3d, 0xf4, 0xc6, 0xf8},
                new byte[] {0xe3, 0xe2, 0x8d, 0x48},
                new byte[] {0xbe, 0x2b, 0x2a, 0x08}
            };

            byte[][] expected =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0x27, 0xbf, 0xb4, 0x41},
                new byte[] {0x11, 0x98, 0x5d, 0x52},
                new byte[] {0xae, 0xf1, 0xe5, 0x30}
            };

            byte[][] result = AESFunction.SubBytes(array);

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.AreEqual(expected[j][i], result[j][i]);
        }

        [TestMethod]
        public void ReverseSubButes_GoesOverSubBytesStep_Replaced()
        {
            byte[][] array =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0x27, 0xbf, 0xb4, 0x41},
                new byte[] {0x11, 0x98, 0x5d, 0x52},
                new byte[] {0xae, 0xf1, 0xe5, 0x30}
            };

            byte[][] expected =
            {
                new byte[] {0x19, 0xa0, 0x9a, 0xe9},
                new byte[] {0x3d, 0xf4, 0xc6, 0xf8},
                new byte[] {0xe3, 0xe2, 0x8d, 0x48},
                new byte[] {0xbe, 0x2b, 0x2a, 0x08}
            };

            byte[][] result = AESFunction.ReverseSubBytes(array);

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.AreEqual(expected[j][i], result[j][i]);
        }

        [TestMethod]
        public void ShiftRows_ShiftsRows_Shifted()
        {
            byte[][] array =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0x27, 0xbf, 0xb4, 0x41},
                new byte[] {0x11, 0x98, 0x5d, 0x52},
                new byte[] {0xae, 0xf1, 0xe5, 0x30}
            };

            byte[][] expected =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0xbf, 0xb4, 0x41, 0x27},
                new byte[] {0x5d, 0x52, 0x11, 0x98},
                new byte[] {0x30, 0xae, 0xf1, 0xe5 }
            };

            byte[][] result = AESFunction.ShiftRows(array);

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.AreEqual(expected[j][i], result[j][i]);
        }

        [TestMethod]
        public void ReverseShiftRows_ShiftsRows_Shifted()
        {
            byte[][] array =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0xbf, 0xb4, 0x41, 0x27},
                new byte[] {0x5d, 0x52, 0x11, 0x98},
                new byte[] {0x30, 0xae, 0xf1, 0xe5 }
            };

            byte[][] expected =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0x27, 0xbf, 0xb4, 0x41},
                new byte[] {0x11, 0x98, 0x5d, 0x52},
                new byte[] {0xae, 0xf1, 0xe5, 0x30}
            };

            byte[][] result = AESFunction.ReverseShiftRows(array);

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.AreEqual(expected[j][i], result[j][i]);
        }

        [TestMethod]
        public void MixColumns_GoesOverMixColumnsStep_Executed()
        {
            byte[][] array =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0xbf, 0xb4, 0x41, 0x27},
                new byte[] {0x5d, 0x52, 0x11, 0x98},
                new byte[] {0x30, 0xae, 0xf1, 0xe5}
            };

            byte[][] expected =
            {
                new byte[] {0x04, 0xe0, 0x48, 0x28},
                new byte[] {0x66, 0xcb, 0xf8, 0x06},
                new byte[] {0x81, 0x19, 0xd3, 0x26},
                new byte[] {0xe5, 0x9a, 0x7a, 0x4c}
            };

            byte[][] result = AESFunction.MixColumns(array);

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.AreEqual(expected[j][i], result[j][i]);
        }

        [TestMethod]
        public void ReverseMixColumns_GoesOverMixColumnsStep_Executed()
        {
            byte[][] array =
            {
                new byte[] {0x04, 0xe0, 0x48, 0x28},
                new byte[] {0x66, 0xcb, 0xf8, 0x06},
                new byte[] {0x81, 0x19, 0xd3, 0x26},
                new byte[] {0xe5, 0x9a, 0x7a, 0x4c}
            };

            byte[][] expected =
            {
                new byte[] {0xd4, 0xe0, 0xb8, 0x1e},
                new byte[] {0xbf, 0xb4, 0x41, 0x27},
                new byte[] {0x5d, 0x52, 0x11, 0x98},
                new byte[] {0x30, 0xae, 0xf1, 0xe5}
            };

            byte[][] result = AESFunction.ReverseMixColumns(array);

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.AreEqual(expected[j][i], result[j][i]);
        }

        [TestMethod]
        public void AddRoundKey_GoesOverAddRoundKeyStep_Executed()
        {
            byte[][] array =
            {
                new byte[] {0x04, 0xe0, 0x48, 0x28},
                new byte[] {0x66, 0xcb, 0xf8, 0x06},
                new byte[] {0x81, 0x19, 0xd3, 0x26},
                new byte[] {0xe5, 0x9a, 0x7a, 0x4c}
            };

            byte[][] roundKey =
            {
                new byte[] {0xa0, 0x88, 0x23, 0x2a},
                new byte[] {0xfa, 0x54, 0xa3, 0x6c},
                new byte[] {0xfe, 0x2c, 0x39, 0x76},
                new byte[] {0x17, 0xb1, 0x39, 0x05}
            };

            byte[][] expected =
            {
                new byte[] {0xa4, 0x68, 0x6b, 0x02},
                new byte[] {0x9c, 0x9f, 0x5b, 0x6a},
                new byte[] {0x7f, 0x35, 0xea, 0x50},
                new byte[] {0xf2, 0x2b, 0x43, 0x49}
            };

            byte[][] result = AESFunction.AddRoundKey(array, roundKey);

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                    Assert.AreEqual(expected[j][i], result[j][i]);
        }
    }
}
