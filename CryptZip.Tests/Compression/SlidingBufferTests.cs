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
            var buffer = new SlidingBuffer(3);

            buffer.Add(1);

            Assert.AreEqual(1, buffer[1]);
        }

        [TestMethod]
        public void Add_AddsFourBytes_Added()
        {
            var buffer = new SlidingBuffer(3);

            for (int i = 1; i <= 5; i++)
                buffer.Add((byte)i);

            Assert.AreEqual(5, buffer[1]);
            Assert.AreEqual(4, buffer[2]);
            Assert.AreEqual(3, buffer[3]);
        }
    }
}