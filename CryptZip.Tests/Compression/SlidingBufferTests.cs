using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class SlidingBufferTests
    {
        [TestMethod]
        public void Add_AddsOneByte_Added()
        {
            SlidingBuffer buffer = new SlidingBuffer(3);
            buffer.Add(1);
            CollectionAssert.AreEqual(new byte[] {0,0,1}, buffer.Bytes);
        }

        [TestMethod]
        public void Add_AddsFourBytes_Added()
        {
            SlidingBuffer buffer = new SlidingBuffer(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);
            CollectionAssert.AreEqual(new byte[] { 2, 3, 4 }, buffer.Bytes);
        }
    }
}
