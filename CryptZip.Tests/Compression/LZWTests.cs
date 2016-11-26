using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class LZWTests
    {
        [TestMethod]
        public void Compress_Compresses_Compressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 1, 2, 5, 1, 7, 2, 6, 1, 2, 5, 2, 7, 1, 2, 1 });
            var output = new MemoryStream();
            byte[] expected = { 0, 32, 3, 0, 96, 2, 0, 128, 3, 0, 113, 1, 0, 96, 3, 0, 129, 1, 1 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 0, 32, 3, 0, 96, 2, 0, 128, 3, 0, 113, 1, 0, 96, 3, 0, 129, 1, 1 });
            var output = new MemoryStream();
            byte[] expected = { 1, 2, 5, 1, 7, 2, 6, 1, 2, 5, 2, 7, 1, 2, 1 };
            lzw.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Compress0_Compresses_Compressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 1, 2, 5, 7, 6, 4 });
            var output = new MemoryStream();
            byte[] expected = { 0, 32, 3, 0, 96, 8, 0, 112, 64 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress0_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 0, 32, 3, 0, 96, 8, 0, 112, 64 });
            var output = new MemoryStream();
            byte[] expected = { 1, 2, 5, 7, 6, 4 };
            lzw.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Compress1_Compresses_Compressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 1, 2, 1, 2, 1, 2, 1, 2 });
            var output = new MemoryStream();
            byte[] expected = { 0, 32, 3, 16, 17, 3, 2 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress1_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 0, 32, 3, 16, 17, 3, 2 });
            var output = new MemoryStream();
            byte[] expected = { 1, 2, 1, 2, 1, 2, 1, 2 };
            lzw.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Compress2_Compresses_Compressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 115, 105, 114, 32, 115, 105, 100, 32, 101, 97, 115, 116, 109, 97, 110, 32, 101, 97, 115, 105, 108, 121, 32, 116, 101, 97, 115, 101, 115, 32, 115, 101, 97, 32, 115, 105, 99, 107, 32, 115, 101, 97, 108, 115 });
            var output = new MemoryStream();
            byte[] expected = { 7, 64, 106, 7, 48, 33, 16, 16, 101, 2, 16, 102, 6, 32, 116, 7, 80, 110, 6, 32, 111, 16, 113, 9, 6, 160, 109, 7, 160, 33, 7, 81, 8, 7, 64, 102, 7, 65, 4, 16, 129, 4, 6, 160, 100, 6, 193, 26, 6, 32, 109, 115 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress2_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 7, 64, 106, 7, 48, 33, 16, 16, 101, 2, 16, 102, 6, 32, 116, 7, 80, 110, 6, 32, 111, 16, 113, 9, 6, 160, 109, 7, 160, 33, 7, 81, 8, 7, 64, 102, 7, 65, 4, 16, 129, 4, 6, 160, 100, 6, 193, 26, 6, 32, 109, 115 });
            var output = new MemoryStream();
            byte[] expected = { 115, 105, 114, 32, 115, 105, 100, 32, 101, 97, 115, 116, 109, 97, 110, 32, 101, 97, 115, 105, 108, 121, 32, 116, 101, 97, 115, 101, 115, 32, 115, 101, 97, 32, 115, 105, 99, 107, 32, 115, 101, 97, 108, 115 };
            lzw.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}