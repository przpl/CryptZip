using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class KeyExtenderTests
    {
        [TestMethod]
        public void Extend_ProvidesCompleteKey_Extended()
        {
            CollectionAssert.AreEqual(new byte[16], KeyExtender.Extend(new byte[16], MockRepository.GenerateMock<IPadding>()));
            CollectionAssert.AreEqual(new byte[24], KeyExtender.Extend(new byte[24], MockRepository.GenerateMock<IPadding>()));
            CollectionAssert.AreEqual(new byte[32], KeyExtender.Extend(new byte[32], MockRepository.GenerateMock<IPadding>()));
        }

        [TestMethod]
        public void Extend_Provides10ByteKey_Extended()
        {
            byte[] key = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 6, 6, 6, 6, 6, 6 }, KeyExtender.Extend(key, new PKCS7Padding()));
        }
    }
}
