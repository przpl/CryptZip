using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void GetBytes_String_Converted()
        {
            CollectionAssert.AreEqual(new byte[] {116,101,120,116}, "text".ToBytes());
        }

        [TestMethod]
        public void GetBytes_EmptyString_Converted()
        {
            CollectionAssert.AreEqual(new byte[0], "".ToBytes());
        }
    }
}
