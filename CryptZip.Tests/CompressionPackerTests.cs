using System.Collections.Generic;
using System.IO;
using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class CompressionPackerTests
    {
        private List<string> _events = new List<string>();

        [TestMethod]
        public void PackAsync_LZ77AESECB_Packed()
        {
            File.WriteAllBytes("packertest.txt", new byte[] { 1, 2, 3 });
            var packer = new CompressionPacker();
            packer.Compressor = new LZ77();
            
            var task = packer.PackAsync("packertest.txt");
            task.Wait();

            byte[] result = File.ReadAllBytes("packertest.txtczp");
            CollectionAssert.AreEqual(new byte[] { 0x02, 0x01, 0x10, 0x09, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x60 }, result);
        }

        [TestMethod]
        public void PackAsync_ProvidesEventMethods_EventsCalled()
        {
            File.WriteAllBytes("packertest.txt", new byte[] { 1, 2, 3 });
            var packer = new CompressionPacker();
            packer.Compressor = new LZ77();
            packer.StatusChanged += OnStatusChanged;
            _events.Clear();

            var task = packer.PackAsync("packertest.txt");
            task.Wait();

            Assert.IsTrue(_events[0].StartsWith("Compressing"));
            Assert.IsTrue(_events[1].StartsWith("Finished"));
        }

        [TestMethod]
        public void UnpackAsync_LZ77AESECB_Unpacked()
        {
            File.WriteAllBytes("packertest.txtczp", new byte[] { 0x02, 0x01, 0x10, 0x09, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x60 });
            var packer = new CompressionPacker();
            packer.Compressor = new LZ77();

            var task = packer.UnpackAsync("packertest.txtczp");
            task.Wait();

            byte[] result = File.ReadAllBytes("packertest.txt");
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, result);
        }

        [TestMethod]
        public void UnpackAsync_ProvidesEventMethods_EventsCalled()
        {
            File.WriteAllBytes("packertest.txtczp", new byte[] { 0x02, 0x01, 0x10, 0x09, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x60 });
            var packer = new CompressionPacker();
            packer.Compressor = new LZ77();
            packer.StatusChanged += OnStatusChanged;
            _events.Clear();

            var task = packer.UnpackAsync("packertest.txtczp");
            task.Wait();

            Assert.IsTrue(_events[0].StartsWith("Decompressing"));
            Assert.IsTrue(_events[1].StartsWith("Finished"));
        }

        public void OnStatusChanged(object source, StatusEventArgs e)
        {
            _events.Add(e.Status);
        }

    }
}
