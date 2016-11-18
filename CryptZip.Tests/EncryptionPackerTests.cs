using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace CryptZip.Tests
{
    [TestClass]
    public class EncryptionPackerTests
    {
        private List<string> _events = new List<string>();

        [TestMethod]
        public void PackAsync_LZ77AESECB_Packed()
        {
            File.WriteAllBytes("packertest.txt", new byte[] { 1, 2, 3 });
            var packer = new EncryptionPacker();
            var cipher = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            packer.Encryptor = new ECB(cipher, new PKCS7Padding());

            var task = packer.PackAsync("packertest.txt");
            task.Wait();

            byte[] result = File.ReadAllBytes("packertest.txtczp");
            CollectionAssert.AreEqual(new byte[] { 0x03, 0x01, 0x01, 0x1f, 0x05, 0xb8, 0xf2, 0xd8, 0xb2, 0x5c, 0x68, 0xc1, 0x4e, 0xfd, 0x97, 0x59, 0x8f, 0xa7, 0xdd }, result);
        }

        [TestMethod]
        public void PackAsync_ProvidesEventMethods_EventsCalled()
        {
            File.WriteAllBytes("packertest.txt", new byte[] { 1, 2, 3 });
            var packer = new EncryptionPacker();
            var cipher = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            packer.Encryptor = new ECB(cipher, new PKCS7Padding());
            packer.StatusChanged += OnStatusChanged;
            _events.Clear();

            var task = packer.PackAsync("packertest.txt");
            task.Wait();

            Assert.IsTrue(_events[0].StartsWith("Encrypting"));
            Assert.IsTrue(_events[1].StartsWith("Finished"));
        }

        [TestMethod]
        public void UnpackAsync_LZ77AESECB_Unpacked()
        {
            File.WriteAllBytes("packertest.txtczp", new byte[] { 0x03, 0x01, 0x01, 0x1f, 0x05, 0xb8, 0xf2, 0xd8, 0xb2, 0x5c, 0x68, 0xc1, 0x4e, 0xfd, 0x97, 0x59, 0x8f, 0xa7, 0xdd });
            var packer = new EncryptionPacker();
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
            File.WriteAllBytes("packertest.txtczp", new byte[] { 0x03, 0x01, 0x01, 0x1f, 0x05, 0xb8, 0xf2, 0xd8, 0xb2, 0x5c, 0x68, 0xc1, 0x4e, 0xfd, 0x97, 0x59, 0x8f, 0xa7, 0xdd });
            var packer = new EncryptionPacker();
            var cipher = new AES(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            packer.Encryptor = new ECB(cipher, new PKCS7Padding());
            packer.StatusChanged += OnStatusChanged;
            _events.Clear();

            var task = packer.UnpackAsync("packertest.txtczp");
            task.Wait();

            Assert.IsTrue(_events[0].StartsWith("Decrypting"));
            Assert.IsTrue(_events[1].StartsWith("Finished"));
        }

        public void OnStatusChanged(object source, StatusEventArgs e)
        {
            _events.Add(e.Status);
        }

    }
}