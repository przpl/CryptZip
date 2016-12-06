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
            byte[] expected = { 1, 0, 192, 192, 32, 64, 12, 15, 1, 3, 0, 193, 16, 16, 16 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 1, 0, 192, 192, 32, 64, 12, 15, 1, 3, 0, 193, 16, 16, 16 });
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
            byte[] expected = { 1, 0, 192, 192, 128, 56, 32 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress0_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 1, 0, 192, 192, 128, 56, 32 });
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
            byte[] expected = { 1, 0, 224, 48, 48, 32 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress1_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 1, 0, 224, 48, 48, 32 });
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
            byte[] expected = { 58, 26, 142, 98, 24, 9, 148, 66, 102, 49, 29, 14, 166, 227, 17, 190, 15, 9, 53, 27, 79, 66, 19, 172, 32, 232, 102, 58, 65, 33, 16, 67, 81, 144, 217, 26, 49, 27, 92, 192 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress2_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 58, 26, 142, 98, 24, 9, 148, 66, 102, 49, 29, 14, 166, 227, 17, 190, 15, 9, 53, 27, 79, 66, 19, 172, 32, 232, 102, 58, 65, 33, 16, 67, 81, 144, 217, 26, 49, 27, 92, 192 });
            var output = new MemoryStream();
            byte[] expected = { 115, 105, 114, 32, 115, 105, 100, 32, 101, 97, 115, 116, 109, 97, 110, 32, 101, 97, 115, 105, 108, 121, 32, 116, 101, 97, 115, 101, 115, 32, 115, 101, 97, 32, 115, 105, 99, 107, 32, 115, 101, 97, 108, 115 };
            lzw.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}