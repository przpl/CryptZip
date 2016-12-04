using System.IO;
using CryptZip.Encryption;
using CryptZip.Encryption.Padding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class CBCTests
    {
        [TestMethod]
        public void Encrypt_EncryptsBlock_Encrypted()
        {
            byte[] bytes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var encryptor = new CBC(new AES(bytes), new PKCS7Padding(), bytes);
            var input = new MemoryStream(bytes);
            var output = new MemoryStream();
            encryptor.Encrypt(input, output);
            byte[] result = output.GetBuffer().SubArray(0, 32);
            CollectionAssert.AreEqual(new byte[] { 0xdb, 0xf1, 0x84, 0x11, 0x2e, 0xb9, 0x11, 0x16, 0x59, 0x71, 0x2b, 0xaf, 0xcf, 0xf2, 0xab, 0x24, 0x28, 0xa7, 0x63, 0x6c, 0x8e, 0x91, 0x91, 0xcb, 0x11, 0x39, 0x72, 0x2e, 0xb7, 0xa6, 0x4c, 0x2e },
                result);
        }

        [TestMethod]
        public void Decrypt_DecryptsBlock_Decrypted()
        {
            byte[] bytes = { 0xdb, 0xf1, 0x84, 0x11, 0x2e, 0xb9, 0x11, 0x16, 0x59, 0x71, 0x2b, 0xaf, 0xcf, 0xf2, 0xab, 0x24, 0x28, 0xa7, 0x63, 0x6c, 0x8e, 0x91, 0x91, 0xcb, 0x11, 0x39, 0x72, 0x2e, 0xb7, 0xa6, 0x4c, 0x2e };
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var encryptor = new CBC(new AES(key), new PKCS7Padding(), key);
            var input = new MemoryStream(bytes);
            var output = new MemoryStream();
            encryptor.Decrypt(input, output);
            byte[] result = output.GetBuffer().SubArray(0, 16);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, result);
        }

        [TestMethod]
        public void Encrypt_Encrypts10Bytes_Encrypted()
        {
            byte[] bytes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var encryptor = new CBC(new AES(key), new PKCS7Padding(), key);
            var input = new MemoryStream(bytes);
            var output = new MemoryStream();
            encryptor.Encrypt(input, output);
            byte[] result = output.GetBuffer().SubArray(0, 16);
            CollectionAssert.AreEqual(new byte[] { 0xff, 0x07, 0x90, 0x16, 0xe7, 0x8a, 0x17, 0xa4, 0xb5, 0xce, 0x2e, 0xac, 0x2b, 0x00, 0xe8, 0x48 },
                result);
        }

        [TestMethod]
        public void Decrypt_Decrypts10Bytes_Decrypted()
        {
            byte[] bytes = { 0xff, 0x07, 0x90, 0x16, 0xe7, 0x8a, 0x17, 0xa4, 0xb5, 0xce, 0x2e, 0xac, 0x2b, 0x00, 0xe8, 0x48 };
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var encryptor = new CBC(new AES(key), new PKCS7Padding(), key);
            var input = new MemoryStream(bytes);
            var output = new MemoryStream();
            encryptor.Decrypt(input, output);
            byte[] result = output.GetBuffer().SubArray(0, 10);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                result);
        }
    }
}