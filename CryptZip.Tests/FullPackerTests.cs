using CryptZip.Compression;
using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace CryptZip.Tests
{
    [TestClass]
    public class FullPackerTests
    {
        private List<string> _events = new List<string>();

        [TestMethod]
        public void PackAsync_LZ77AESECB_Packed()
        {
            File.WriteAllBytes("packertest.txt", new byte[] { 1, 2, 3 });
            var packer = new FullPacker();
            packer.Compressor = new LZ77();
            var cipher = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            packer.Encryptor = new ECB(cipher, new PKCS7Padding());

            var task = packer.PackAsync("packertest.txt");
            task.Wait();

            byte[] result = File.ReadAllBytes("packertest.txtczp");
            CollectionAssert.AreEqual(new byte[] { 0x01, 0x01, 0x01, 0x01, 0x71, 0x6b, 0x15, 0x0b, 0xa3, 0x2d, 0xd6, 0x9f, 0xd9, 0x26, 0x06, 0x1c, 0x84, 0x93, 0x7b, 0x66 }, result);
        }

        [TestMethod]
        public void PackAsync_ProvidesEventMethods_EventsCalled()
        {
            File.WriteAllBytes("packertest.txt", new byte[] { 1, 2, 3 });
            var packer = new FullPacker();
            packer.Compressor = new LZ77();
            var cipher = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            packer.Encryptor = new ECB(cipher, new PKCS7Padding());
            packer.StatusChanged += OnStatusChanged;
            _events.Clear();

            var task = packer.PackAsync("packertest.txt");
            task.Wait();

            Assert.IsTrue(_events[0].StartsWith("Compressing"));
            Assert.IsTrue(_events[1].StartsWith("Encrypting"));
            Assert.IsTrue(_events[2].StartsWith("Finished"));
        }

        [TestMethod]
        public void UnpackAsync_LZ77AESECB_Unpacked()
        {
            File.WriteAllBytes("packertest.txtczp", new byte[] { 0x01, 0x01, 0x01, 0x01, 0x71, 0x6b, 0x15, 0x0b, 0xa3, 0x2d, 0xd6, 0x9f, 0xd9, 0x26, 0x06, 0x1c, 0x84, 0x93, 0x7b, 0x66 });
            var packer = new FullPacker();
            packer.Compressor = new LZ77();
            var cipher = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            packer.Encryptor = new ECB(cipher, new PKCS7Padding());

            var task = packer.UnpackAsync("packertest.txtczp");
            task.Wait();

            byte[] result = File.ReadAllBytes("packertest.txt");
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, result);
        }

        [TestMethod]
        public void UnpackAsync_ProvidesEventMethods_EventsCalled()
        {
            File.WriteAllBytes("packertest.txtczp", new byte[] { 0x01, 0x01, 0x01, 0x01, 0x71, 0x6b, 0x15, 0x0b, 0xa3, 0x2d, 0xd6, 0x9f, 0xd9, 0x26, 0x06, 0x1c, 0x84, 0x93, 0x7b, 0x66 });
            var packer = new FullPacker();
            packer.Compressor = new LZ77();
            var cipher = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            packer.Encryptor = new ECB(cipher, new PKCS7Padding());
            packer.StatusChanged += OnStatusChanged;
            _events.Clear();

            var task = packer.UnpackAsync("packertest.txtczp");
            task.Wait();

            Assert.IsTrue(_events[0].StartsWith("Decompressing"));
            Assert.IsTrue(_events[1].StartsWith("Decrypting"));
            Assert.IsTrue(_events[2].StartsWith("Finished"));
        }

        public void OnStatusChanged(object source, StatusEventArgs e)
        {
            _events.Add(e.Status);
        }
    }
}
