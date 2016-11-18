using CryptZip.Encryption.SerpentAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption.SerpentAlgorithms
{
    [TestClass]
    public class LinearTransformatorTests
    {
        [TestMethod]
        public void Transform_TransformsValues_Transformed()
        {
            uint[] values = { 0x014aba55, 0x0020c5e4, 0x00000b83, 0x0051e7d5 };
            uint[] expected = { 0x78722bdf, 0xaed473aa, 0xf15a1932, 0x025d42dd };
            CollectionAssert.AreEqual(expected, LinearTransformator.Transform(values));
        }

        [TestMethod]
        public void Inverse_InversessValues_Inversed()
        {
            uint[] values = { 0x78722bdf, 0xaed473aa, 0xf15a1932, 0x025d42dd };
            uint[] expected = { 0x014aba55, 0x0020c5e4, 0x00000b83, 0x0051e7d5 };
            uint[] result = LinearTransformator.Inverse(values);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
