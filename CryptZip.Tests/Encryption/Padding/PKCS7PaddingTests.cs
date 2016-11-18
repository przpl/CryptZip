using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption.Padding
{
    [TestClass]
    public class PKCS7PaddingTests
    {
        [TestMethod]
        public void Add_AddsPaddingToEmptyBlock_Added()
        {
            PKCS7Padding padding = new PKCS7Padding();
            byte[] bytes = new byte[8];
            padding.Add(bytes);
            CollectionAssert.AreEqual(new byte[] {8, 8, 8, 8, 8, 8, 8, 8}, bytes);
        }

        [TestMethod]
        public void Add_AddsPaddingToBlock_Added()
        {
            PKCS7Padding padding = new PKCS7Padding();
            byte[] bytes = new byte[8];
            padding.Add(bytes, 3);
            CollectionAssert.AreEqual(new byte[] { 0, 0, 0, 0, 4, 4, 4, 4 }, bytes);
        }

        [TestMethod]
        public void Remove_RemovesPaddingFromBlock_RemovedWholeBlock()
        {
            PKCS7Padding padding = new PKCS7Padding();
            byte[] bytes = {8, 8, 8, 8, 8, 8, 8, 8};
            CollectionAssert.AreEqual(new byte[0], padding.Remove(bytes));
        }

        [TestMethod]
        public void Remove_RemovesPaddingFromBlock_Removed()
        {
            PKCS7Padding padding = new PKCS7Padding();
            byte[] bytes = { 0, 0, 0, 0, 4, 4, 4, 4 };
            CollectionAssert.AreEqual(new byte[]{ 0, 0, 0, 0 }, padding.Remove(bytes));
        }
    }
}
