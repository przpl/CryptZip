using CryptZip.Encryption.TwofishAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption.TwofishAlgorithms
{
    [TestClass]
    public class TwofishMDSTests
    {
        [TestMethod]
        public void Multiply_MultipliesVector_Calculated()
        {
            uint[] value = {124, 241, 8, 13};
            uint excpected = 2212420926;
            Assert.AreEqual(excpected, new TwofishMDS().Multiply(value));
        }
    }
}
