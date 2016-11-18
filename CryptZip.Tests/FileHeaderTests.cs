using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class FileHeaderTests
    {
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void GetBytes_NoEncryptionMode_Returned()
        //{
        //    new FileHeader().GetBytes(CompressionAlg.LZ77, EncryptionAlg.AES);
        //}

        //[TestMethod]
        //public void GetBytes_FullMode_Returned()
        //{
        //    CollectionAssert.AreEqual(new byte[] {0, (byte)CompressionAlg.LZ77, (byte)EncryptionAlg.AES, (byte)EncrypctionMode.ECB}, 
        //        new FileHeader().GetBytes(CompressionAlg.LZ77, EncryptionAlg.AES, EncrypctionMode.ECB));
        //}

        //[TestMethod]
        //public void GetBytes_CompressionOnly_Returned()
        //{
        //    CollectionAssert.AreEqual(new byte[] { 1, (byte)CompressionAlg.LZ77 },
        //        new FileHeader().GetBytes(CompressionAlg.LZ77, EncryptionAlg.None));
        //}

        //[TestMethod]
        //public void GetBytes_EncryptionOnly_Returned()
        //{
        //    CollectionAssert.AreEqual(new byte[] { 2, (byte)EncryptionAlg.AES, (byte)EncrypctionMode.ECB },
        //        new FileHeader().GetBytes(CompressionAlg.None, EncryptionAlg.AES, EncrypctionMode.ECB));
        //}

        //[TestMethod]
        //public void Constructor_FullMode_Returned()
        //{
        //    MemoryStream stream = new MemoryStream(new byte[] {0, 0, 0, 0});
        //    FileHeader header = new FileHeader(stream);
        //    Assert.AreEqual(CompressionAlg.LZ77, header.CompressionAlg);
        //    Assert.AreEqual(EncryptionAlg.AES, header.EncryptionAlg);
        //    Assert.AreEqual(EncrypctionMode.ECB, header.EncrypctionMode);
        //}

        //[TestMethod]
        //public void Constructor_CompressionOnly_Returned()
        //{
        //    MemoryStream stream = new MemoryStream(new byte[] { 1, 0 });
        //    FileHeader header = new FileHeader(stream);
        //    Assert.AreEqual(CompressionAlg.LZ77, header.CompressionAlg);
        //    Assert.AreEqual(EncryptionAlg.None, header.EncryptionAlg);
        //    Assert.AreEqual(EncrypctionMode.None, header.EncrypctionMode);
        //}

        //[TestMethod]
        //public void Constructor_EncryptionOnly_Returned()
        //{
        //    MemoryStream stream = new MemoryStream(new byte[] { 2, 0, 0 });
        //    FileHeader header = new FileHeader(stream);
        //    Assert.AreEqual(CompressionAlg.None, header.CompressionAlg);
        //    Assert.AreEqual(EncryptionAlg.AES, header.EncryptionAlg);
        //    Assert.AreEqual(EncrypctionMode.ECB, header.EncrypctionMode);
        //}
    }
}
