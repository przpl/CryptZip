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
            byte[] expected = { 12, 28, 88, 48, 224, 56, 186, 32, 34, 193, 199, 8, 128, 128, 128 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 12, 28, 88, 48, 224, 56, 186, 32, 34, 193, 199, 8, 128, 128, 128 });
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
            byte[] expected = { 12, 28, 88, 112, 46, 8 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress0_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 12, 28, 88, 112, 46, 8 });
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
            byte[] expected = { 12, 29, 16, 20, 64, 192, 128 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress1_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 12, 29, 16, 20, 64, 192, 128 });
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
            byte[] expected = { 55, 67, 106, 55, 50, 194, 136, 9, 178, 150, 19, 102, 54, 35, 116, 55, 83, 110, 54, 35, 111, 68, 29, 16, 147, 106, 54, 211, 122, 44, 38, 234, 136, 65, 186, 27, 49, 186, 34, 8, 136, 66, 32, 134, 212, 108, 134, 216, 136, 209, 177, 27, 107, 152 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress2_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 55, 67, 106, 55, 50, 194, 136, 9, 178, 150, 19, 102, 54, 35, 116, 55, 83, 110, 54, 35, 111, 68, 29, 16, 147, 106, 54, 211, 122, 44, 38, 234, 136, 65, 186, 27, 49, 186, 34, 8, 136, 66, 32, 134, 212, 108, 134, 216, 136, 209, 177, 27, 107, 152 });
            var output = new MemoryStream();
            byte[] expected = { 115, 105, 114, 32, 115, 105, 100, 32, 101, 97, 115, 116, 109, 97, 110, 32, 101, 97, 115, 105, 108, 121, 32, 116, 101, 97, 115, 101, 115, 32, 115, 101, 97, 32, 115, 105, 99, 107, 32, 115, 101, 97, 108, 115 };
            lzw.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}