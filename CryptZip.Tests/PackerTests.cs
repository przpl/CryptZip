using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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

        [TestMethod]
        public void IsEncrypted_FullMode_Detected()
        {
            File.WriteAllBytes(@"encrypted.txt", new []{Mode.Full});

            Assert.IsTrue(Packer.IsEncrypted(@"encrypted.txt"));

            File.Delete(@"encrypted.txt");
        }

        [TestMethod]
        public void IsEncrypted_EncryptionMode_Detected()
        {
            File.WriteAllBytes(@"encrypted.txt", new[] { Mode.Encrypt });

            Assert.IsTrue(Packer.IsEncrypted(@"encrypted.txt"));

            File.Delete(@"encrypted.txt");
        }

        [TestMethod]
        public void IsEncrypted_CompressionMode_Detected()
        {
            File.WriteAllBytes(@"encrypted.txt", new[] { Mode.Compress });

            Assert.IsFalse(Packer.IsEncrypted(@"encrypted.txt"));

            File.Delete(@"encrypted.txt");
        }
    }
}