using CryptZip.Compression;
using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CryptZip.Tests
{
    [TestClass]
    public class FileHeaderTests
    {
        [TestMethod]
        public void GetPacker_FullMode_Detected()
        {
            var header = new FileHeader();
            File.WriteAllBytes(@"debug.txt", new[] { Mode.Full, CompressorId.LZ77, CipherId.AES, EncryptorId.ECB });
            Packer packer = header.GetPacker(@"debug.txt", new byte[] { 1, 2, 3, 4, 5, 6 });

            Assert.IsInstanceOfType(packer, typeof(FullPacker));

            File.Delete(@"debug.txt");
        }

        [TestMethod]
        public void GetPacker_CompressionMode_Detected()
        {
            var header = new FileHeader();
            File.WriteAllBytes(@"debug.txt", new[] { Mode.Compress, CompressorId.LZ77 });
            Packer packer = header.GetPacker(@"debug.txt");

            Assert.IsInstanceOfType(packer, typeof(CompressionPacker));

            File.Delete(@"debug.txt");
        }

        [TestMethod]
        public void GetPacker_EncryptionMode_Detected()
        {
            var header = new FileHeader();
            File.WriteAllBytes(@"debug.txt", new[] { Mode.Encrypt, CipherId.AES, EncryptorId.ECB });
            Packer packer = header.GetPacker(@"debug.txt", new byte[] { 1, 2, 3, 4, 5, 6 });

            Assert.IsInstanceOfType(packer, typeof(EncryptionPacker));

            File.Delete(@"debug.txt");
        }

        [TestMethod]
        public void GetBytes_FullMode_Returned()
        {
            var header = new FileHeader();

            byte[] headerBytes = header.GetBytes(new LZ77(), new ECB(new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }), 
                new PKCS7Padding()));

            CollectionAssert.AreEqual(new [] { Mode.Full, CompressorId.LZ77, CipherId.AES, EncryptorId.ECB }, headerBytes);
        }

        [TestMethod]
        public void GetBytes_CompressionMode_Returned()
        {
            var header = new FileHeader();

            byte[] headerBytes = header.GetBytes(new LZ77(), null);

            CollectionAssert.AreEqual(new[] { Mode.Compress, CompressorId.LZ77 }, headerBytes);
        }

        [TestMethod]
        public void GetBytes_EncryptionMode_Returned()
        {
            var header = new FileHeader();

            byte[] headerBytes = header.GetBytes(null, new ECB(new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }),
                new PKCS7Padding()));

            CollectionAssert.AreEqual(new[] { Mode.Encrypt, CipherId.AES, EncryptorId.ECB }, headerBytes);
        }
    }
}
