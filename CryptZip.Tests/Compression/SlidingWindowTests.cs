using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class SlidingWindowTests
    {
        [TestMethod]
        public void Constructor_SixBytes_Initialized()
        {
            MemoryStream stream = new MemoryStream(new byte[] {0, 1, 2, 3, 4, 5});
            SlidingWindow window = new SlidingWindow(stream, 4, 3);
            var bytes = window.Bytes;
            CollectionAssert.AreEqual(new [] {0,1,2,0,0,0,0}, bytes);
        }

        [TestMethod]
        public void Slide_SlidesOneTime_Slided()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5 });
            SlidingWindow window = new SlidingWindow(stream, 4, 3);
            window.Slide(1);
            var bytes = window.Bytes;
            CollectionAssert.AreEqual(new [] { 0, 1, 2, 3, 0, 0, 0 }, bytes);
        }

        [TestMethod]
        public void Slide_SlidesThreeTimes_Slided()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5 });
            SlidingWindow window = new SlidingWindow(stream, 4, 3);
            window.Slide(3);
            var bytes = window.Bytes;
            CollectionAssert.AreEqual(new [] { 0, 1, 2, 3, 4, 5, 0 }, bytes);
        }

        [TestMethod]
        public void Slide_SlidesFiveTimes_Slided()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 });
            SlidingWindow window = new SlidingWindow(stream, 4, 3);
            window.Slide(5);
            var bytes = window.Bytes;
            CollectionAssert.AreEqual(new [] { 7, 1, 2, 3, 4, 5, 6 }, bytes);
        }

        [TestMethod]
        public void Slide_SlidesSixTimes_Slided()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5 });
            SlidingWindow window = new SlidingWindow(stream, 4, 3);
            window.Slide(6);
            var bytes = window.Bytes;
            CollectionAssert.AreEqual(new [] { 0, 1, 2, 3, 4, 5, 0 }, bytes);
        }

        [TestMethod]
        public void NextToken_AtBegining_Found()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 1, 2, 5, 4, 6, 7, 1, 9, 10, 7, 11, 4 });
            SlidingWindow window = new SlidingWindow(stream, 32, 16);
            Token token = window.NextToken();
            Assert.AreEqual(0, token.Offset);
            Assert.AreEqual(0, token.Length);
            Assert.AreEqual(1, token.Byte);
        }

        [TestMethod]
        public void NextToken_SlidesFiveeTimes_Found()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 1, 2, 5, 4, 6, 7, 1, 9, 10, 7, 11, 4 });
            SlidingWindow window = new SlidingWindow(stream, 32, 16);
            window.NextToken();
            window.NextToken();
            window.NextToken();
            window.NextToken();
            Token token = window.NextToken();
            Assert.AreEqual(4, token.Offset);
            Assert.AreEqual(2, token.Length);
            Assert.AreEqual(5, token.Byte);
            token = window.NextToken();
            Assert.AreEqual(4, token.Offset);
            Assert.AreEqual(1, token.Length);
            Assert.AreEqual(6, token.Byte);
        }

        [TestMethod]
        public void NextToken_34Token()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 1, 6, 7, 8, 1, 9, 4, 5, 1, 6, 10, 2, 11, 4, 12, 13, 14, 15, 6, 4, 1, 2, 3, 1, 2, 3, 1, 4, 10, 9, 4, 16, 10, 6, 4, 12, 1, 13, 17, 5, 9, });
            SlidingWindow window = new SlidingWindow(stream, 28, 12);
            window.Slide(28);
            Token token = window.NextToken();
            Assert.AreEqual(3, token.Offset);
            Assert.AreEqual(4, token.Length);
            Assert.AreEqual(4, token.Byte);
        }

        [TestMethod]
        public void NextToken_19Token()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 1, 6, 7, 8, 1, 9, 4, 5, 1, 6, 10, 2, 11, 4, 11, 5, 2, 2, 6, 4, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 13 });
            SlidingWindow window = new SlidingWindow(stream, 26, 10);
            window.Slide(26);
            Token token = window.NextToken();
            Assert.AreEqual(1, token.Offset);
            Assert.AreEqual(9, token.Length);
            Assert.AreEqual(12, token.Byte);
        }

        [TestMethod]
        public void NextToken_LastToken_00Token()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5 });
            SlidingWindow window = new SlidingWindow(stream, 3, 2);
            window.Slide(4);
            Token token = window.NextToken();
            Assert.AreEqual(0, token.Offset);
            Assert.AreEqual(0, token.Length);
            Assert.AreEqual(5, token.Byte);
        }

        [TestMethod]
        public void NextToken_ReadsAllTokens_Correct()
        {
            var stream = new MemoryStream(new byte[] { 1, 2, 3, 12, 4, 5, 6, 12, 7, 4, 8, 9, 10, 4, 11, 12, 7, 4, 8, 16, 5, 19, 12, 13, 2, 14, 15, 8, 12, 4, 5, 6, 4, 5, 6, 4, 12, 16, 11, 12, 17, 16, 8, 12, 13, 4, 2, 18, 7, 11, 3 });
            var window = new SlidingWindow(stream, 28, 12);
            for (int i = 0; i < 22; i++)
                window.NextToken();

            var token = window.NextToken();
            Assert.AreEqual(22, token.Offset);
            Assert.AreEqual(1, token.Length);
            Assert.AreEqual(8, token.Byte);

            token = window.NextToken();
            Assert.AreEqual(21, token.Offset);
            Assert.AreEqual(2, token.Length);
            Assert.AreEqual(4, token.Byte);

            token = window.NextToken();
            Assert.AreEqual(22, token.Offset);
            Assert.AreEqual(1, token.Length);
            Assert.AreEqual(18, token.Byte);

            token = window.NextToken();
            Assert.AreEqual(0, token.Offset);
            Assert.AreEqual(0, token.Length);
            Assert.AreEqual(7, token.Byte);

            token = window.NextToken();
            Assert.AreEqual(11, token.Offset);
            Assert.AreEqual(1, token.Length);
            Assert.AreEqual(3, token.Byte);
        }

        [TestMethod]
        public void LookAheadEmpty_Nothing_NotEmpty()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5 });
            SlidingWindow window = new SlidingWindow(stream, 3, 2);
            Assert.IsFalse(window.LookAheadEmpty);
        }

        [TestMethod]
        public void LookAheadEmpty_SlidesFourBytes_NotEmpty()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5 });
            SlidingWindow window = new SlidingWindow(stream, 3, 2);
            window.Slide(4);
            Assert.IsFalse(window.LookAheadEmpty);
        }

        [TestMethod]
        public void LookAheadEmpty_SlidesAllBytes_Empty()
        {
            MemoryStream stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            SlidingWindow window = new SlidingWindow(stream, 3, 2);
            window.Slide(8);
            Assert.IsTrue(window.LookAheadEmpty);
        }
    }
}