using System.IO;
using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class LZ78Tests
    {
        [TestMethod]
        public void Compress_Compresses_Compressed()
        {
            LZ78 lz78 = new LZ78();
            MemoryStream input = new MemoryStream(new byte[]{ 2, 5, 7, 1, 8, 2, 5, 2, 0, 2, 5, 1, 6, 8, 9, 2, 1 });
            MemoryStream output = new MemoryStream();
            byte[] expected = { 0, 8, 0, 80, 1, 192, 1, 0, 32, 16, 80, 64, 5, 128, 64, 6, 21, 9, 4, 4 };
            lz78.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress_Decompresses_Decompressed()
        {
            LZ78 lz78 = new LZ78();
            MemoryStream input = new MemoryStream(new byte[] { 0, 8, 0, 80, 1, 192, 1, 0, 32, 16, 80, 64, 5, 128, 64, 6, 21, 9, 4, 4 });
            MemoryStream output = new MemoryStream();
            byte[] expected = { 2, 5, 7, 1, 8, 2, 5, 2, 0, 2, 5, 1, 6, 8, 9, 2, 1 };
            lz78.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
