using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class PKCS7PaddingTests
    {
        [TestMethod]
        public void Add_ProvidesEmptyBlock_Added()
        {
            byte[] bytes = new byte[16];
            PKCS7Padding padding = new PKCS7Padding();
            padding.Add(bytes);
            CollectionAssert.AreEqual(new byte[] {16, 16, 16, 16, 16, 16, 16, 16 , 16, 16, 16, 16 , 16, 16, 16, 16 }, bytes);
        }

        [TestMethod]
        public void Add_ProvidesBlock_Added()
        {
            byte[] bytes = new byte[16];
            bytes[0] = 1;
            bytes[1] = 2;
            bytes[2] = 3;
            bytes[3] = 4;
            bytes[4] = 5;
            PKCS7Padding padding = new PKCS7Padding();
            padding.Add(bytes, 4);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11 }, bytes);
        }

        [TestMethod]
        public void Add_ProvidesFullBlock_NotAdded()
        {
            byte[] bytes = new byte[16];
            PKCS7Padding padding = new PKCS7Padding();
            padding.Add(bytes, 16);
            CollectionAssert.AreEqual(new byte[16], bytes);
        }
    }
}
