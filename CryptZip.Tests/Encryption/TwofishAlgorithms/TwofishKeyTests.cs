using CryptZip.Encryption.TwofishAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption.TwofishAlgorithms
{
    [TestClass]
    public class TwofishKeyTests
    {
        [TestMethod]
        public void KSubkeys_CalculatesKSubkeys128Bits_Calculated()
        {
            TwofishKey key = new TwofishKey(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6});
            uint[] expectedK =
            {
                0x0431bb6d, 0x6d84c792, 0x6dddc75b, 0xe10047eb, 0x2901c607, 0xc55ae4ab, 0x10f00b95, 
                0xfc21abbd, 0x43dafe87, 0x5745ec3c, 0x389bd1fc, 0x78f3c2a6, 0x04d66028, 0x623b8afb, 
                0x8c2ebc34, 0x6feea47c, 0x4427e500, 0x45fbec26, 0xdfcc1585, 0x6d4d21f7, 0x8d8b6bb2, 
                0xd89cd1e3, 0x78ef2d79, 0x91add953, 0xf69da69c, 0xa63bd27a, 0x5580b95f, 0xf846ff42, 
                0xd5dc9d8c, 0x406ec0e4, 0x53a67f39, 0xbb87b7fb, 0xfa6068fb, 0xd1a581fc, 0x3324ddf8, 
                0x7be66a71, 0xbd5f53d2, 0x530fc988, 0x41d55552, 0x94d86ff5
            };
            CollectionAssert.AreEqual(expectedK, key.K);
        }

        [TestMethod]
        public void KSubkeys_CalculatesSBox128Bits_Calculated()
        {
            TwofishKey key = new TwofishKey(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 });
            uint[] expectedSbox = { 0xa655352f, 0xcfc9638d };
            CollectionAssert.AreEqual(expectedSbox, key.SBox);
        }

        [TestMethod]
        public void KSubkeys_CalculatesKSubkeys192Bits_Calculated()
        {
            TwofishKey key = new TwofishKey(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4 });
            uint[] expectedK =
            {
                0xe6260b09, 0x97a5a2a6, 0x9c4a1094, 0x38f564bc, 0xf31f4725, 0xa0d96cea, 0xf4a4d1c8,
                0xbaf8341e, 0x95825d2e, 0xae4777cf, 0x269203f4, 0xd5e6ea61, 0xadbf9c9c, 0x1a7f7bc7,
                0x74f22acd, 0xa9eacd72, 0x332359f4, 0x25b0c106, 0x75354171, 0x789609f4, 0x24154118,
                0x635b53ab, 0x7b7c4f9f, 0xb0e785e3, 0xc8dd9125, 0xaee84bd3, 0x042476ec, 0xb8430447,
                0xf4ea28b3, 0xddc09a82, 0xbc68271e, 0xb7361c6f, 0xe8c5de43, 0x9ab4f47b, 0x61588068,
                0x9126b24e, 0x65a005cd, 0x18a49053, 0xdcc09b45, 0x0f143cf0,
            };
            CollectionAssert.AreEqual(expectedK, key.K);
        }

        [TestMethod]
        public void KSubkeys_CalculatesSBox192Bits_Calculated()
        {
            TwofishKey key = new TwofishKey(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4 });
            uint[] expectedSbox = { 0x1ff54e5c, 0xa655352f, 0xcfc9638d };
            CollectionAssert.AreEqual(expectedSbox, key.SBox);
        }

        [TestMethod]
        public void KSubkeys_CalculatesKSubkeys256Bits_Calculated()
        {
            TwofishKey key = new TwofishKey(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 });
            uint[] expectedK =
            {
                0xd3879761, 0x5bbafa4a, 0x6d51b4c9, 0x858f8e37, 0x0caba720, 0x6b2fe128, 0x5059139b,
                0xb0533e88, 0xc8842e8f, 0xcb095e3f, 0xe1407ba4, 0x007bb74a, 0x1ad25e65, 0xcbcbdf25,
                0x945715a9, 0x15d3ee94, 0x284c952c, 0x16f2de73, 0x48726c2a, 0x5c9fa031, 0x356e150e,
                0x456470e4, 0x7ba529f0, 0xb25d9d17, 0x2e9d7d99, 0x644df933, 0x68bc3ebe, 0x37a6f77e,
                0x57fbd1ba, 0x1bb72773, 0xa31ffd44, 0xfc4c1fac, 0x65c34db9, 0x98893af1, 0xd4eb3c5f,
                0x3a75f50c, 0xadf070c6, 0xf44e902d, 0xc04c6ce4, 0x279fbd2f,
            };
            CollectionAssert.AreEqual(expectedK, key.K);
        }

        [TestMethod]
        public void KSubkeys_CalculatesSBox256Bits_Calculated()
        {
            TwofishKey key = new TwofishKey(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 });
            uint[] expectedSbox = { 0x8f747917, 0x1ff54e5c, 0xa655352f, 0xcfc9638d };
            CollectionAssert.AreEqual(expectedSbox, key.SBox);
        }
    }
}