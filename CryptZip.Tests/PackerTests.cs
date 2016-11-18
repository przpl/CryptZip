using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class PackerTests
    {
        [TestMethod]
        public void IsPacked_PackedFile_Detected()
        {
            Assert.IsTrue(Packer.IsPackedFile("file.txtczp"));
        }

        [TestMethod]
        public void IsPacked_NotPackedFile_Detected()
        {
            Assert.IsFalse(Packer.IsPackedFile("file.txt"));
        }
    }
}
