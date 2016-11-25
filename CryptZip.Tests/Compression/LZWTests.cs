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
            var input = new MemoryStream(new byte[] { 1,2,1,2,1,2,1,2 });
            var output = new MemoryStream();
            byte[] expected = { 1,2,3,5 };
            lzw.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress_Decompresses_Decompressed()
        {
            var lzw = new LZW();
            var input = new MemoryStream(new byte[] { 1, 2, 3, 5 });
            var output = new MemoryStream();
            byte[] expected = { 1, 2, 1, 2, 1, 2, 1, 2 };
            lzw.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        //[TestMethod]
        //public void Compress_Compresses_Compressed()
        //{
        //    var lzw = new LZW();
        //    var input = new MemoryStream(new byte[] { 1, 2, 3, 3, 4, 5, 1, 2, 3, 3, 4, 5, 1, 3, 4, 5, 1, 3, 4, 5, 1, 3, 4, 5 });
        //    var output = new MemoryStream();
        //    byte[] expected = { 1, 2, 3, 3, 4, 5, 6, 8, 10, 1, 9, 11, 16, 15, 10 };
        //    lzw.Compress(input, output);
        //    byte[] result = output.ToArray();
        //    CollectionAssert.AreEqual(expected, result);
        //}

        //[TestMethod]
        //public void Decompress_Decompresses_Decompressed()
        //{
        //    var lzw = new LZW();
        //    var input = new MemoryStream(new byte[] { 1, 2, 3, 3, 4, 5, 6, 8, 10, 1, 9, 11, 16, 15, 10 });
        //    var output = new MemoryStream();
        //    byte[] expected = { 1, 2, 3, 3, 4, 5, 1, 2, 3, 3, 4, 5, 1, 3, 4, 5, 1, 3, 4, 5, 1, 3, 4, 5 };
        //    lzw.Decompress(input, output);
        //    byte[] result = output.ToArray();
        //    CollectionAssert.AreEqual(expected, result);
        //}
    }
}
