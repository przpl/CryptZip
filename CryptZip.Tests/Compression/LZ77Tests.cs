using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CryptZip.Tests
{
    [TestClass]
    public class LZ77Tests
    {
        [TestMethod]
        public void Compress_Compresses_Compressed()
        {
            var lz77 = new LZ77();
            var input = new MemoryStream(new byte[] { 2, 5, 7, 1, 8, 2, 5, 2, 0, 2, 5, 1, 6, 8, 9, 2, 1 });
            var output = new MemoryStream();
            byte[] excpected = { 16, 9, 0, 0, 0, 1, 0, 0, 0, 1, 64, 0, 0, 0, 224, 0, 0, 0, 16, 0, 0, 0, 64, 0, 40, 8, 8, 0, 0, 0, 0, 0, 18, 2, 1, 0, 0, 0, 3, 0, 4, 128, 66, 64, 3, 192, 32, 32 };
            lz77.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(excpected, result);
        }

        [TestMethod]
        public void Decompress_Decompresses_Decompressed()
        {
            var lz77 = new LZ77();
            var input = new MemoryStream(new byte[] { 16, 9, 0, 0, 0, 1, 0, 0, 0, 1, 64, 0, 0, 0, 224, 0, 0, 0, 16, 0, 0, 0, 64, 0, 40, 8, 8, 0, 0, 0, 0, 0, 18, 2, 1, 0, 0, 0, 3, 0, 4, 128, 66, 64, 3, 192, 32, 32 });
            var output = new MemoryStream();
            byte[] excpected = { 2, 5, 7, 1, 8, 2, 5, 2, 0, 2, 5, 1, 6, 8, 9, 2, 1 };
            lz77.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(excpected, result);
        }
    }
}
