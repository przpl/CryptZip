using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CryptZip.Tests.Compression
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
        public void Compress2_Compresses_Compressed()
        {
            var lz77 = new LZ77();
            var input = new MemoryStream(new byte[] { 5,2,5,8,1,10,2,4,6,9,2,3,5,8,1,1,2,1,1,1,1,8,0,1,12 });
            var output = new MemoryStream();
            byte[] excpected = { 16, 9, 0, 0, 0, 2, 128, 0, 0, 0, 128, 0, 128, 33, 0, 0, 0, 0, 16, 0, 0, 0, 80, 0, 40, 4, 16, 0, 0, 0, 12, 0, 0, 0, 9, 0, 9, 0, 129, 128, 5, 0, 192, 64, 3, 192, 32, 32, 0, 32, 48, 128, 0, 0, 0, 0, 0, 152, 4, 48 };
            lz77.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(excpected, result);
        }

        [TestMethod]
        public void Compress3_Compresses_Compressed()
        {
            var lz77 = new LZ77();
            var input = new MemoryStream(new byte[] { 1, 2, 3 });
            var output = new MemoryStream();
            byte[] excpected = { 16, 9, 0, 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 96 };
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

        [TestMethod]
        public void Decompress2_Decompresses_Decompressed()
        {
            var lz77 = new LZ77();
            var input = new MemoryStream(new byte[] { 16, 9, 0, 0, 0, 2, 128, 0, 0, 0, 128, 0, 128, 33, 0, 0, 0, 0, 16, 0, 0, 0, 80, 0, 40, 4, 16, 0, 0, 0, 12, 0, 0, 0, 9, 0, 9, 0, 129, 128, 5, 0, 192, 64, 3, 192, 32, 32, 0, 32, 48, 128, 0, 0, 0, 0, 0, 152, 4, 48 });
            var output = new MemoryStream();
            byte[] excpected = { 5, 2, 5, 8, 1, 10, 2, 4, 6, 9, 2, 3, 5, 8, 1, 1, 2, 1, 1, 1, 1, 8, 0, 1, 12 };
            lz77.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(excpected, result);
        }

        [TestMethod]
        public void Decompress3_Decompresses_Decompressed()
        {
            var lz77 = new LZ77();
            var input = new MemoryStream(new byte[] { 16, 9, 0, 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 96 });
            var output = new MemoryStream();
            byte[] excpected = { 1, 2, 3 };
            lz77.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(excpected, result);
        }
    }
}